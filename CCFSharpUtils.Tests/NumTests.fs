namespace CCFSharpUtils.Tests

open Xunit
open CCFSharpUtils
open System

module NumericsTests =

    module IsZeroTests =

        [<Fact>]
        let ``isZero returns true for any zero value`` () =
            Assert.True <| Num.isZero 0
            Assert.True <| Num.isZero 0u
            Assert.True <| Num.isZero 0us
            Assert.True <| Num.isZero 0.
            Assert.True <| Num.isZero 0L
            Assert.True <| Num.isZero 0m
            Assert.True <| Num.isZero -0
            Assert.True <| Num.isZero -0.
            Assert.True <| Num.isZero -0L
            Assert.True <| Num.isZero -0m

        [<Fact>]
        let ``isZero returns false for any non-zero value`` () =
            Assert.False <| Num.isZero 1
            Assert.False <| Num.isZero -1
            Assert.False <| Num.isZero Int64.MinValue
            Assert.False <| Num.isZero Int64.MaxValue
            Assert.False <| Num.isZero 2
            Assert.False <| Num.isZero 1u
            Assert.False <| Num.isZero 1us
            Assert.False <| Num.isZero -0.0000000000001
            Assert.False <| Num.isZero 0.0000000000001
            Assert.False <| Num.isZero 1.
            Assert.False <| Num.isZero 1L
            Assert.False <| Num.isZero 1m

    module IsOneTests =

        [<Fact>]
        let ``isOne returns true for any one value`` () =
            Assert.True <| Num.isOne 1
            Assert.True <| Num.isOne 1u
            Assert.True <| Num.isOne 1us
            Assert.True <| Num.isOne 1.
            Assert.True <| Num.isOne 1L
            Assert.True <| Num.isOne 1m

        [<Fact>]
        let ``isOne returns false for any non-one value`` () =
            Assert.False <| Num.isOne 0
            Assert.False <| Num.isOne -1
            Assert.False <| Num.isOne Int64.MinValue
            Assert.False <| Num.isOne Int64.MaxValue
            Assert.False <| Num.isOne 2
            Assert.False <| Num.isOne 0u
            Assert.False <| Num.isOne 16u
            Assert.False <| Num.isOne 0us
            Assert.False <| Num.isOne -0.
            Assert.False <| Num.isOne 0.001
            Assert.False <| Num.isOne 0L
            Assert.False <| Num.isOne 0m

    module IsEvenTests =

        [<Fact>]
        let ``isEven returns true for even values`` () =
            // Integers
            Assert.True <| Num.isEven 0
            Assert.True <| Num.isEven 2
            Assert.True <| Num.isEven 4
            Assert.True <| Num.isEven -2
            Assert.True <| Num.isEven -4
            Assert.True <| Num.isEven Int32.MinValue

            // Unsigned integers
            Assert.True <| Num.isEven 0u
            Assert.True <| Num.isEven 2u
            Assert.True <| Num.isEven 4u

            // Unsigned shorts
            Assert.True <| Num.isEven 0us
            Assert.True <| Num.isEven 2us
            Assert.True <| Num.isEven 4us

            // Long integers
            Assert.True <| Num.isEven 0L
            Assert.True <| Num.isEven 2L
            Assert.True <| Num.isEven -2L
            Assert.True <| Num.isEven Int64.MinValue

            // Floats
            Assert.True <| Num.isEven 0.
            Assert.True <| Num.isEven 2.
            Assert.True <| Num.isEven 4.
            Assert.True <| Num.isEven -2.
            Assert.True <| Num.isEven -4.
            Assert.True <| Num.isEven -0.

            // Decimals
            Assert.True <| Num.isEven 0m
            Assert.True <| Num.isEven 2m
            Assert.True <| Num.isEven 4m
            Assert.True <| Num.isEven -2m
            Assert.True <| Num.isEven -4m
            Assert.True <| Num.isEven -0m

        [<Fact>]
        let ``isEven returns false for odd values`` () =
            // Integers
            Assert.False <| Num.isEven 1
            Assert.False <| Num.isEven 3
            Assert.False <| Num.isEven 5
            Assert.False <| Num.isEven -1
            Assert.False <| Num.isEven -3
            Assert.False <| Num.isEven -5
            Assert.False <| Num.isEven Int32.MaxValue

            // Unsigned integers
            Assert.False <| Num.isEven 1u
            Assert.False <| Num.isEven 3u
            Assert.False <| Num.isEven 5u
            Assert.False <| Num.isEven UInt32.MaxValue

            // Unsigned shorts
            Assert.False <| Num.isEven 1us
            Assert.False <| Num.isEven 3us
            Assert.False <| Num.isEven 5us

            // Long integers
            Assert.False <| Num.isEven 1L
            Assert.False <| Num.isEven 3L
            Assert.False <| Num.isEven -1L
            Assert.False <| Num.isEven -3L
            Assert.False <| Num.isEven Int64.MaxValue

            // Floats
            Assert.False <| Num.isEven 1.
            Assert.False <| Num.isEven 3.
            Assert.False <| Num.isEven 5.
            Assert.False <| Num.isEven -1.
            Assert.False <| Num.isEven -3.

            // Decimals
            Assert.False <| Num.isEven 1m
            Assert.False <| Num.isEven 3m
            Assert.False <| Num.isEven 5m
            Assert.False <| Num.isEven -1m
            Assert.False <| Num.isEven -3m

    module IsZeroEdgeTests =

        [<Fact>]
        let ``isZero returns false for NaN`` () =
            Assert.False <| Num.isZero nan

    module AreZeroTests =

        [<Fact>]
        let ``areZero returns true when all values are zero`` () =
            Assert.True <| Num.areZero [ 0; 0; 0 ]
            Assert.True <| Num.areZero [ 0.; -0.; 0. ]
            Assert.True <| Num.areZero [ 0m ]

        [<Fact>]
        let ``areZero returns false when any value is non-zero`` () =
            Assert.False <| Num.areZero [ 0; 1; 0 ]
            Assert.False <| Num.areZero [ 0; 0; -1 ]
            Assert.False <| Num.areZero [ 0.0000000000001 ]

        [<Fact>]
        let ``areZero returns true for an empty sequence`` () =
            Assert.True <| Num.areZero Seq.empty<int>

    module IsNonZeroTests =

        [<Fact>]
        let ``isNonZero returns true for non-zero values`` () =
            Assert.True <| Num.isNonZero 1
            Assert.True <| Num.isNonZero -1
            Assert.True <| Num.isNonZero Int64.MinValue
            Assert.True <| Num.isNonZero 0.0000000000001
            Assert.True <| Num.isNonZero 1m

        [<Fact>]
        let ``isNonZero returns false for zero values`` () =
            Assert.False <| Num.isNonZero 0
            Assert.False <| Num.isNonZero 0u
            Assert.False <| Num.isNonZero 0.
            Assert.False <| Num.isNonZero -0.
            Assert.False <| Num.isNonZero 0m

        [<Fact>]
        let ``isNonZero returns true for NaN`` () =
            // NaN is not equal to zero
            Assert.True <| Num.isNonZero nan

    module AreNonZeroTests =

        [<Fact>]
        let ``areNonZero returns true when all values are non-zero`` () =
            Assert.True <| Num.areNonZero [ 1; -1; 5 ]
            Assert.True <| Num.areNonZero [ 0.5; -0.5 ]

        [<Fact>]
        let ``areNonZero returns false when any value is zero`` () =
            Assert.False <| Num.areNonZero [ 1; 0; 5 ]
            Assert.False <| Num.areNonZero [ -0. ]

        [<Fact>]
        let ``areNonZero returns true for an empty sequence`` () =
            Assert.True <| Num.areNonZero Seq.empty<int>

    module AreOneTests =

        [<Fact>]
        let ``areOne returns true when all values are one`` () =
            Assert.True <| Num.areOne [ 1; 1; 1 ]
            Assert.True <| Num.areOne [ 1.; 1. ]
            Assert.True <| Num.areOne [ 1m ]

        [<Fact>]
        let ``areOne returns false when any value is not one`` () =
            Assert.False <| Num.areOne [ 1; 2; 1 ]
            Assert.False <| Num.areOne [ 0; 1 ]
            Assert.False <| Num.areOne [ 1.0000000000001 ]

        [<Fact>]
        let ``areOne returns true for an empty sequence`` () =
            Assert.True <| Num.areOne Seq.empty<int>

    module IsOneEdgeTests =

        [<Fact>]
        let ``isOne returns false for values that are close to one`` () =
            Assert.False <| Num.isOne 1.0000000000001
            Assert.False <| Num.isOne 0.9999999999999

        [<Fact>]
        let ``isOne returns false for negative one`` () =
            Assert.False <| Num.isOne (-1m)
            Assert.False <| Num.isOne (-1L)

    module IsLtOneTests =

        [<Fact>]
        let ``isLtOne returns true for values less than one`` () =
            Assert.True <| Num.isLtOne 0
            Assert.True <| Num.isLtOne (-5)
            Assert.True <| Num.isLtOne 0.99999
            Assert.True <| Num.isLtOne Int64.MinValue
            Assert.True <| Num.isLtOne 0.5m

        [<Fact>]
        let ``isLtOne returns false for one and values greater than one`` () =
            Assert.False <| Num.isLtOne 1
            Assert.False <| Num.isLtOne 1.
            Assert.False <| Num.isLtOne 1.00001
            Assert.False <| Num.isLtOne 1m
            Assert.False <| Num.isLtOne Int64.MaxValue

    module AreLtOneTests =

        [<Fact>]
        let ``areLtOne returns true when all values are less than one`` () =
            Assert.True <| Num.areLtOne [ 0; -1; -5 ]
            Assert.True <| Num.areLtOne [ 0.5; 0.9 ]

        [<Fact>]
        let ``areLtOne returns false when any value is one or greater`` () =
            Assert.False <| Num.areLtOne [ 0; 1; -1 ]
            Assert.False <| Num.areLtOne [ 0.5; 2. ]

        [<Fact>]
        let ``areLtOne returns true for an empty sequence`` () =
            Assert.True <| Num.areLtOne Seq.empty<int>

    module IsGtOneTests =

        [<Fact>]
        let ``isGtOne returns true for values greater than one`` () =
            Assert.True <| Num.isGtOne 2
            Assert.True <| Num.isGtOne 1.00001
            Assert.True <| Num.isGtOne 1.00000001m
            Assert.True <| Num.isGtOne Int64.MaxValue

        [<Fact>]
        let ``isGtOne returns false for one and values less than one`` () =
            Assert.False <| Num.isGtOne 1
            Assert.False <| Num.isGtOne 1m
            Assert.False <| Num.isGtOne 0
            Assert.False <| Num.isGtOne (-1)
            Assert.False <| Num.isGtOne 0.99999

    module AreGtOneTests =

        [<Fact>]
        let ``areGtOne returns true when all values are greater than one`` () =
            Assert.True <| Num.areGtOne [ 2; 3; 4 ]
            Assert.True <| Num.areGtOne [ 1.5; 2.5 ]

        [<Fact>]
        let ``areGtOne returns false when any value is one or less`` () =
            Assert.False <| Num.areGtOne [ 2; 1; 3 ]
            Assert.False <| Num.areGtOne [ 2.; 0. ]

        [<Fact>]
        let ``areGtOne returns true for an empty sequence`` () =
            Assert.True <| Num.areGtOne Seq.empty<int>

    module IsNegTests =

        [<Fact>]
        let ``isNeg returns true for negative values`` () =
            Assert.True <| Num.isNeg (-1)
            Assert.True <| Num.isNeg Int32.MinValue
            Assert.True <| Num.isNeg Int64.MinValue
            Assert.True <| Num.isNeg (-0.0000001)
            Assert.True <| Num.isNeg (-1m)

        [<Fact>]
        let ``isNeg returns false for zero, positive values, and NaN`` () =
            Assert.False <| Num.isNeg 0
            Assert.False <| Num.isNeg -0.
            Assert.False <| Num.isNeg 0m
            Assert.False <| Num.isNeg 1
            Assert.False <| Num.isNeg Int32.MaxValue
            Assert.False <| Num.isNeg nan

    module AreNegTests =

        [<Fact>]
        let ``areNeg returns true when all values are negative`` () =
            Assert.True <| Num.areNeg [ -1; -2; Int32.MinValue ]
            Assert.True <| Num.areNeg [ -0.5; -3. ]

        [<Fact>]
        let ``areNeg returns false when any value is zero or positive`` () =
            Assert.False <| Num.areNeg [ -1; 0; -2 ]
            Assert.False <| Num.areNeg [ -1; 1 ]

        [<Fact>]
        let ``areNeg returns true for an empty sequence`` () =
            Assert.True <| Num.areNeg Seq.empty<int>

    module IsPosTests =

        [<Fact>]
        let ``isPos returns true for positive values`` () =
            Assert.True <| Num.isPos 1
            Assert.True <| Num.isPos Int32.MaxValue
            Assert.True <| Num.isPos 0.0000001
            Assert.True <| Num.isPos 1m
            Assert.True <| Num.isPos 1u

        [<Fact>]
        let ``isPos returns false for zero, negative values, and NaN`` () =
            Assert.False <| Num.isPos 0
            Assert.False <| Num.isPos 0u
            Assert.False <| Num.isPos -0.
            Assert.False <| Num.isPos 0m
            Assert.False <| Num.isPos (-1)
            Assert.False <| Num.isPos nan

    module ArePosTests =

        [<Fact>]
        let ``arePos returns true when all values are positive`` () =
            Assert.True <| Num.arePos [ 1; 2; Int32.MaxValue ]
            Assert.True <| Num.arePos [ 0.1; 3. ]

        [<Fact>]
        let ``arePos returns false when any value is zero or negative`` () =
            Assert.False <| Num.arePos [ 1; 0; 2 ]
            Assert.False <| Num.arePos [ 1; -1 ]

        [<Fact>]
        let ``arePos returns true for an empty sequence`` () =
            Assert.True <| Num.arePos Seq.empty<int>

    module IsZeroOrPosTests =

        [<Fact>]
        let ``isZeroOrPos returns true for zero and positive values`` () =
            Assert.True <| Num.isZeroOrPos 0
            Assert.True <| Num.isZeroOrPos 0.
            Assert.True <| Num.isZeroOrPos -0.
            Assert.True <| Num.isZeroOrPos 0m
            Assert.True <| Num.isZeroOrPos 1
            Assert.True <| Num.isZeroOrPos Int64.MaxValue

        [<Fact>]
        let ``isZeroOrPos returns false for negative values and NaN`` () =
            Assert.False <| Num.isZeroOrPos (-1)
            Assert.False <| Num.isZeroOrPos Int32.MinValue
            Assert.False <| Num.isZeroOrPos (-0.0000001)
            Assert.False <| Num.isZeroOrPos (-1m)
            Assert.False <| Num.isZeroOrPos nan

    module AreZeroOrPosTests =

        [<Fact>]
        let ``areZeroOrPos returns true when all values are zero or positive`` () =
            Assert.True <| Num.areZeroOrPos [ 0; 1; 2 ]
            Assert.True <| Num.areZeroOrPos [ 0.; 0.5 ]

        [<Fact>]
        let ``areZeroOrPos returns false when any value is negative`` () =
            Assert.False <| Num.areZeroOrPos [ 0; -1 ]
            Assert.False <| Num.areZeroOrPos [ 1.0; -0.5 ]

        [<Fact>]
        let ``areZeroOrPos returns true for an empty sequence`` () =
            Assert.True <| Num.areZeroOrPos Seq.empty<int>

    module IsZeroOrNegTests =

        [<Fact>]
        let ``isZeroOrNeg returns true for zero and negative values`` () =
            Assert.True <| Num.isZeroOrNeg 0
            Assert.True <| Num.isZeroOrNeg -0.
            Assert.True <| Num.isZeroOrNeg 0m
            Assert.True <| Num.isZeroOrNeg (-1)
            Assert.True <| Num.isZeroOrNeg Int64.MinValue
            Assert.True <| Num.isZeroOrNeg (-1m)

        [<Fact>]
        let ``isZeroOrNeg returns false for positive values and NaN`` () =
            Assert.False <| Num.isZeroOrNeg 1
            Assert.False <| Num.isZeroOrNeg 0.0000001
            Assert.False <| Num.isZeroOrNeg 1m
            Assert.False <| Num.isZeroOrNeg 1u
            Assert.False <| Num.isZeroOrNeg nan

    module AreZeroOrNegTests =

        [<Fact>]
        let ``areZeroOrNeg returns true when all values are zero or negative`` () =
            Assert.True <| Num.areZeroOrNeg [ 0; -1; -2 ]
            Assert.True <| Num.areZeroOrNeg [ -0.5; 0. ]

        [<Fact>]
        let ``areZeroOrNeg returns false when any value is positive`` () =
            Assert.False <| Num.areZeroOrNeg [ 0; 1; -1 ]
            Assert.False <| Num.areZeroOrNeg [ -1.; 0.5 ]

        [<Fact>]
        let ``areZeroOrNeg returns true for an empty sequence`` () =
            Assert.True <| Num.areZeroOrNeg Seq.empty<int>

    module IsOddTests =

        [<Fact>]
        let ``isOdd returns true for odd integers of every type`` () =
            Assert.True <| Num.isOdd 1
            Assert.True <| Num.isOdd (-1)
            Assert.True <| Num.isOdd (-3)
            Assert.True <| Num.isOdd Int32.MaxValue
            Assert.True <| Num.isOdd 3u
            Assert.True <| Num.isOdd 1us
            Assert.True <| Num.isOdd 1L
            Assert.True <| Num.isOdd (Int64.MinValue + 1L)
            Assert.True <| Num.isOdd 1.
            Assert.True <| Num.isOdd (-3.)
            Assert.True <| Num.isOdd 1m

        [<Fact>]
        let ``isOdd returns false for even integers of every type`` () =
            Assert.False <| Num.isOdd 0
            Assert.False <| Num.isOdd 2
            Assert.False <| Num.isOdd (-2)
            Assert.False <| Num.isOdd Int32.MinValue
            Assert.False <| Num.isOdd 0u
            Assert.False <| Num.isOdd 2us
            Assert.False <| Num.isOdd 0L
            Assert.False <| Num.isOdd 0.
            Assert.False <| Num.isOdd 2m

    module AreOddTests =

        [<Fact>]
        let ``areOdd returns true when all values are odd`` () =
            Assert.True <| Num.areOdd [ 1; -3; 5 ]
            Assert.True <| Num.areOdd [ 1L; 3L ]

        [<Fact>]
        let ``areOdd returns false when any value is even`` () =
            Assert.False <| Num.areOdd [ 1; 2; 3 ]
            Assert.False <| Num.areOdd [ 0 ]

        [<Fact>]
        let ``areOdd returns true for an empty sequence`` () =
            Assert.True <| Num.areOdd Seq.empty<int>

    module IsEvenEdgeTests =

        [<Fact>]
        let ``isEven returns false for non-integer floats`` () =
            // Non-integer values have a fractional remainder, so they are not even
            Assert.False <| Num.isEven 0.5
            Assert.False <| Num.isEven 2.5

        [<Fact>]
        let ``isEven returns false for NaN`` () =
            Assert.False <| Num.isEven nan

    module AreEvenTests =

        [<Fact>]
        let ``areEven returns true when all values are even`` () =
            Assert.True <| Num.areEven [ 0; 2; -4 ]
            Assert.True <| Num.areEven [ 0.; -2. ]

        [<Fact>]
        let ``areEven returns false when any value is odd`` () =
            Assert.False <| Num.areEven [ 0; 1 ]
            Assert.False <| Num.areEven [ 2; -3; 4 ]

        [<Fact>]
        let ``areEven returns true for an empty sequence`` () =
            Assert.True <| Num.areEven Seq.empty<int>

    module IsInRangeTests =

        [<Fact>]
        let ``isInRange returns true for values inside the range`` () =
            Assert.True <| Num.isInRange 0 10 5
            Assert.True <| Num.isInRange (-10) (-5) (-7)
            Assert.True <| Num.isInRange 0. 1. 0.5
            Assert.True <| Num.isInRange 0m 1m 0.5m

        [<Fact>]
        let ``isInRange is inclusive of both bounds`` () =
            Assert.True <| Num.isInRange 0 10 0
            Assert.True <| Num.isInRange 0 10 10
            Assert.True <| Num.isInRange 5 5 5

        [<Fact>]
        let ``isInRange returns false for values outside the range`` () =
            Assert.False <| Num.isInRange 0 10 (-1)
            Assert.False <| Num.isInRange 0 10 11
            Assert.False <| Num.isInRange 5 5 4
            Assert.False <| Num.isInRange 0. 1. 1.0000001
            Assert.False <| Num.isInRange 0 10 Int32.MaxValue

        [<Fact>]
        let ``isInRange works with partial application in a pipeline`` () =
            let result =
                [ -1; 0; 5; 10; 11 ]
                |> Seq.filter (Num.isInRange 0 10)
                |> Seq.toList

            Assert.Equal<int list>([ 0; 5; 10 ], result)

    module AreInRangeTests =

        [<Fact>]
        let ``areInRange returns true when all values are inside the range`` () =
            Assert.True <| Num.areInRange 0 10 [ 0; 5; 10 ]
            Assert.True <| Num.areInRange 0. 1. [ 0.25; 0.75 ]

        [<Fact>]
        let ``areInRange returns false when any value is outside the range`` () =
            Assert.False <| Num.areInRange 0 10 [ 0; 5; 11 ]
            Assert.False <| Num.areInRange 0 10 [ -1; 5 ]

        [<Fact>]
        let ``areInRange returns true for an empty sequence`` () =
            Assert.True <| Num.areInRange 0 10 Seq.empty<int>

    module IsPowerOfTwoTests =

        [<Fact>]
        let ``isPowerOfTwo returns true for powers of two`` () =
            Assert.True <| Num.isPowerOfTwo 1L
            Assert.True <| Num.isPowerOfTwo 2L
            Assert.True <| Num.isPowerOfTwo 4L
            Assert.True <| Num.isPowerOfTwo 1024L
            Assert.True <| Num.isPowerOfTwo (1L <<< 62)

        [<Fact>]
        let ``isPowerOfTwo returns false for zero, negative values, and non-powers`` () =
            Assert.False <| Num.isPowerOfTwo 0L
            Assert.False <| Num.isPowerOfTwo 3L
            Assert.False <| Num.isPowerOfTwo 6L
            Assert.False <| Num.isPowerOfTwo 1023L
            Assert.False <| Num.isPowerOfTwo (-1L)
            Assert.False <| Num.isPowerOfTwo (-2L)
            Assert.False <| Num.isPowerOfTwo Int64.MinValue
            Assert.False <| Num.isPowerOfTwo Int64.MaxValue

    module IsWholeTests =

        [<Fact>]
        let ``isWhole returns true for floats with no fractional part`` () =
            Assert.True <| Num.isWhole 0.
            Assert.True <| Num.isWhole 1.
            Assert.True <| Num.isWhole (-3.)
            Assert.True <| Num.isWhole -0.
            Assert.True <| Num.isWhole 1e15

        [<Fact>]
        let ``isWhole returns false for floats with a fractional part and NaN`` () =
            Assert.False <| Num.isWhole 0.5
            Assert.False <| Num.isWhole (-0.5)
            Assert.False <| Num.isWhole 1.0000001
            Assert.False <| Num.isWhole nan

    module WholeNumberParityTests =

        [<Fact>]
        let ``isEven and isOdd return false for non-whole floats`` () =
            Assert.False <| Num.isEven 0.5
            Assert.False <| Num.isEven 2.5
            Assert.False <| Num.isEven (-2.5)
            Assert.False <| Num.isOdd 1.5
            Assert.False <| Num.isOdd 0.5
            Assert.False <| Num.isOdd (-2.5)

        [<Fact>]
        let ``isEven and isOdd return false for non-whole decimals`` () =
            Assert.False <| Num.isEven 2.5m
            Assert.False <| Num.isOdd 1.5m
            Assert.False <| Num.isOdd (-0.5m)

        [<Fact>]
        let ``isEven and isOdd return false for NaN`` () =
            Assert.False <| Num.isEven nan
            Assert.False <| Num.isOdd nan

        [<Fact>]
        let ``isWhole returns true for integer types`` () =
            Assert.True <| Num.isWhole 5
            Assert.True <| Num.isWhole Int64.MinValue
            Assert.True <| Num.isWhole 3u
            Assert.True <| Num.isWhole 2m

    module ClampTests =

        [<Fact>]
        let ``clamp returns the value unchanged when it is within the bounds`` () =
            Assert.Equal(5, Num.clamp 0 10 5)
            Assert.Equal(0.5, Num.clamp 0. 1. 0.5)
            Assert.Equal(1.5m, Num.clamp 0m 2m 1.5m)

        [<Fact>]
        let ``clamp returns the lower bound when the value is below it`` () =
            Assert.Equal(0, Num.clamp 0 10 (-5))
            Assert.Equal(-10, Num.clamp (-10) (-5) (-20))

        [<Fact>]
        let ``clamp returns the upper bound when the value is above it`` () =
            Assert.Equal(10, Num.clamp 0 10 15)
            Assert.Equal(-5, Num.clamp (-10) (-5) 0)
            Assert.Equal(1m, Num.clamp 0m 1m 1.5m)

        [<Fact>]
        let ``clamp returns the bound itself when the value equals a bound`` () =
            Assert.Equal(0, Num.clamp 0 10 0)
            Assert.Equal(10, Num.clamp 0 10 10)

        [<Fact>]
        let ``clamp returns the single bound when lo equals hi`` () =
            Assert.Equal(5, Num.clamp 5 5 100)
            Assert.Equal(5, Num.clamp 5 5 (-100))
            Assert.Equal(5, Num.clamp 5 5 5)

        [<Fact>]
        let ``clamp raises ArgumentException when lo is greater than hi`` () =
            let ex =
                Assert.Throws<ArgumentException>(fun () -> Num.clamp 10 0 5 |> ignore)

            Assert.Equal("lo", ex.ParamName)

    module SignOfTests =

        [<Fact>]
        let ``signOf returns 1 for positive values`` () =
            Assert.Equal(1, Num.signOf 5)
            Assert.Equal(1, Num.signOf Int32.MaxValue)
            Assert.Equal(1L, Num.signOf Int64.MaxValue)
            Assert.Equal(1., Num.signOf 2.5)
            Assert.Equal(1m, Num.signOf 3m)
            Assert.Equal(1u, Num.signOf 7u)

        [<Fact>]
        let ``signOf returns -1 for negative values`` () =
            Assert.Equal(-1, Num.signOf (-5))
            Assert.Equal(-1, Num.signOf Int32.MinValue)
            Assert.Equal(-1L, Num.signOf Int64.MinValue)
            Assert.Equal(-1., Num.signOf (-0.001))
            Assert.Equal(-1m, Num.signOf (-3m))

        [<Fact>]
        let ``signOf returns 0 for zero`` () =
            Assert.Equal(0, Num.signOf 0)
            Assert.Equal(0., Num.signOf -0.)
            Assert.Equal(0m, Num.signOf 0m)
            Assert.Equal(0u, Num.signOf 0u)

        [<Fact>]
        let ``signOf raises ArgumentException for NaN`` () =
            // Requires the NaN guard from the earlier suggestion
            Assert.Throws<ArgumentException>(fun () -> Num.signOf nan |> ignore) |> ignore
