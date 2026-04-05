namespace CCFSharpUtils

[<RequireQualifiedAccess>]
module Num =

    let inline isZero (n: ^a) =
        n = LanguagePrimitives.GenericZero<'a>

    let inline isNonZero (n: ^a) =
        not <| isZero n

    let inline isOne (n: ^a) =
        n = LanguagePrimitives.GenericOne<'a>

    /// If the value is less than 0, returns true; otherwise, false.
    let inline isNeg (n: ^a) =
        n < LanguagePrimitives.GenericZero<'a>

    /// If the value is greater than 0, returns true; otherwise, false.
    let inline isPos (n: ^a) =
        n > LanguagePrimitives.GenericZero<'a>

    /// If the value is less than or equal to 0, returns true; otherwise, false.
    let inline isZeroOrNeg (n: ^a) =
        isZero n || isNeg n
