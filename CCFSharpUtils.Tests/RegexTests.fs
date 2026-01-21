namespace CCFSharpUtils.Tests

open Xunit
open CCFSharpUtils.Library

module RegexTests =

    module RegexMatchValueActivePatternTests =

        [<Fact>]
        let ``does not match with non-matching data`` () =
            let input = "0123456789"
            let pattern = @"^\D+$"
            match input with
            | Rgx.MatchValue pattern _ -> failwith "Unexpected regex match when none was expected"
            | _ -> Assert.True true // Does not match, as expected.

        [<Fact>]
        let ``returns a match group list of one item`` () =
            let input = "0123456789"
            let pattern = @"^\d+$"
            let expected = "0123456789"
            match input with
            | Rgx.MatchValue pattern x -> Assert.Equal(expected, x)
            | _ -> failwith "No matches found though at least one was expected"

        [<Fact>]
        let ``returns a match group list of two items and ignored groups`` () =
            let input = "01234 56789"
            let pattern = @"(\d+)\s(\d+)"
            let expected = "01234 56789"
            match input with
            | Rgx.MatchValue pattern x -> Assert.Equal(expected, x)
            | _ -> failwith "No matches found though at least one was expected"

    module RegexMatchGroupActivePatternTests =

        [<Fact>]
        let ``does not match with non-matching data`` () =
            let input = "0123456789"
            let pattern = @"^(\D+)$"
            match input with
            | Rgx.MatchGroups pattern _ -> failwith "Unexpected regex match when none was expected"
            | _ -> Assert.True true // Does not match, as expected.

        [<Fact>]
        let ``returns a match group list of one item`` () =
            let input = "0123456789"
            let pattern = @"^(\d+)$"
            let expected = ["0123456789"]
            match input with
            | Rgx.MatchGroups pattern x -> Assert.Equal<string list>(expected, x)
            | _ -> failwith "No matches found though at least one was expected"

        [<Fact>]
        let ``returns a match group list of one item extracted via list pattern matching`` () =
            let input = "0123456789"
            let pattern = @"^(\d+)$"
            let expected = "0123456789"
            match input with
            | Rgx.MatchGroups pattern [x] -> Assert.Equal(expected, x)
            | _ -> failwith "No matches found though at least one was expected"

        [<Fact>]
        let ``returns a match group list of two items`` () =
            let input = "01234 56789"
            let pattern = @"(\d+)\s(\d+)"
            let expected = ["01234"; "56789"]
            match input with
            | Rgx.MatchGroups pattern x -> Assert.Equal<string list>(expected, x)
            | _ -> failwith "No matches found though at least one was expected"
