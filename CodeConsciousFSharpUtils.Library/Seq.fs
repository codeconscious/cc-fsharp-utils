namespace CodeConsciousFSharpUtils.Library

open System
open System.IO
open System.Text
open System.Text.Json
open System.Text.Encodings.Web
open System.Text.Unicode
open System.Globalization

[<RequireQualifiedAccess>]
module Seq =

    let isNotEmpty seq = not (Seq.isEmpty seq)

    let doesNotContain x seq = not <| Seq.contains x seq

    let hasOne seq = seq |> Seq.length |> Numerics.isOne

    let hasMultiple seq = seq |> Seq.length |> (<) 1

    let containsIgnoreCase text (xs: string seq) : bool =
        xs |> Seq.exists (fun x -> String.Equals(x, text, StringComparison.OrdinalIgnoreCase))

    let anyContainsIgnoreCase text (seqs: string seq seq) =
        seqs |> Seq.exists (containsIgnoreCase text)
