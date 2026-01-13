namespace CCFSharpUtils.Library

/// Functions for or involving regular expressions (regex).
[<RequireQualifiedAccess>]
module Rgx =
    open System.Text.RegularExpressions

    let trySuccessMatch (rgx: Regex) txt =
        let result = rgx.Match txt
        match result.Success with
        | false -> None
        | true  -> Some result

    let capturesToSeq (m: Match) : Match seq =
        m.Captures |> Seq.cast<Match>

    let fstCapture (m: Match) : Match =
        m |> capturesToSeq |> Seq.head
