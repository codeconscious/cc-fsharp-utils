namespace CCFSharpUtils.Operators

open System
open FsToolkit.ErrorHandling

[<AutoOpen>]
module Operators =

    /// Operator for `Result.map`.
    [<Obsolete("This operator seems superfluous as it seems identical to F#+'s `|>>` operator.")>]
    let inline (|*)
        (r: Result<'ok1, 'err>)
        ([<InlineIfLambda>] f: 'ok1 -> 'ok2)
        : Result<'ok2, 'err> =

        Result.map f r

    /// Operator for `Result.mapError`.
    /// [<Obsolete("This is being phased out for `|!!` instead.")>]
    let inline (|!)
        (r: Result<'ok, 'err1>)
        ([<InlineIfLambda>] f: 'err1 -> 'err2)
        : Result<'ok, 'err2> =

        Result.mapError f r

    /// Operator for `Result.mapError`.
    let inline (|!!)
        (r: Result<'ok, 'err1>)
        ([<InlineIfLambda>] f: 'err1 -> 'err2)
        : Result<'ok, 'err2> =

        Result.mapError f r

    /// Operator for `Result.tee` from FsToolkit.ErrorHandling.
    [<Obsolete("I will use `|--` in the future.")>]
    let inline (|.)
        (result: Result<'ok, 'err>)
        ([<InlineIfLambda>] sideEffect: 'ok -> unit)
        : Result<'ok, 'err> =

        Result.tee sideEffect result

    /// Operator for `Result.tee` from FsToolkit.ErrorHandling.
    let inline (|--)
        (result: Result<'ok, 'err>)
        ([<InlineIfLambda>] sideEffect: 'ok -> unit)
        : Result<'ok, 'err> =

        Result.tee sideEffect result
