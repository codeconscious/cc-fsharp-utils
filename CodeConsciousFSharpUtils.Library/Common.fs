namespace CodeConsciousFSharpUtils.Library

[<AutoOpen>]
module Common =

    /// Helper for try/with -> Result.
    let ofTry (f: unit -> 'a) : Result<'a, string> =
        try Ok (f())
        with exn -> Error exn.Message

    /// Execute side effects using the given function, then returns the value unmodified.
    let inline tee fn x = x |> fn |> ignore; x

