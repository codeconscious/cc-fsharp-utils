namespace CCFSharpUtils.Tests

open Xunit
open CCFSharpUtils.Library
open System

module ArrayTests =

    module TakeLastTests =

        [<Fact>]
        let ``returns last items when enough items in the array`` () =
            let arr = [| 1..10 |]
            let expected = [| 6..10 |]
            let actual = arr |> Array.takeLast 5
            Assert.Equal<int array>(expected, actual)

        [<Fact>]
        let ``returns entire array when count exceeds its length`` () =
            let arr = [| 1..10 |]
            let expected = arr
            let actual = arr |> Array.takeLast 1_000
            Assert.Equal<int array>(expected, actual)

        [<Fact>]
        let ``returns empty array when no items in the array`` () =
            let arr = Array.empty<int>
            let expected = arr
            let actual = arr |> Array.takeLast 1_000
            Assert.Equal<int array>(expected, actual)

        [<Fact>]
        let ``returns empty array when count is negative`` () =
            let arr = [| 1..10 |]
            let expected = Array.empty<int>
            let actual = arr |> Array.takeLast -5
            Assert.Equal<int array>(expected, actual)

        [<Fact>]
        let ``throws when array is null`` () =
            Assert.Throws<ArgumentNullException>(fun _ -> null |> Array.takeLast 1 |> ignore)

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
        let ``returns Error with emptyErr when array is empty`` () =
            let result = [||] |> Array.ensureOne emptyErr multipleErr
            match result with
            | Error e -> Assert.Equal(emptyErr, e)
            | Ok _    -> failwith "Expected Error for empty array"

        [<Fact>]
        let ``returns Ok with the original array when it has exactly one element`` () =
            let single = [| 100 |]
            let result = single |> Array.ensureOne emptyErr multipleErr
            match result with
            | Ok x    -> Assert.Equal(single[0], x[0])
            | Error e -> failwithf $"Expected Ok but got Error %s{e}"

        [<Fact>]
        let ``returns Error with multipleErr when array has more than one element`` () =
            let result = [| 100; 200 |] |> Array.ensureOne emptyErr multipleErr
            match result with
            | Error e -> Assert.Equal(multipleErr, e)
            | Ok _    -> failwith "Expected Error for multiple-element array"

    module EnsureSizeTests =

        let arr = [| 1; 2 |]
        let tooSmallErr = "too small"
        let tooLargeErr = "too large"

        [<Fact>]
        let ``returns Error with tooSmallErr when array is smaller than target`` () =
            let target = 3
            match arr |> Array.ensureSize target tooSmallErr tooLargeErr with
            | Error e -> Assert.Equal(tooSmallErr, e)
            | Ok _    -> failwith "Expected Error for array smaller than target"

        [<Fact>]
        let ``returns Ok with the original array when length equals target`` () =
            let target = 2
            match arr |> Array.ensureSize target tooSmallErr tooLargeErr with
            | Ok arr' -> Assert.Equal<int array>(arr', arr)
            | Error e -> failwithf $"Expected Ok but got Error %s{e}"

        [<Fact>]
        let ``returns Error with tooLargeErr when array is larger than target`` () =
            let target = 1
            match arr |> Array.ensureSize target tooSmallErr tooLargeErr with
            | Error e -> Assert.Equal(tooLargeErr, e)
            | Ok _    -> failwith "Expected Error for array larger than target"

    module TryGetSingleTests =

        let emptyErr = "empty error"
        let multipleErr = "multiple error"

        [<Fact>]
        let ``returns Error with emptyErr when array is empty`` () =
            let result = [||] |> Array.tryGetSingle emptyErr multipleErr
            match result with
            | Error e -> Assert.Equal(emptyErr, e)
            | Ok v    -> failwithf $"Expected Error but got Ok %d{v}"

        [<Fact>]
        let ``returns Ok with the single element when array has exactly one element`` () =
            let value = 99
            let result = [| value |] |> Array.tryGetSingle emptyErr multipleErr
            match result with
            | Ok v    -> Assert.Equal(value, v)
            | Error e -> failwithf $"Expected Ok but got Error %s{e}"

        [<Fact>]
        let ``returns Error with multipleErr when array has multiple elements`` () =
            let result = [| 1; 2; 3 |] |> Array.tryGetSingle emptyErr multipleErr
            match result with
            | Error e -> Assert.Equal(multipleErr, e)
            | Ok v    -> failwithf $"Expected Error but got Ok %d{v}"
