namespace CCFSharpUtils.Tests

open Xunit
open System
open CCFSharpUtils.Library

module SeqTests =

    [<Fact>]
    let ``caseInsensitiveContains returns true when exact match exists`` () =
        let input = ["Hello"; "World"; "Test"]
        Assert.True <| Seq.containsIgnoreCase "Hello" input
        Assert.True <| Seq.containsIgnoreCase "World" input
        Assert.True <| Seq.containsIgnoreCase "Test" input

    [<Fact>]
    let ``caseInsensitiveContains returns true when exists but case differs`` () =
        let input = ["hello"; "WORLD"; "test"]
        Assert.True <| Seq.containsIgnoreCase "Hello" input
        Assert.True <| Seq.containsIgnoreCase "hello" input
        Assert.True <| Seq.containsIgnoreCase "HELLO" input
        Assert.True <| Seq.containsIgnoreCase "wOrLd" input
        Assert.True <| Seq.containsIgnoreCase "tESt" input
        Assert.True <| Seq.containsIgnoreCase "TEST" input

    [<Fact>]
    let ``caseInsensitiveContains returns false when text not in sequence`` () =
        let input = ["Hello"; "World"; "Test"]
        Assert.False <| Seq.containsIgnoreCase "Missing" input

    [<Fact>]
    let ``caseInsensitiveContains works with empty sequence`` () =
        Assert.False <| Seq.containsIgnoreCase "Any" []

    [<Fact>]
    let ``caseInsensitiveContains handles null or empty strings`` () =
        let input = [String.Empty; null; "Test"]
        Assert.True <| Seq.containsIgnoreCase String.Empty input
        Assert.True <| Seq.containsIgnoreCase null input

    [<Fact>]
    let ``caseInsensitiveContains handles Japanese strings`` () =
        let input = ["関数型プログラミング"; "楽しいぞ"]
        Assert.True <| Seq.containsIgnoreCase "関数型プログラミング" input
        Assert.False <| Seq.containsIgnoreCase "いや、楽しくないや" input

    module EnsureOneTests =

        let empty = "empty error"
        let multiple = "multiple error"

        [<Fact>]
        let ``returns Error with emptyErr when sequence is empty`` () =
            let result = Seq.ensureOne empty multiple Seq.empty<int>
            match result with
            | Error e -> Assert.Equal(empty, e)
            | Ok _ -> failwith "Expected Error for empty sequence"

        [<Fact>]
        let ``returns Ok with the original sequence when it has exactly one element`` () =
            let single = seq { yield 100 }
            let result = Seq.ensureOne empty multiple single
            match result with
            | Ok x -> Assert.Equal(single |> Seq.toList |> Seq.head,
                                        x |> Seq.toList |> Seq.head)
            | Error e -> failwithf $"Expected Ok but got Error %s{e}"

        [<Fact>]
        let ``returns Error with multipleErr when sequence has more than one element`` () =
            let many = seq { yield 1; yield 2 }
            let result = Seq.ensureOne empty multiple many
            match result with
            | Error e -> Assert.Equal(multiple, e)
            | Ok _ -> failwith "Expected Error for multiple-element sequence"

    module EnsureSizeTests =

        let tooSmall = "too small"
        let tooLarge = "too large"

        [<Fact>]
        let ``returns Error with tooSmallErr when sequence is smaller than target`` () =
            let target = 3
            let s = seq { 1; 2 }
            match Seq.ensureSize target tooSmall tooLarge s with
            | Error e -> Assert.Equal(tooSmall, e)
            | Ok _ -> failwith "Expected Error for sequence smaller than target"

        [<Fact>]
        let ``returns Ok with the original sequence when length equals target`` () =
            let target = 2
            let s = seq { 10; 20 }
            match Seq.ensureSize target tooSmall tooLarge s with
            | Ok [returnedSeq] -> Assert.Equal<int seq>(returnedSeq, s)
            | Ok _ -> failwith "Expected a single-element list containing the original sequence"
            | Error e -> failwithf $"Expected Ok but got Error %s{e}"

        [<Fact>]
        let ``returns Error with tooLargeErr when sequence is larger than target`` () =
            let target = 1
            let s = seq { 1; 2; 3 }
            match Seq.ensureSize target tooSmall tooLarge s with
            | Error e -> Assert.Equal(tooLarge, e)
            | Ok _ -> failwith "Expected Error for sequence larger than target"


    module TryGetSingleTests =

        let empty = "empty error"
        let multiple = "multiple error"

        [<Fact>]
        let ``returns Error with emptyErr when sequence is empty`` () =
            let emptySeq = Seq.empty<int>
            let result = emptySeq |> Seq.tryGetSingle empty multiple
            match result with
            | Error e -> Assert.Equal(empty, e)
            | Ok v -> failwithf $"Expected Error but got Ok %d{v}"

        [<Fact>]
        let ``returns Ok with the single element when sequence has exactly one element`` () =
            let single = seq { yield 99 }
            let result = single |> Seq.tryGetSingle empty multiple
            match result with
            | Ok v -> Assert.Equal(99, v)
            | Error e -> failwithf $"Expected Ok but got Error %s{e}"

        [<Fact>]
        let ``returns Error with multipleErr when sequence has more than one element`` () =
            let many = seq { yield 1; yield 2; yield 3 }
            let result = many |> Seq.tryGetSingle empty multiple
            match result with
            | Error e -> Assert.Equal(multiple, e)
            | Ok v -> failwithf $"Expected Error but got Ok %d{v}"
