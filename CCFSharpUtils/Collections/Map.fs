namespace CCFSharpUtils

open FSharpPlus.Data

[<RequireQualifiedAccess>]
module Map =

    /// Lookup a key in the map, returning the corresponding value if found,
    /// or else a default value.
    let tryFindElse (key: 'a) (alt: 'b) (map: Map<'a, 'b>) : 'b =
        map
        |> Map.tryFind key
        |> Option.defaultValue alt

    /// If the map is empty, returns the specified Error.
    /// Otherwise, converts it to a NonEmptyMap wrapped in Ok.
    let toNonEmptyMapResult (err: 'a) (m: Map<'b, 'c>) : Result<NonEmptyMap<'b,'c>,'a> =
        if Map.isEmpty m
        then Error err
        else Ok (NonEmptyMap.ofMap m)

    // Reverse the keys and values in a given map.
    let flip (map: Map<'k, 'v>) : Map<'v, 'k> =
        Map.fold
            (fun acc k v -> Map.add v k acc)
            Map.empty
            map

    // Merge two map, prioritizing the primary one in case of conflicts.
    let merge secondary primary : Map<'k, 'v> =
        Map.fold
            (fun acc k v -> Map.add k v acc)
            secondary
            primary // Takes precedence, overwriting as needed.

    /// Returns the value associated with `key` in `map`, or `fallbackValue` if the key is not present.
    let valueOrFallback (key: 'a) (fallbackValue: 'b) (map: Map<'a, 'b>) : 'b =
        match map.TryGetValue key with
        | true, foundValue -> foundValue
        | false, _ -> fallbackValue

    /// Returns the mapped value for `target` in `map`, or `target` itself if no mapping exists.
    /// The map must be homogenous (e.g., `Map<string, string>`).
    let valueOrTarget (target: 'a) (map: Map<'a, 'a>) : 'a =
        match map.TryGetValue target with
        | true, foundValue -> foundValue
        | false, _ -> target
