namespace CCFSharpUtils.Library

[<AutoOpen>]
module Common =

    /// Helper for try/with -> Result.
    let ofTry (f: unit -> 'a) : Result<'a, string> =
        try Ok (f())
        with exn -> Error exn.Message

    /// Execute side effects using the given function, then returns the value unmodified.
    let inline tee fn x = x |> fn |> ignore; x

    type Ordering = LT | EQ | GT

    let compareWith target x =
        match compare x target with
        | n when n < 0 -> LT
        | 0 -> EQ
        | _ -> GT
