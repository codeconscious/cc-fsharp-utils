namespace CCFSharpUtils

[<RequireQualifiedAccess>]
module Num =

    /// Returns true if the value is 0; otherwise, false.
    let inline isZero (n: ^a) =
        n = LanguagePrimitives.GenericZero<'a>

    let inline private allNonEmpty ([<InlineIfLambda>] pred: ^a -> bool) (ns: ^a seq) =
        use e = ns.GetEnumerator()
        if not (e.MoveNext()) then
            false
        else
            let mutable ok = pred e.Current
            while ok && e.MoveNext() do
                ok <- pred e.Current
            ok

    /// Returns true if every value is 0 or the collection is empty; otherwise, false.
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

    /// Returns true if every value is less than 0 or if the collection is empty; otherwise, false.
    let inline areNeg (ns: ^a seq) =
        Seq.forall isNeg ns

    /// Returns true if the value is greater than 0; otherwise, false.
    let inline isPos (n: ^a) =
        n > LanguagePrimitives.GenericZero<'a>

    /// Returns true if every value is greater than 0 or if the collection is empty; otherwise, false.
    let inline arePos (ns: ^a seq) =
        Seq.forall isPos ns

    /// Returns true if the value is 0 or greater; otherwise, false.
    let inline isZeroOrPos (n: ^a) =
        n >= LanguagePrimitives.GenericZero<'a>

    /// Returns true if every value is 0 or greater or if the collection is empty; otherwise, false.
    let inline areZeroOrPos (ns: ^a seq) =
        Seq.forall isZeroOrPos ns

    /// Returns true if the value is less than or equal to 0; otherwise, false.
    let inline isZeroOrNeg (n: ^a) =
        isZero n || isNeg n

    /// Returns true if every value is less than or equal to 0 or if the collection is empty;
    /// otherwise, false.
    let inline areZeroOrNeg (ns: ^a seq) =
        Seq.forall isZeroOrNeg ns

    /// Returns true if the value has no fractional part; otherwise, false.
    let inline isWhole (n: ^a) =
        let one = LanguagePrimitives.GenericOne<'a>
        let zero = LanguagePrimitives.GenericZero<'a>
        (n % one) = zero

    /// Returns true if the value is even and whole; otherwise, false.
    let inline isEven (n: ^a) =
        let two = LanguagePrimitives.GenericOne<'a> + LanguagePrimitives.GenericOne<'a>
        let zero = LanguagePrimitives.GenericZero<'a>
        isWhole n && (n % two) = zero

    /// Returns true if every value is even or if the collection is empty; otherwise, false.
    let inline areEven (ns: ^a seq) =
        Seq.forall isEven ns

    /// Returns true if the value is odd and whole; otherwise, false.
    let inline isOdd (n: ^a) =
        isWhole n && not (isEven n)

    /// Returns true if every value is odd or if the collection is empty; otherwise, false.
    let inline areOdd (ns: ^a seq) =
        Seq.forall isOdd ns

    /// Returns true if the value is within [lo, hi] inclusive; otherwise, false.
    let inline isInRange (lo: ^a) (hi: ^a) (n: ^a) =
        n >= lo && n <= hi

    /// Returns true if every value is within [lo, hi] inclusive or for an empty collection;
    /// otherwise, false.
    let inline areInRange (lo: ^a) (hi: ^a) (ns: ^a seq) =
        Seq.forall (isInRange lo hi) ns

    /// Returns true if the value is a power of two; otherwise, false.
    let isPowerOfTwo (n: int64) =
        n > 0L && (n &&& (n - 1L)) = 0L

    /// Clamps the value to the range [lo, hi]. Raises if lo > hi.
    let inline clamp (lo: ^a) (hi: ^a) (n: ^a) =
        if lo > hi then
            invalidArg "lo" "lo must be less than or equal to hi."
        elif n < lo then lo
        elif n > hi then hi
        else n

    /// Returns -1, 0, or 1 according to the sign of the value.
    /// Raises ArgumentException for NaN.
    /// Named signOf to avoid shadowing the built-in sign function.
    let inline signOf (n: ^a) : ^a =
        if n <> n then invalidArg "n" "n must not be NaN."
        elif isZero n then LanguagePrimitives.GenericZero<'a>
        elif isNeg n then LanguagePrimitives.GenericZero<'a> - LanguagePrimitives.GenericOne<'a>
        else LanguagePrimitives.GenericOne<'a>
