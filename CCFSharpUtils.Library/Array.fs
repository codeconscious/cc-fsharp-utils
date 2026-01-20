namespace CCFSharpUtils.Library

open System

[<RequireQualifiedAccess>]
module Array =

    let isNotEmpty arr =
        not <| Array.isEmpty arr

    let anyNotEmpty arrays =
        arrays |> Array.exists isNotEmpty

    let allNotEmpty arrays =
        arrays |> Array.forall isNotEmpty

    let doesNotContain x =
        not << Array.contains x

    let headElse alt =
        Array.tryHead >> Option.defaultValue alt

    let takeLast count arr =
        arr
        |> Array.rev
        |> Array.truncate count
        |> Array.rev

    let hasOne arr = arr |> Array.length |> Num.isOne

    let hasMultiple arr = arr |> Array.length |> (<) 1

    let ensureOne emptyErr multipleErr arr =
        match arr with
        | [| |]   -> Error emptyErr
        | [| x |] -> Ok [x]
        | _       -> Error multipleErr

    let ensureSize targetSize tooSmallErr tooLargeErr (arr: 'b array) : Result<'b array,'a> =
        match compareWith targetSize arr.Length with
        | EQ -> Ok arr
        | LT -> Error tooSmallErr
        | GT -> Error tooLargeErr

    let tryGetSingle emptyErr multipleErr arr =
        match arr with
        | [| |]   -> Error emptyErr
        | [| x |] -> Ok x
        | _       -> Error multipleErr

    let containsIgnoreCase txt (arr: string array) : bool =
        arr |> Array.exists (fun x -> String.Equals(x, txt, StringComparison.OrdinalIgnoreCase))

    let anyContainsIgnoreCase txt =
        Array.exists (containsIgnoreCase txt)

    /// If the array is empty, returns None. Otherwise, wraps the array in Some.
    let toOption arr =
        if Array.isEmpty arr then None else Some arr

    /// If the array is empty, returns the specified Error. Otherwise, wraps the array in Ok.
    let toResult err arr =
        if Array.isEmpty arr then Error err else Ok arr
