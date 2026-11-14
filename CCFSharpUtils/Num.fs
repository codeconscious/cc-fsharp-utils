namespace CCFSharpUtils

[<RequireQualifiedAccess>]
module Num =

    /// Returns true if the value is 0; otherwise, false.
    let inline isZero (n: ^a) =
        n = LanguagePrimitives.GenericZero<'a>

    /// Returns true if every value is 0; otherwise, false.
    let inline areZero (ns: ^a seq) =
        Seq.forall isZero ns

    /// Returns true if the value is not 0; otherwise, false.
    let inline isNonZero (n: ^a) =
        not <| isZero n

    /// Returns true if every value is not 0; otherwise, false.
    let inline areNonZero (ns: ^a seq) =
        Seq.forall isNonZero ns

    /// Returns true if the value is 1; otherwise, false.
    let inline isOne (n: ^a) =
        n = LanguagePrimitives.GenericOne<'a>

    /// Returns true if every value is 1; otherwise, false.
    let inline areOne (ns: ^a seq) =
        Seq.forall isOne ns

    /// Returns true if value is less than 1; otherwise, false.
    let inline isLtOne (n: ^a) =
        n < LanguagePrimitives.GenericOne<'a>

    /// Returns true if every value is less than 1; otherwise, false.
    let inline areLtOne (ns: ^a seq) =
        Seq.forall isLtOne ns

    /// Returns true if value is greater than 1; otherwise, false.
    let inline isGtOne (n: ^a) =
        n > LanguagePrimitives.GenericOne<'a>

    /// Returns true if every value is greater than 1; otherwise, false.
    let inline areGtOne (ns: ^a seq) =
        Seq.forall isGtOne ns

    /// Returns true if the value is less than 0; otherwise, false.
    let inline isNeg (n: ^a) =
        n < LanguagePrimitives.GenericZero<'a>

    /// Returns true if every value is less than 0; otherwise, false.
    let inline areNeg (ns: ^a seq) =
        Seq.forall isNeg ns

    /// Returns true if the value is greater than 0; otherwise, false.
    let inline isPos (n: ^a) =
        n > LanguagePrimitives.GenericZero<'a>

    /// Returns true if every value is greater than 0; otherwise, false.
    let inline arePos (ns: ^a seq) =
        Seq.forall isPos ns

    /// Returns true if the value is less than or equal to 0; otherwise, false.
    let inline isZeroOrNeg (n: ^a) =
        isZero n || isNeg n

    /// Returns true if every value is less than or equal to 0; otherwise, false.
    let inline areZeroOrNeg (ns: ^a seq) =
        Seq.forall isZeroOrNeg ns

    /// Returns true if the value is even; otherwise, false.
    let inline isEven (n: ^a) =
        let two = LanguagePrimitives.GenericOne<'a> + LanguagePrimitives.GenericOne<'a>
        let zero = LanguagePrimitives.GenericZero<'a>
        (n % two) = zero

    /// Returns true if every value is even; otherwise, false.
    let inline areEven (ns: ^a seq) =
        Seq.forall isEven ns

    /// Returns true if the value is odd; otherwise, false.
    let inline isOdd (n: ^a) =
        not (isEven n)

    /// Returns true if every value is odd; otherwise, false.
    let inline areOdd (ns: ^a seq) =
        Seq.forall isOdd ns
