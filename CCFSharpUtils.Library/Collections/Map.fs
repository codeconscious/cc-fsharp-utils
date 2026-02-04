namespace CCFSharpUtils.Library

open FSharpPlus.Data

[<RequireQualifiedAccess>]
module Map =

    /// Lookup a key in the map, returning the corresponding value if found,
    /// or else a default value.
    let tryFindElse key alt map =
        map
        |> Map.tryFind key
        |> Option.defaultValue alt

    /// If the map is empty, returns the specified Error.
    /// Otherwise, converts it to a NonEmptyMap wrapped in Ok.
    let toNonEmptyMapResult err m =
        if Map.isEmpty m
        then Error err
        else Ok (NonEmptyMap.ofMap m)
