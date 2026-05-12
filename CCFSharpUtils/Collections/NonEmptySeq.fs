namespace CCFSharpUtils.Collections

open CCFSharpUtils
open FSharpPlus.Data
open System

[<RequireQualifiedAccess>]
module NonEmptySeq =
    open System.Linq

    let doesNotContain (x: 'a) : 'a nseq -> bool =
        not << NonEmptySeq.contains x

    let takeLast (count: int) (xs: 'a nseq) : 'a nseq =
        xs
        |> NonEmptySeq.rev
        |> NonEmptySeq.truncate count
        |> NonEmptySeq.rev

    let hasOne (xs: 'a nseq) : bool =
        xs |> NonEmptySeq.length |> Num.isOne

    let hasMultiple (xs: 'a nseq) : bool =
        xs |> NonEmptySeq.length |> (<) 1

    let ensureOne (multipleErr: 'a) (xs: 'b nseq) : Result<'b nseq, 'a> =
        if xs |> NonEmptySeq.length |> Num.isOne
        then Ok xs
        else Error multipleErr

    let ensureSize (targetSize: int) (tooSmallErr: 'err) (tooLargeErr: 'err) (xs: 'a nseq): Result<'a nseq, 'err> =
        if Num.isNeg targetSize then
            invalidArg (nameof targetSize) "Target size cannot be negative."

        match xs |> NonEmptySeq.length |> compareWith targetSize with
        | EQ -> Ok xs
        | LT -> Error tooSmallErr
        | GT -> Error tooLargeErr

    let tryGetSingle(multipleErr: 'err) (xs: 'a nseq) : Result<'a, 'err> =
        if xs |> NonEmptySeq.length |> Num.isOne
        then Ok (NonEmptySeq.head xs)
        else Error multipleErr

    let containsIgnoreCase (txt: string) (xs: string nseq) : bool =
        xs |> NonEmptySeq.exists (fun x -> String.Equals(x, txt, StringComparison.OrdinalIgnoreCase))

    let anyContainsIgnoreCase (txt: string) : string nseq nseq -> bool =
        NonEmptySeq.exists (containsIgnoreCase txt)

    let distinctByIgnoreCase (seq: string nseq) : string nseq =
        Enumerable.Distinct(seq, StringComparer.OrdinalIgnoreCase) |> NonEmptySeq.ofSeq

    /// Map over the first sequence of each pair in a sequence of tuples, preserving each pair's second sequence.
    let mapFst (f : 'a -> 'b) (xs: ('a nseq * 'c) nseq) : ('b nseq * 'c) nseq =
        xs |> NonEmptySeq.map (fun (as_, y) -> NonEmptySeq.map f as_, y)

    /// Map over the second sequence of each pair in a sequence of tuples, preserving each pair's first sequence.
    let mapSnd (f : 'b -> 'c) (xs: ('a * 'b nseq) nseq) : ('a * 'c nseq) nseq =
        xs |> NonEmptySeq.map (fun (x, ys) -> x, NonEmptySeq.map f ys)
