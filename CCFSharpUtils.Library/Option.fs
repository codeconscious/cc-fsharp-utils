namespace CCFSharpUtils.Library

[<RequireQualifiedAccess>]
module Option =

    // Shorthand for Option.map with Option.defaultValue.
    let mapElse (mapping: 'a -> 'b) (alt: 'b) (opt: 'a option) : 'b =
        opt
        |> Option.map mapping
        |> Option.defaultValue alt
