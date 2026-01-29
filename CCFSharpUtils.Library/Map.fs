namespace CCFSharpUtils.Library

[<RequireQualifiedAccess>]
module Map =

    /// Lookup a key in the map, returning the corresponding value if found,
    /// or else a default value.
    let tryFindElse key alt map =
        map
        |> Map.tryFind key
        |> Option.defaultValue alt
