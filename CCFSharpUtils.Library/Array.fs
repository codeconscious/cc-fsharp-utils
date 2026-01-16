namespace CCFSharpUtils.Library

open System

[<RequireQualifiedAccess>]
module Array =

    let isNotEmpty arr = not <| Array.isEmpty arr

    let anyNotEmpty arrs = arrs |> Array.exists isNotEmpty

    let allNotEmpty arrs = arrs |> Array.forall isNotEmpty

    let doesNotContain x = not << Array.contains x

    let headElse alt =
        Array.tryHead >> Option.defaultValue alt

    let hasOne arr = arr |> Array.length |> Num.isOne

    let hasMultiple arr = arr |> Array.length |> (<) 1

    let containsIgnoreCase text (arr: string array) : bool =
        arr |> Array.exists (fun x -> String.Equals(x, text, StringComparison.OrdinalIgnoreCase))

    let anyContainsIgnoreCase text =
        Array.exists (containsIgnoreCase text)

    /// If the array is empty, returns None. Otherwise, wraps the array in Some.
    let toOption arr =
        if Array.isEmpty arr then None else Some arr

    /// If the array is empty, returns the specified Error. Otherwise, wraps the array in Ok.
    let toResult err arr =
        if Array.isEmpty arr then Error err else Ok arr
