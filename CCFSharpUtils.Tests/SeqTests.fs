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
