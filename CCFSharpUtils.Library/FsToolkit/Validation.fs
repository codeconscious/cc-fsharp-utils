namespace CCFSharpUtils.Library

open FsToolkit.ErrorHandling

/// Functions pertaining to applicative validations.
[<RequireQualifiedAccess>]
module Validation =

    /// Converts a Validation to a Result.
    let validationToResult (customError: (string list -> 'b)) (v: Validation<'a, string>) : Result<'a, 'b> =
        match v with
        | Ok x -> Ok x
        | Error errs -> errs |> customError |> Error
