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
    let ``caseInsensitiveContains handles Japanese strings`` () =
        let input = ["関数型プログラミング"; "楽しいぞ"]
        Assert.True  <| List.containsIgnoreCase "関数型プログラミング" input
        Assert.False <| List.containsIgnoreCase "いや、楽しくないや"   input
