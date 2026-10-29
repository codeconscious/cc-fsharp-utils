namespace CCFSharpUtils.Tests.Collections.NonEmptyListTests

open Xunit
open CCFSharpUtils.Collections
open System
open FSharpPlus.Data

module NonEmptyListTests =
    type nlist<'a> = NonEmptyList<'a>

    module ShuffleTests =

        [<Fact>]
        let ``preserves nlist length`` () =
            let rng = Random(55)
            let items = NonEmptyList.ofSeq [1; 2; 3; 4; 5]
            let result = NonEmptyList.shuffle rng items
            Assert.Equal(NonEmptyList.length items, NonEmptyList.length result)

        [<Fact>]
        let ``preserves all elements`` () =
            let rng = Random(55)
            let items = NonEmptyList.ofSeq [1; 2; 3; 4; 5]
            let result = NonEmptyList.shuffle rng items
            let sortedOriginal = NonEmptyList.toArray items |> Array.sort
            let sortedResult = NonEmptyList.toArray result |> Array.sort
            Assert.Equal<int array>(sortedOriginal, sortedResult)

        [<Fact>]
        let ``handles single element`` () =
            let rng = Random(55)
            let items = NonEmptyList.ofSeq [10]
            let result = NonEmptyList.shuffle rng items
            Assert.Equal<int nlist>(items, result)

        [<Fact>]
        let ``handles two elements`` () =
            let rng = Random(55)
            let items = NonEmptyList.ofSeq [1; 2]
            let result = NonEmptyList.shuffle rng items
            Assert.Equal(2, NonEmptyList.length result)
            let resultArr = NonEmptyList.toArray result
            Assert.True((resultArr = [|1; 2|] || resultArr = [|2; 1|]))

        [<Fact>]
        let ``is deterministic with the same seed`` () =
            let items = NonEmptyList.ofSeq [1; 2; 3; 4; 5]
            let result1 = NonEmptyList.shuffle (Random(55)) items
            let result2 = NonEmptyList.shuffle (Random(55)) items
            Assert.Equal<int nlist>(result1, result2)

        [<Fact>]
        let ``produces different results with different seeds`` () =
            let items = NonEmptyList.ofSeq [1; 2; 3; 4; 5; 6; 7; 8; 9; 10]
            let result1 = NonEmptyList.shuffle (Random(1)) items
            let result2 = NonEmptyList.shuffle (Random(2)) items
            Assert.NotEqual<int nlist>(result1, result2)
