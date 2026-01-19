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

    module EnsureOneTests =

        let emptyErr = "empty error"
        let multipleErr = "multiple error"

        [<Fact>]
        let ``returns Error with emptyErr when sequence is empty`` () =
            let result = [||] |> Array.ensureOne emptyErr multipleErr
            match result with
            | Error e -> Assert.Equal(emptyErr, e)
            | Ok _    -> failwith "Expected Error for empty sequence"

        [<Fact>]
        let ``returns Ok with the original sequence when it has exactly one element`` () =
            let single = [| 100 |]
            let result = single |> Array.ensureOne emptyErr multipleErr
            match result with
            | Ok x    -> Assert.Equal(single[0], x[0])
            | Error e -> failwithf $"Expected Ok but got Error %s{e}"

        [<Fact>]
        let ``returns Error with multipleErr when sequence has more than one element`` () =
            let result = [| 100; 200 |] |> Array.ensureOne emptyErr multipleErr
            match result with
            | Error e -> Assert.Equal(multipleErr, e)
            | Ok _    -> failwith "Expected Error for multiple-element sequence"

    module EnsureSizeTests =

        let arr = [| 1; 2 |]
        let tooSmallErr = "too small"
        let tooLargeErr = "too large"

        [<Fact>]
        let ``returns Error with tooSmallErr when sequence is smaller than target`` () =
            let target = 3
            match arr |> Array.ensureSize target tooSmallErr tooLargeErr with
            | Error e -> Assert.Equal(tooSmallErr, e)
            | Ok _    -> failwith "Expected Error for sequence smaller than target"

        [<Fact>]
        let ``returns Ok with the original sequence when length equals target`` () =
            let target = 2
            match arr |> Array.ensureSize target tooSmallErr tooLargeErr with
            | Ok [| returnedSeq |] -> Assert.Equal<int array>(returnedSeq, arr)
            | Ok _    -> failwith "Expected a single-element list containing the original sequence"
            | Error e -> failwithf $"Expected Ok but got Error %s{e}"

        [<Fact>]
        let ``returns Error with tooLargeErr when sequence is larger than target`` () =
            let target = 1
            match arr |> Array.ensureSize target tooSmallErr tooLargeErr with
            | Error e -> Assert.Equal(tooLargeErr, e)
            | Ok _    -> failwith "Expected Error for sequence larger than target"

    module TryGetSingleTests =

        let emptyErr = "empty error"
        let multipleErr = "multiple error"

        [<Fact>]
        let ``returns Error with emptyErr when sequence is empty`` () =
            let result = [||] |> Array.tryGetSingle emptyErr multipleErr
            match result with
            | Error e -> Assert.Equal(emptyErr, e)
            | Ok v    -> failwithf $"Expected Error but got Ok %d{v}"

        [<Fact>]
        let ``returns Ok with the single element when sequence has exactly one element`` () =
            let value = 99
            let result = [| value |] |> Array.tryGetSingle emptyErr multipleErr
            match result with
            | Ok v    -> Assert.Equal(value, v)
            | Error e -> failwithf $"Expected Ok but got Error %s{e}"

        [<Fact>]
        let ``returns Error with multipleErr when sequence has multiple elements`` () =
            let result = [| 1; 2; 3 |] |> Array.tryGetSingle emptyErr multipleErr
            match result with
            | Error e -> Assert.Equal(multipleErr, e)
            | Ok v    -> failwithf $"Expected Error but got Ok %d{v}"
