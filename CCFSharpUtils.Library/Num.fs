namespace CCFSharpUtils.Library

[<RequireQualifiedAccess>]
module Num =

    let inline isZero (n: ^a) =
        n = LanguagePrimitives.GenericZero<'a>

    let inline isNonZero (n: ^a) =
        not <| isZero n

    let inline isOne (n: ^a) =
        n = LanguagePrimitives.GenericOne<'a>

    let inline isNeg (n: ^a) =
        n < LanguagePrimitives.GenericZero<'a>

    let inline isPos (n: ^a) =
        n > LanguagePrimitives.GenericZero<'a>
