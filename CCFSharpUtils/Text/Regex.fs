namespace CCFSharpUtils.Text

open System
open System.Text.RegularExpressions

/// Functions for or involving regular expressions (regex).
[<RequireQualifiedAccess>]
module Rgx =

    let groups (m: Match) : Group seq =
        m.Groups |> Seq.cast<Group>

    let successMatches (matches: MatchCollection) : Match seq =
        matches
        |> Seq.cast<Match>
        |> Seq.filter _.Success

    let trySuccessMatch (rgx: Regex) txt =
        let result = rgx.Match txt
        match result.Success with
        | false -> None
        | true  -> Some result

    let capturesToSeq (m: Match) : Match seq =
        m.Captures |> Seq.cast<Match>

    let fstCapture (m: Match) : Match =
        m |> capturesToSeq |> Seq.head

    /// Use regex patterns to remove matching substrings in text.
    /// Each pattern will remove all occurrences in the text.
    /// Uses `Regex.Replace`, which will throw if any pattern is invalid.
    let scrubMatches (patterns: string seq) text : string =
        if String.IsNullOrEmpty text || Seq.isEmpty patterns
        then text
        else Seq.fold (fun acc p -> Regex.Replace(acc, p, String.Empty)) text patterns

    /// An active pattern for matching regular expression patterns during pattern matching.
    /// Returns the entire matched value (i.e., match group 0).
    let (|MatchValue|_|) (pattern: string) (input: string) : string option =
        match Regex.Match(input, pattern) with
        | m when m.Success -> Some m.Value
        | _ -> None

    /// An active pattern for matching regular expression patterns during pattern matching.
    /// Returns only the matched subgroups (i.e., groups 1 and later).
    /// (Use parentheses in your regex patterns to indicate groups.)
    let (|MatchGroups|_|) (pattern: string) (input: string) : string list option =
        match Regex.Match(input, pattern) with
        | m when m.Success -> Some (List.tail [for g in m.Groups -> g.Value])
        | _ -> None
