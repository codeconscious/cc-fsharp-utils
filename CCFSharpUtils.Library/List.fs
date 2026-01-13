namespace CCFSharpUtils.Library

open System

[<RequireQualifiedAccess>]
module List =

    let isNotEmpty lst = not <| List.isEmpty lst

    let anyNotEmpty lsts = lsts |> List.exists isNotEmpty

    let allNotEmpty lsts = lsts |> List.forall isNotEmpty

    let doesNotContain x = not << List.contains x

    let headElse alt =
        List.tryHead >> Option.defaultValue alt

    let hasOne lst = lst |> List.length |> Num.isOne

    let hasMultiple lst = lst |> List.length |> (<) 1

    let containsIgnoreCase text (lst: string list) : bool =
        lst |> List.exists (fun x -> String.Equals(x, text, StringComparison.OrdinalIgnoreCase))

    let anyContainsIgnoreCase txt =
        List.exists (containsIgnoreCase txt)

    /// If the list is empty, returns None. Otherwise, wraps the list in Some.
    let toOption lst = if List.isEmpty lst then None else Some lst
