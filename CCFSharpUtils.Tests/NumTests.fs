namespace CCFSharpUtils.Tests

open Xunit
open CCFSharpUtils.Library
open System

module NumericsTests =

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
