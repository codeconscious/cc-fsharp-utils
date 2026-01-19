namespace CCFSharpUtils.Tests

open Xunit
open System
open CCFSharpUtils.Library

module ListTests =

    module ContainsIgnoreCaseTests =
        [<Fact>]
        let ``containsIgnoreCase returns true when exact match exists`` () =
            let input = ["Hello"; "World"; "Test"]
            Assert.True <| List.containsIgnoreCase "Hello" input
            Assert.True <| List.containsIgnoreCase "World" input
            Assert.True <| List.containsIgnoreCase "Test" input

        [<Fact>]
        let ``containsIgnoreCase returns true when exists but case differs`` () =
            let input = ["hello"; "WORLD"; "test"]
            Assert.True <| List.containsIgnoreCase "Hello" input
            Assert.True <| List.containsIgnoreCase "hello" input
            Assert.True <| List.containsIgnoreCase "HELLO" input
            Assert.True <| List.containsIgnoreCase "wOrLd" input
            Assert.True <| List.containsIgnoreCase "tESt" input
            Assert.True <| List.containsIgnoreCase "TEST" input

        [<Fact>]
        let ``containsIgnoreCase returns false when text not in sequence`` () =
            let input = ["Hello"; "World"; "Test"]
            Assert.False <| List.containsIgnoreCase "Missing" input

        [<Fact>]
        let ``containsIgnoreCase works with empty sequence`` () =
            Assert.False <| List.containsIgnoreCase "Any" []

        [<Fact>]
        let ``containsIgnoreCase handles null or empty strings`` () =
            let input = [String.Empty; null; "Test"]
            Assert.True <| List.containsIgnoreCase String.Empty input
            Assert.True <| List.containsIgnoreCase null input

        [<Fact>]
        let ``containsIgnoreCase handles Japanese strings`` () =
            let input = ["関数型プログラミング"; "楽しいぞ"]
            Assert.True  <| List.containsIgnoreCase "関数型プログラミング" input
            Assert.False <| List.containsIgnoreCase "いや、楽しくないや" input

    module EnsureOneTests =

        let emptyErr = "empty error"
        let multipleErr = "multiple error"

        [<Fact>]
        let ``returns Error with emptyErr when sequence is empty`` () =
            let result = [] |> List.ensureOne emptyErr multipleErr
            match result with
            | Error e -> Assert.Equal(emptyErr, e)
            | Ok _    -> failwith "Expected Error for empty sequence"

        [<Fact>]
        let ``returns Ok with the original sequence when it has exactly one element`` () =
            let single = [100]
            let result = single |> List.ensureOne emptyErr multipleErr
            match result with
            | Ok x    -> Assert.Equal(single[0], x[0])
            | Error e -> failwithf $"Expected Ok but got Error %s{e}"

        [<Fact>]
        let ``returns Error with multipleErr when sequence has more than one element`` () =
            let result = [100; 200] |> List.ensureOne emptyErr multipleErr
            match result with
            | Error e -> Assert.Equal(multipleErr, e)
            | Ok _    -> failwith "Expected Error for multiple-element sequence"

    module EnsureSizeTests =

        let lst = [1; 2]
        let tooSmallErr = "too small"
        let tooLargeErr = "too large"

        [<Fact>]
        let ``returns Error with tooSmallErr when sequence is smaller than target`` () =
            let target = 3
            match lst |> List.ensureSize target tooSmallErr tooLargeErr with
            | Error e -> Assert.Equal(tooSmallErr, e)
            | Ok _    -> failwith "Expected Error for sequence smaller than target"

        [<Fact>]
        let ``returns Ok with the original sequence when length equals target`` () =
            let target = 2
            match lst |> List.ensureSize target tooSmallErr tooLargeErr with
            | Ok [returnedSeq] -> Assert.Equal<int list>(returnedSeq, lst)
            | Ok _    -> failwith "Expected a single-element list containing the original sequence"
            | Error e -> failwithf $"Expected Ok but got Error %s{e}"

        [<Fact>]
        let ``returns Error with tooLargeErr when sequence is larger than target`` () =
            let target = 1
            match lst |> List.ensureSize target tooSmallErr tooLargeErr with
            | Error e -> Assert.Equal(tooLargeErr, e)
            | Ok _    -> failwith "Expected Error for sequence larger than target"

    module TryGetSingleTests =

        let emptyErr = "empty error"
        let multipleErr = "multiple error"

        [<Fact>]
        let ``returns Error with emptyErr when sequence is empty`` () =
            let result = [] |> List.tryGetSingle emptyErr multipleErr
            match result with
            | Error e -> Assert.Equal(emptyErr, e)
            | Ok v    -> failwithf $"Expected Error but got Ok %d{v}"

        [<Fact>]
        let ``returns Ok with the single element when sequence has exactly one element`` () =
            let value = 99
            let result = [value] |> List.tryGetSingle emptyErr multipleErr
            match result with
            | Ok v    -> Assert.Equal(value, v)
            | Error e -> failwithf $"Expected Ok but got Error %s{e}"

        [<Fact>]
        let ``returns Error with multipleErr when sequence has multiple elements`` () =
            let result = [ 1; 2; 3 ] |> List.tryGetSingle emptyErr multipleErr
            match result with
            | Error e -> Assert.Equal(multipleErr, e)
            | Ok v    -> failwithf $"Expected Error but got Ok %d{v}"
