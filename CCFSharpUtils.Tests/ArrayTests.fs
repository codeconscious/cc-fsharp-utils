namespace CCFSharpUtils.Tests

open Xunit
open CCFSharpUtils.Library

module ArrayTests =

    module HasMultipleTests =

        [<Fact>]
        let ``hasMultiple returns true for array with more than one element`` () =
            Assert.True <| Array.hasMultiple [| 1; 2; 3 |]

        [<Fact>]
        let ``hasMultiple returns false for empty array`` () =
            Assert.False <| Array.hasMultiple [||]

        [<Fact>]
        let ``hasMultiple returns false for single-element array`` () =
            Assert.False <| Array.hasMultiple [| 0 |]

        [<Fact>]
        let ``hasMultiple works with different types of arrays`` () =
            Assert.True <| Array.hasMultiple [| "hello"; "world" |]
            Assert.True <| Array.hasMultiple [| 1.0; 2.0; 3.0 |]
            Assert.True <| Array.hasMultiple [| false; true; true |]
            Assert.True <| Array.hasMultiple [| Array.sum; Array.length |]

        [<Fact>]
        let ``hasMultiple handles large arrays`` () =
            Assert.True <| Array.hasMultiple (Array.init 100 id)
