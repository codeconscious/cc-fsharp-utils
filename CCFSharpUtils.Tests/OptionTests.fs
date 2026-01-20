namespace CCFSharpUtils.Tests

open Xunit
open CCFSharpUtils.Library

module OptionTests =

    module MapElseTets =
        let alt = -1
        let mapping = (+) 10

        [<Fact>]
        let ``returns value calculated from Option when Some`` () =
            let opt = Some 2
            let expected = 12
            let actual = opt |> Option.mapElse mapping alt
            Assert.Equal(expected, actual)

        [<Fact>]
        let ``returns default value if None`` () =
            let opt = None
            let expected = alt
            let actual = opt |> Option.mapElse mapping alt
            Assert.Equal(expected, actual)
