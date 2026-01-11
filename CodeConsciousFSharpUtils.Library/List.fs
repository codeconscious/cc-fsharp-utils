namespace CodeConsciousFSharpUtils.Library

open System

[<RequireQualifiedAccess>]
module List =

    let isNotEmpty lst = not (List.isEmpty lst)

    let doesNotContain x lst = not <| List.contains x lst

    let hasOne lst = lst |> List.length |> Num.isOne

    let hasMultiple lst = lst |> List.length |> (<) 1

    let containsIgnoreCase text (lst: string list) : bool =
        lst |> List.exists (fun x -> String.Equals(x, text, StringComparison.OrdinalIgnoreCase))

    let anyContainsIgnoreCase txt (lists: string list list) =
        lists |> List.exists (containsIgnoreCase txt)
