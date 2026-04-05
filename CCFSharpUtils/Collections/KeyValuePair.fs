namespace CCFSharpUtils.Library

[<RequireQualifiedAccess>]
module KeyValuePair =
    open System.Collections.Generic

    /// Group a sequence of KeyValuePair items by their values.
    /// Preserves the grouping order produced by Seq.groupBy; keys for each value
    /// are returned as a sequence in the same relative order they appeared in the input.
    let groupByValues (pairs: KeyValuePair<'k, 'v> seq) : ('v * 'k seq) seq =
        pairs
        |> Seq.groupBy _.Value
        |> Seq.map (fun (v, pairs) -> v, pairs |> Seq.map _.Key)
