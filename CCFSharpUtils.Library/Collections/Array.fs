namespace CCFSharpUtils.Library

open FSharpPlus.Data
open System

[<RequireQualifiedAccess>]
module Array =

    let isNotEmpty (arr: 'a array) : bool =
        not <| Array.isEmpty arr

    let anyNotEmpty (arrays: 'a array array) : bool =
        arrays |> Array.exists isNotEmpty

    let allNotEmpty (arrays: 'a array array) : bool =
        arrays |> Array.forall isNotEmpty

    let doesNotContain (x: 'a) : 'a array -> bool =
        not << Array.contains x

    let headElse (alt: 'a) : 'a array -> 'a =
        Array.tryHead >> Option.defaultValue alt

    let takeLast (count: int) (arr: 'a array) : 'a array =
        if isNull arr then
            nullArg (nameof arr)
        elif count <= 0 then
            Array.empty
        else
            let length = arr.Length
            if count >= length
            then arr
            else Array.sub arr (length - count) count

    let hasOne (arr: 'a array) : bool =
        arr |> Array.length |> Num.isOne

    let hasMultiple (arr: 'a array) : bool =
        arr |> Array.length |> (<) 1

    let ensureOne (emptyErr: 'err) (multipleErr: 'err) (arr: 'a array) : Result<'a list, 'err> =
        match arr with
        | [| |]   -> Error emptyErr
        | [| x |] -> Ok [x]
        | _       -> Error multipleErr

    let ensureSize (targetSize: int) (tooSmallErr: 'err) (tooLargeErr: 'err) (arr: 'a array) : Result<'a array, 'err> =
        if Num.isNeg targetSize then
            invalidArg (nameof targetSize) "Target size cannot be negative."

        match compareWith targetSize arr.Length with
        | EQ -> Ok arr
        | LT -> Error tooSmallErr
        | GT -> Error tooLargeErr

    let tryGetSingle (emptyErr: 'err) (multipleErr: 'err) (arr: 'a array) : Result<'a, 'err> =
        match arr with
        | [| |]   -> Error emptyErr
        | [| x |] -> Ok x
        | _       -> Error multipleErr

    let containsIgnoreCase (txt: string) (arr: string array) : bool =
        arr |> Array.exists (fun x -> String.Equals(x, txt, StringComparison.OrdinalIgnoreCase))

    let anyContainsIgnoreCase (txt: string) : string array array -> bool =
        Array.exists (containsIgnoreCase txt)

    /// If the array is empty, returns None. Otherwise, wraps the array in Some.
    let toOption (arr: 'a array) : 'a array option =
        if Array.isEmpty arr then None else Some arr

    /// If the array is empty, returns the specified Error. Otherwise, wraps the array in Ok.
    let toResult (err: 'err) (arr: 'a array) : Result<'a array, 'err> =
        if Array.isEmpty arr then Error err else Ok arr

    /// If the array is empty, returns the specified Error.
    /// Otherwise, converts it to a NonEmptyList wrapped in Ok.
    let toNonEmptyListResult (err: 'err) : 'a array -> Result<'a NonEmptyList, 'err> =
        function
        | [||] -> Error err
        | arr  -> Ok (NonEmptyList.ofArray arr)

    /// If the array is empty, returns the specified Error.
    /// Otherwise, converts it to a NonEmptySeq wrapped in Ok.
    let toNonEmptySeqResult (err: 'err) : 'a array -> Result<'a NonEmptySeq, 'err> =
        function
        | [||] -> Error err
        | arr  -> Ok (NonEmptySeq.ofArray arr)

    /// If the array is empty, returns the specified Error.
    /// Otherwise, converts it to a NonEmptySet wrapped in Ok.
    let toNonEmptySetResult (err: 'err) : 'a array -> Result<NonEmptySet<'a>, 'err> =
        function
        | [||] -> Error err
        | arr  -> Ok (NonEmptySet.ofArray arr)
