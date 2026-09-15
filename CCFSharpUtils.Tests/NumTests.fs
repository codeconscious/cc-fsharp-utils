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
            Assert.False <| Num.isOne -1
            Assert.False <| Num.isOne Int64.MinValue
            Assert.False <| Num.isOne Int64.MaxValue
            Assert.False <| Num.isOne 2
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
