namespace CCFSharpUtils.Collections

open CCFSharpUtils
open FSharpPlus.Data
open System

[<RequireQualifiedAccess>]
module NonEmptyList =

    let doesNotContain (x: 'a) : 'a nlist -> bool =
        not << NonEmptyList.contains x

    let takeLast (count: int) (lst: 'a nlist) : 'a nlist =
        lst
        |> NonEmptyList.rev
        |> NonEmptyList.truncate count
        |> NonEmptyList.rev

    let hasOne (lst: 'a nlist) : bool =
        lst |> NonEmptyList.length |> Num.isOne

    let hasMultiple (lst: 'a nlist) : bool =
        lst |> NonEmptyList.length |> (<) 1

    let ensureOne (multipleErr: 'a) (lst: 'b nlist) : Result<'b nlist, 'a> =
        if Num.isOne lst.Length
        then Ok lst
        else Error multipleErr

    let ensureSize (targetSize: int) (tooSmallErr: 'err) (tooLargeErr: 'err) (lst: 'a nlist): Result<'a nlist, 'err> =
        if Num.isNeg targetSize then
            invalidArg (nameof targetSize) "Target size cannot be negative."

        match compareWith targetSize lst.Length with
        | EQ -> Ok lst
        | LT -> Error tooSmallErr
        | GT -> Error tooLargeErr

    let tryGetSingle(multipleErr: 'err) (lst: 'a nlist) : Result<'a, 'err> =
        if Num.isOne lst.Length
        then Ok lst[0]
        else Error multipleErr

    let containsIgnoreCase (txt: string) (lst: string nlist) : bool =
        lst |> NonEmptyList.exists (fun x -> String.Equals(x, txt, StringComparison.OrdinalIgnoreCase))

    let anyContainsIgnoreCase (txt: string) : string nlist nlist -> bool =
        NonEmptyList.exists (containsIgnoreCase txt)

    /// Map over the first collection of each pair in a list of tuples, preserving each pair's second collection.
    let mapFst (f : 'a -> 'b) (xs: ('a nlist * 'c) nlist) : ('b nlist * 'c) nlist =
        xs |> NonEmptyList.map (fun (as_, y) -> NonEmptyList.map f as_, y)

    /// Map over the second collection of each pair in a list of tuples, preserving each pair's first collection.
    let mapSnd (f : 'b -> 'c) (xs: ('a * 'b nlist) nlist) : ('a * 'c nlist) nlist =
        xs |> NonEmptyList.map (fun (x, ys) -> x, NonEmptyList.map f ys)
