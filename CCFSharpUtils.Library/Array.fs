namespace CCFSharpUtils.Library

open FSharpPlus.Data
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

    let takeLast count (arr: 'a array) =
        if isNull arr then
            nullArg (nameof arr)
        elif count <= 0 then
            Array.empty
        else
            let length = arr.Length
            if count >= length
            then arr
            else Array.sub arr (length - count) count

    let hasOne arr =
        arr |> Array.length |> Num.isOne

    let hasMultiple arr =
        arr |> Array.length |> (<) 1

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

    /// If the array is empty, returns the specified Error.
    /// Otherwise, converts it to a NonEmptyList wrapped in Ok.
    let toNonEmptyListResult err = function
        | [||] -> Error err
        | arr  -> Ok (NonEmptyList.ofArray arr)

    /// If the array is empty, returns the specified Error.
    /// Otherwise, converts it to a NonEmptySeq wrapped in Ok.
    let toNonEmptySeqResult err = function
        | [||] -> Error err
        | arr  -> Ok (NonEmptySeq.ofArray arr)

    /// If the array is empty, returns the specified Error.
    /// Otherwise, converts it to a NonEmptySet wrapped in Ok.
    let toNonEmptySetResult err = function
        | [||] -> Error err
        | arr  -> Ok (NonEmptySet.ofArray arr)
