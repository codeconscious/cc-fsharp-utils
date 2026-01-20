namespace CCFSharpUtils.Library

[<RequireQualifiedAccess>]
module Option =

    // Shorthand for Option.map with Option.defaultValue.
    let mapElse mapping alt opt =
        opt |> Option.map mapping |> Option.defaultValue alt
