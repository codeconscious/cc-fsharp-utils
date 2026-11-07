namespace CCFSharpUtils.Tests

open Xunit
open CCFSharpUtils
open System

module CommonTests =

    module CompareTests =

        [<Fact>]
        let ``compare' returns LT when subject is less than target`` () =
            Assert.True <| (compare' 3 5 = LT)
            Assert.True <| (compare' (-5) 0 = LT)
            Assert.True <| (compare' Int32.MinValue Int32.MaxValue = LT)

        [<Fact>]
        let ``compare' returns EQ when values are equal`` () =
            Assert.True <| (compare' 5 5 = EQ)
            Assert.True <| (compare' 0 0 = EQ)
            Assert.True <| (compare' (-7) (-7) = EQ)

        [<Fact>]
        let ``compare' returns GT when subject is greater than target`` () =
            Assert.True <| (compare' 7 5 = GT)
            Assert.True <| (compare' 0 (-5) = GT)
            Assert.True <| (compare' Int32.MaxValue Int32.MinValue = GT)

        [<Fact>]
        let ``compare' works with int64 values`` () =
            Assert.True <| (compare' 3_000_000_000L 1L = GT)
            Assert.True <| (compare' 1L 3_000_000_000L = LT)
            Assert.True <| (compare' 3_000_000_000L 3_000_000_000L = EQ)

        [<Fact>]
        let ``compare' works with floats`` () =
            Assert.True <| (compare' 1.5 2.5 = LT)
            Assert.True <| (compare' 2.5 1.5 = GT)
            Assert.True <| (compare' 0.1 0.1 = EQ)

        [<Fact>]
        let ``compare' works with chars`` () =
            Assert.True <| (compare' 'a' 'b' = LT)
            Assert.True <| (compare' 'b' 'a' = GT)
            Assert.True <| (compare' 'a' 'a' = EQ)

        [<Fact>]
        let ``compare' orders strings ordinally`` () =
            Assert.True <| (compare' "apple" "banana" = LT)
            Assert.True <| (compare' "banana" "apple" = GT)
            Assert.True <| (compare' "apple" "apple" = EQ)
            Assert.True <| (compare' "B" "a" = LT)
            Assert.True <| (compare' "a" "" = GT)

        [<Fact>]
        let ``compare' orders tuples lexicographically`` () =
            Assert.True <| (compare' (1, "b") (1, "c") = LT)
            Assert.True <| (compare' (2, "a") (1, "z") = GT)
            Assert.True <| (compare' (1, "a") (1, "a") = EQ)

        [<Fact>]
        let ``compare' orders dates chronologically`` () =
            let earlier = DateTime(2026, 1, 1)
            let later = DateTime(2026, 11, 7)
            Assert.True <| (compare' earlier later = LT)
            Assert.True <| (compare' later earlier = GT)
            Assert.True <| (compare' earlier earlier = EQ)
