namespace CCFSharpUtils.Collections

open CCFSharpUtils
open FSharpPlus.Data
open FsToolkit.ErrorHandling
open System

[<RequireQualifiedAccess>]
module Array =
    open System.Linq

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

    /// If the array contains one item, returns it wrapped in Ok. Otherwise, returns the appropriate error.
    /// Intended to be used with FsToolkit.ErrorHandling's validation.
    let ensureOneV xs emptyErr multipleErr : Validation<'a, 'err> =
        match xs with
        | [||]    -> Error [emptyErr]
        | [| x |] -> Ok x
        | _       -> Error [multipleErr]

    let ensureSize (targetSize: int) (tooSmallErr: 'err) (tooLargeErr: 'err) (arr: 'a array) : Result<'a array, 'err> =
        if Num.isNeg targetSize then
            invalidArg (nameof targetSize) "Target size cannot be negative."

        match compareWith targetSize arr.Length with
        | EQ -> Ok arr
        | LT -> Error tooSmallErr
        | GT -> Error tooLargeErr

    let ensureNotEmptyV xs err : Validation<'a array, 'b> =
        if isNotEmpty xs
        then Ok xs
        else Error [err]

    let tryGetSingle (emptyErr: 'err) (multipleErr: 'err) (arr: 'a array) : Result<'a, 'err> =
        match arr with
        | [| |]   -> Error emptyErr
        | [| x |] -> Ok x
        | _       -> Error multipleErr

    let containsIgnoreCase (txt: string) (arr: string array) : bool =
        arr |> Array.exists (fun x -> String.Equals(x, txt, StringComparison.OrdinalIgnoreCase))

    let anyContainsIgnoreCase (txt: string) : string array array -> bool =
        Array.exists (containsIgnoreCase txt)

    let distinctIgnoreCase (arr: string array) =
        Enumerable.Distinct(arr, StringComparer.OrdinalIgnoreCase) |> Seq.toArray

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

    let toNonEmptyListOption : 'a array -> Option<'a NonEmptyList> =
        function
        | [||] -> None
        | arr  -> Some (NonEmptyList.ofArray arr)

    let toNonEmptySeqOption : 'a array -> Option<'a NonEmptySeq> =
        function
        | [||] -> None
        | arr  -> Some (NonEmptySeq.ofArray arr)

    let toNonEmptySetOption : 'a array -> Option<NonEmptySet<'a>> =
        function
        | [||] -> None
        | arr  -> Some (NonEmptySet.ofArray arr)

    /// Map over the first collection of each pair in a array of tuples, preserving each pair's second collection.
    let mapFst (f : 'a -> 'b) (xs: ('a array * 'c) array) : ('b array * 'c) array =
        xs |> Array.map (fun (as_, y) -> Array.map f as_, y)

    /// Map over the second collection of each pair in a array of tuples, preserving each pair's first collection.
    let mapSnd (f : 'b -> 'c) (xs: ('a * 'b array) array) : ('a * 'c array) array =
        xs |> Array.map (fun (x, ys) -> x, Array.map f ys)
