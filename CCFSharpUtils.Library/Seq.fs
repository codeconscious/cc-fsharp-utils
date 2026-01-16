namespace CCFSharpUtils.Library

open System

[<RequireQualifiedAccess>]
module Seq =

    let isNotEmpty seq = not <| Seq.isEmpty seq

    let anyNotEmpty seqs = seqs |> Seq.exists isNotEmpty

    let allNotEmpty seqs = seqs |> Seq.forall isNotEmpty

    let doesNotContain x = not << Seq.contains x

    let headElse alt =
        Seq.tryHead >> Option.defaultValue alt

    let hasOne seq = seq |> Seq.length |> Num.isOne

    let hasMultiple seq = seq |> Seq.length |> (<) 1

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
