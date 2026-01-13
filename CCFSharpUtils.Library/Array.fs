namespace CCFSharpUtils.Library

open System

[<RequireQualifiedAccess>]
module Array =

    let isNotEmpty arr = not <| Array.isEmpty arr

    let anyNotEmpty arrs = arrs |> Array.exists isNotEmpty

    let allNotEmpty arrs = arrs |> Array.forall isNotEmpty

    let doesNotContain x arr = not <| Array.contains x arr

    let headElse alt arr =
        arr |> Array.tryHead |> Option.defaultValue alt

    let hasOne arr = arr |> Array.length |> Num.isOne

    let hasMultiple arr = arr |> Array.length |> (<) 1

    let containsIgnoreCase text (arr: string array) : bool =
        arr |> Array.exists (fun x -> String.Equals(x, text, StringComparison.OrdinalIgnoreCase))

    let anyContainsIgnoreCase text (arrays: string array array) =
        arrays |> Array.exists (containsIgnoreCase text)

    /// If the array is empty, returns None. Otherwise, wraps the array in Some.
    let toOption arr = if Array.isEmpty arr then None else Some arr
