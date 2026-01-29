namespace CCFSharpUtils.Library

open FSharpPlus.Data
open System

[<RequireQualifiedAccess>]
module Seq =

    let isNotEmpty seq =
        not <| Seq.isEmpty seq

    let anyNotEmpty seqs =
        seqs |> Seq.exists isNotEmpty

    let allNotEmpty seqs =
        seqs |> Seq.forall isNotEmpty

    let doesNotContain x =
        not << Seq.contains x

    let headElse alt =
        Seq.tryHead >> Option.defaultValue alt

    let takeLast count seq =
        if count <= 0 then
            Seq.empty
        else
            seq
            |> Array.ofSeq
            |> Array.takeLast count
            |> Seq.ofArray

    let hasOne seq =
        seq |> Seq.length |> Num.isOne

    let hasMultiple seq =
        seq |> Seq.length |> (<) 1

    let ensureOne emptyErr multipleErr (seq: 'b seq) :  Result<'b seq,'a> =
        if Seq.isEmpty seq then
            Error emptyErr
        elif hasOne seq then
            Ok seq
        else Error multipleErr

    let ensureSize targetSize tooSmallErr tooLargeErr (seq: 'b seq) : Result<'b seq,'a> =
        let length = Seq.length seq
        match compareWith targetSize length with
        | EQ -> Ok seq
        | LT -> Error tooSmallErr
        | GT -> Error tooLargeErr

    let tryGetSingle emptyErr multipleErr seq =
        if hasOne seq then
            Ok (Seq.head seq)
        elif hasMultiple seq then
            Error multipleErr
        else
            Error emptyErr

    let containsIgnoreCase text (xs: string seq) : bool =
        xs |> Seq.exists (fun x -> String.Equals(x, text, StringComparison.OrdinalIgnoreCase))

    let anyContainsIgnoreCase text =
        Seq.exists (containsIgnoreCase text)

    /// If the seq is empty, returns None. Otherwise, wraps the seq in Some.
    let toOption seq =
        if Seq.isEmpty seq then None else Some seq

    /// If the seq is empty, returns the specified Error. Otherwise, wraps the seq in Ok.
    let toResult err seq =
        if Seq.isEmpty seq then Error err else Ok seq

    /// If the seq is empty, returns the specified Error.
    /// Otherwise, converts it to a NonEmptySeq wrapped in Ok.
    let toNonEmptySeqResult err s =
        if Seq.isEmpty s
        then Error err
        else Ok (NonEmptySeq.ofSeq s)
