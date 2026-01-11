namespace CCFSharpUtils.Library

open System
open System.Globalization

[<RequireQualifiedAccess>]
module Num =

    let inline isZero (n: ^a) =
        n = LanguagePrimitives.GenericZero<'a>

    let inline isOne (n: ^a) =
        n = LanguagePrimitives.GenericOne<'a>

    /// Formats a number of any type to a comma-formatted string.
    let inline formatNumber (i: ^T) : string
        when ^T : (member ToString : string * IFormatProvider -> string) =
        (^T : (member ToString : string * IFormatProvider -> string) (i, "#,##0", CultureInfo.InvariantCulture))
