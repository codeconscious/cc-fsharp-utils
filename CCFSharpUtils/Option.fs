namespace CCFSharpUtils

[<RequireQualifiedAccess>]
module Option =

    /// Shorthand for Option.map with Option.defaultValue.
    let mapElse (mapping: 'a -> 'b) (alt: 'b) (opt: 'a option) : 'b =
        opt
        |> Option.map mapping
        |> Option.defaultValue alt

    /// Gives an int wrapped in Some if it is less than zero. Otherwise, None.
    let inline isNeg n : int option =
        if Num.isNeg n then Some n else None

    /// Gives an int wrapped in Some if it is greater than zero. Otherwise, None.
    let inline isPos n : int option =
        if Num.isPos n then Some n else None
