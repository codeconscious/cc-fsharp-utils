namespace CCFSharpUtils.Collections

open CCFSharpUtils
open FSharpPlus.Data
open FsToolkit.ErrorHandling
open System

[<RequireQualifiedAccess>]
module Seq =

    let isNotEmpty (seq: 'a seq) : bool =
        not <| Seq.isEmpty seq

    let anyNotEmpty (seqs: 'a seq) : bool =
        seqs |> Seq.exists isNotEmpty

    let allNotEmpty (seqs: 'a seq) : bool =
        seqs |> Seq.forall isNotEmpty

    let doesNotContain (x: 'a) : 'a seq -> bool =
        not << Seq.contains x

    let headElse (alt: 'a) : 'a seq -> 'a =
        Seq.tryHead >> Option.defaultValue alt

    let takeLast (count: int) (seq: 'a seq) : 'a seq =
        if count <= 0 then
            Seq.empty
        else
            seq
            |> Array.ofSeq
            |> Array.takeLast count
            |> Seq.ofArray

    let hasOne (seq: 'a seq) : bool =
        seq |> Seq.length |> Num.isOne

    let hasMultiple (seq: 'a seq) : bool =
        seq |> Seq.length |> (<) 1

    let ensureOne (emptyErr: 'err) (multipleErr: 'err) (seq: 'a seq) :  Result<'a seq, 'err> =
        if Seq.isEmpty seq then
            Error emptyErr
        elif hasOne seq then
            Ok seq
        else Error multipleErr

    /// If the seq contains one item, returns it wrapped in Ok. Otherwise, returns the appropriate error.
    /// Intended to be used with FsToolkit.ErrorHandling's validation.
    let ensureOneV xs emptyErr multipleErr : Validation<'a, 'err> =
        match Seq.length xs with
        | 0 -> Error [emptyErr]
        | 1 -> Ok (Seq.head xs)
        | _ -> Error [multipleErr]

    let ensureSize
        (targetSize: int)
        (tooSmallErr: 'err)
        (tooLargeErr: 'err)
        (seq: 'a seq)
        : Result<'a seq, 'err> =

        if Num.isNeg targetSize then
            invalidArg (nameof targetSize) "Target size cannot be negative."

        let length = Seq.length seq
        match compareWith targetSize length with
        | EQ -> Ok seq
        | LT -> Error tooSmallErr
        | GT -> Error tooLargeErr

    let ensureNotEmptyV xs err : Validation<'a seq, 'b> =
        if isNotEmpty xs
        then Ok xs
        else Error [err]

    let tryGetSingle (emptyErr: 'err) (multipleErr: 'err) (seq: 'a seq) : Result<'a, 'err> =
        if hasOne seq then
            Ok (Seq.head seq)
        elif hasMultiple seq then
            Error multipleErr
        else
            Error emptyErr

    let containsIgnoreCase (text: string) (xs: string seq) : bool =
        xs |> Seq.exists (fun x -> String.Equals(x, text, StringComparison.OrdinalIgnoreCase))

    let anyContainsIgnoreCase (text: string) : 'a seq -> bool =
        Seq.exists (containsIgnoreCase text)

    /// If the seq is empty, returns None. Otherwise, wraps the seq in Some.
    let toOption (seq: 'a) : 'a option =
        if Seq.isEmpty seq then None else Some seq

    /// If the seq is empty, returns the specified Error. Otherwise, wraps the seq in Ok.
    let toResult (err: 'err) (seq: 'a) : Result<'a, 'err> =
        if Seq.isEmpty seq then Error err else Ok seq

    /// If the sequence is empty, returns the specified Error.
    /// Otherwise, converts it to a NonEmptySeq wrapped in Ok.
    let toNonEmptySeqResult (err: 'err) (s: 'a seq) : Result<'a NonEmptySeq, 'err> =
        if Seq.isEmpty s
        then Error err
        else Ok (NonEmptySeq.ofSeq s)

    /// If the sequence is empty, returns the specified Error.
    /// Otherwise, converts it to a NonEmptySeq wrapped in Ok.
    let toNonEmptyListResult (err: 'err) (s: 'a seq) : Result<'a NonEmptyList, 'err> =
        if Seq.isEmpty s
        then Error err
        else Ok (NonEmptyList.ofSeq s)

    /// If the sequence is empty, returns the specified Error.
    /// Otherwise, converts it to a NonEmptySeq wrapped in Ok.
    let toNonEmptySetResult (err: 'err) (s: 'a seq) : Result<NonEmptySet<'a>, 'err> =
        if Seq.isEmpty s
        then Error err
        else Ok (NonEmptySet.ofSeq s)

    let toNonEmptySeqOption (s: 'a seq) : Option<'a NonEmptySeq> =
        if Seq.isEmpty s
        then None
        else Some (NonEmptySeq.ofSeq s)

    let toNonEmptyListOption (s: 'a seq) : Option<'a NonEmptyList> =
        if Seq.isEmpty s
        then None
        else Some (NonEmptyList.ofSeq s)

    let toNonEmptySetOption (s: 'a seq) : Option<NonEmptySet<'a>> =
        if Seq.isEmpty s
        then None
        else Some (NonEmptySet.ofSeq s)

    /// Map over the first collection of each pair in a list of tuples, preserving each pair's second collection.
    let mapFst (f : 'a -> 'b) (xs: ('a seq * 'c) seq) : ('b seq * 'c) seq =
        xs |> Seq.map (fun (as_, y) -> Seq.map f as_, y)

    /// Map over the second collection of each pair in a list of tuples, preserving each pair's first collection.
    let mapSnd (f : 'b -> 'c) (xs: ('a * 'b seq) seq) : ('a * 'c seq) seq =
        xs |> Seq.map (fun (x, ys) -> x, Seq.map f ys)
