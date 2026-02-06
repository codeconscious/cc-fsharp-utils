namespace CCFSharpUtils.Library

open FsToolkit.ErrorHandling

[<AutoOpen>]
module Operators =

    /// Operator for `Result.mapError`.
    let inline (|!)
        (r: Result<'ok, 'err1>)
        ([<InlineIfLambda>] f: 'err1 -> 'err2)
        : Result<'ok, 'err2> =

        Result.mapError f r

    /// Operator for `Result.tee` from FsToolkit.ErrorHandling.
    let inline (|.)
        (result: Result<'ok, 'err>)
        ([<InlineIfLambda>] sideEffect: 'ok -> unit)
        : Result<'ok, 'err> =

        Result.tee sideEffect result
