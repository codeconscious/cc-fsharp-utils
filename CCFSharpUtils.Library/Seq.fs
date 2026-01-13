namespace CCFSharpUtils.Library

open System

[<RequireQualifiedAccess>]
module Seq =

    let isNotEmpty seq = not (Seq.isEmpty seq)

    let anyNotEmpty seqs = seqs |> Seq.exists isNotEmpty

    let allNotEmpty seqs = seqs |> Seq.forall isNotEmpty

    let doesNotContain x seq = not <| Seq.contains x seq

    let headElse alt seq =
        seq |> Seq.tryHead |> Option.defaultValue alt

    let hasOne seq = seq |> Seq.length |> Num.isOne

    let hasMultiple seq = seq |> Seq.length |> (<) 1

    let containsIgnoreCase text (xs: string seq) : bool =
        xs |> Seq.exists (fun x -> String.Equals(x, text, StringComparison.OrdinalIgnoreCase))

    let anyContainsIgnoreCase text (seqs: string seq seq) =
        seqs |> Seq.exists (containsIgnoreCase text)

    /// If the seq is empty, returns None. Otherwise, wraps the seq in Some.
    let toOption seq = if Seq.isEmpty seq then None else Some seq
