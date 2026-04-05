namespace CCFSharpUtils

open FSharpPlus.Data

[<AutoOpen>]
module Common =

    type Ordering = LT | EQ | GT

    /// Helper for try/with -> Result.
    let ofTry (f: unit -> 'a) : Result<'a, string> =
        try Ok (f())
        with exn -> Error exn.Message

    /// Helper for try/with -> Validation.
    let ofTry' (f: unit -> 'a) : Validation<string list, 'a> =
        try Success (f())
        with exn -> Failure [exn.Message]

    let compareWith (target: 'a) (x: 'a) : Ordering =
        match compare x target with
        | n when n < 0 -> LT
        | 0 -> EQ
        | _ -> GT
