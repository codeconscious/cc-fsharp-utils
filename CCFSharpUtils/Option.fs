namespace CCFSharpUtils

[<RequireQualifiedAccess>]
module Option =

    /// Shorthand for Option.map with Option.defaultValue.
    let mapElse (mapping: 'a -> 'b) (alt: 'b) (opt: 'a option) : 'b =
        opt
        |> Option.map mapping
        |> Option.defaultValue alt

    /// Gives a number wrapped in Some if it is less than zero. Otherwise, None.
    let inline isNeg (n: ^a) : ^a option when ^a : comparison and ^a : (static member get_Zero : unit -> ^a) =
        if Num.isNeg n then Some n else None

    /// Gives a number wrapped in Some if it is greater than zero. Otherwise, None.
    let inline isPos (n: ^a) : ^a option when ^a : comparison and ^a : (static member get_Zero : unit -> ^a) =
        if Num.isPos n then Some n else None
