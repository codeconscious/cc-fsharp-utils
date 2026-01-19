namespace CCFSharpUtils.Tests

open Xunit
open System
open CCFSharpUtils.Library

module SeqTests =

    module ContainsIgnoreCaseTests =

        [<Fact>]
        let ``containsIgnoreCase returns true when exact match exists`` () =
            let input = ["Hello"; "World"; "Test"]
            Assert.True <| Seq.containsIgnoreCase "Hello" input
            Assert.True <| Seq.containsIgnoreCase "World" input
            Assert.True <| Seq.containsIgnoreCase "Test" input

        [<Fact>]
        let ``containsIgnoreCase returns true when exists but case differs`` () =
            let input = ["hello"; "WORLD"; "test"]
            Assert.True <| Seq.containsIgnoreCase "Hello" input
            Assert.True <| Seq.containsIgnoreCase "hello" input
            Assert.True <| Seq.containsIgnoreCase "HELLO" input
            Assert.True <| Seq.containsIgnoreCase "wOrLd" input
            Assert.True <| Seq.containsIgnoreCase "tESt" input
            Assert.True <| Seq.containsIgnoreCase "TEST" input

        [<Fact>]
        let ``containsIgnoreCase returns false when text not in sequence`` () =
            let input = ["Hello"; "World"; "Test"]
            Assert.False <| Seq.containsIgnoreCase "Missing" input

        [<Fact>]
        let ``containsIgnoreCase works with empty sequence`` () =
            Assert.False <| Seq.containsIgnoreCase "Any" []

        [<Fact>]
        let ``containsIgnoreCase handles null or empty strings`` () =
            let input = [String.Empty; null; "Test"]
            Assert.True <| Seq.containsIgnoreCase String.Empty input
            Assert.True <| Seq.containsIgnoreCase null input

        [<Fact>]
        let ``containsIgnoreCase handles Japanese strings`` () =
            let input = ["関数型プログラミング"; "楽しいぞ"]
            Assert.True <| Seq.containsIgnoreCase "関数型プログラミング" input
            Assert.False <| Seq.containsIgnoreCase "いや、楽しくないや" input

    module EnsureOneTests =

        let emptyErr = "empty error"
        let multipleErr = "multiple error"

        [<Fact>]
        let ``returns Error with emptyErr when sequence is empty`` () =
            let result = Seq.empty<int> |> Seq.ensureOne emptyErr multipleErr
            match result with
            | Error e -> Assert.Equal(emptyErr, e)
            | Ok _    -> failwith "Expected Error for empty sequence"

        [<Fact>]
        let ``returns Ok with the original sequence when it has exactly one element`` () =
            let single = seq { yield 100 }
            let result = single |> Seq.ensureOne emptyErr multipleErr
            match result with
            | Ok x    -> Assert.Equal(single |> Seq.toList |> Seq.head,
                                           x |> Seq.toList |> Seq.head)
            | Error e -> failwithf $"Expected Ok but got Error %s{e}"

        [<Fact>]
        let ``returns Error with multipleErr when sequence has more than one element`` () =
            let many = seq { yield 1; yield 2 }
            let result = many |> Seq.ensureOne emptyErr multipleErr
            match result with
            | Error e -> Assert.Equal(multipleErr, e)
            | Ok _    -> failwith "Expected Error for multiple-element sequence"

    module EnsureSizeTests =

        let tooSmallErr = "too small"
        let tooLargeErr = "too large"

        [<Fact>]
        let ``returns Error with tooSmallErr when sequence is smaller than target`` () =
            let target = 3
            let s = seq { 1; 2 }
            match s |> Seq.ensureSize target tooSmallErr tooLargeErr with
            | Error e -> Assert.Equal(tooSmallErr, e)
            | Ok _    -> failwith "Expected Error for sequence smaller than target"

        [<Fact>]
        let ``returns Ok with the original sequence when length equals target`` () =
            let target = 2
            let s = seq { 10; 20 }
            match s |> Seq.ensureSize target tooSmallErr tooLargeErr with
            | Ok s'   -> Assert.Equal<int seq>(s', s)
            | Error e -> failwithf $"Expected Ok but got Error %s{e}"

        [<Fact>]
        let ``returns Error with tooLargeErr when sequence is larger than target`` () =
            let target = 1
            let s = seq { 1; 2; 3 }
            match s |> Seq.ensureSize target tooSmallErr tooLargeErr with
            | Error e -> Assert.Equal(tooLargeErr, e)
            | Ok _    -> failwith "Expected Error for sequence larger than target"


    module TryGetSingleTests =

        let emptyErr = "empty error"
        let multipleErr = "multiple error"

        [<Fact>]
        let ``returns Error with emptyErr when sequence is empty`` () =
            let result = Seq.empty<int> |> Seq.tryGetSingle emptyErr multipleErr
            match result with
            | Error e -> Assert.Equal(emptyErr, e)
            | Ok v    -> failwithf $"Expected Error but got Ok %d{v}"

        [<Fact>]
        let ``returns Ok with the single element when sequence has exactly one element`` () =
            let single = seq { yield 99 }
            let result = single |> Seq.tryGetSingle emptyErr multipleErr
            match result with
            | Ok v    -> Assert.Equal(99, v)
            | Error e -> failwithf $"Expected Ok but got Error %s{e}"

        [<Fact>]
        let ``returns Error with multipleErr when sequence has multiple elements`` () =
            let many = seq { yield 1; yield 2; yield 3 }
            let result = many |> Seq.tryGetSingle emptyErr multipleErr
            match result with
            | Error e -> Assert.Equal(multipleErr, e)
            | Ok v    -> failwithf $"Expected Error but got Ok %d{v}"
