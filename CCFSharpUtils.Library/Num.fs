namespace CCFSharpUtils.Library

open System
open System.Globalization

[<RequireQualifiedAccess>]
module Num =

    let inline isZero (n: ^a) =
        n = LanguagePrimitives.GenericZero<'a>

    let inline isOne (n: ^a) =
        n = LanguagePrimitives.GenericOne<'a>
