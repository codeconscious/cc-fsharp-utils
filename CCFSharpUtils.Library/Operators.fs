namespace CCFSharpUtils.Library

open FsToolkit.ErrorHandling

[<AutoOpen>]
module Operators =

    /// Operator for Result.mapError.
    let inline (|!)
        (r: Result<'ok, 'e1>)
        ([<InlineIfLambda>] f: 'e1 -> 'e2)
        : Result<'ok, 'e2> =

        Result.mapError f r

    /// Operator for Result.tee from FsToolkit.ErrorHandling.
    let inline (|.)
        (result: Result<'ok, 'error>)
        ([<InlineIfLambda>] sideEffect: 'ok -> unit)
        : Result<'ok, 'error> =

        Result.tee sideEffect result
