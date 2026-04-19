namespace CCFSharpUtils

open System
open System.IO
open System.Text
open System.Text.Json
open System.Text.Encodings.Web
open System.Text.Unicode
open System.Globalization

[<RequireQualifiedAccess>]
module String =

    /// An alias for `System.Environment.NewLine`.
    let newLine = Environment.NewLine

    /// An alias for `newline`.
    let nl = newLine

    let hasNoText (text: string) : bool =
        String.IsNullOrWhiteSpace text

    let hasText (text: string) : bool =
        not (hasNoText text)

    let allHaveText (xs: string seq) : bool =
        xs |> Seq.forall hasText

    let firstWithTextElse (alt: string) (texts: string seq) : string =
        texts |> Seq.tryFind hasText |> Option.defaultValue alt

    let textElse (alt: string) (text: string) : string =
        if hasText text then text else alt

    let textElseEmpty (text: string) : string =
        textElse text String.Empty

    let equalIgnoreCase (x: string) (y: string) : bool =
        String.Equals(x, y, StringComparison.OrdinalIgnoreCase)

    let startsWithIgnoreCase (startText: string) (text: string) : bool  =
        text.StartsWith(startText, StringComparison.InvariantCultureIgnoreCase)

    let endsWithIgnoreCase (endText: string) (text: string) : bool =
        text.EndsWith(endText, StringComparison.InvariantCultureIgnoreCase)

    let toLower (x: string) = x.ToLowerInvariant()

    let toUpper (x: string) = x.ToUpperInvariant()

    /// Splits text using line breaks as the separator, returning an array of substrings.
    let splitLines (text: string) =
        text.Split newLine

    /// Splits text using line breaks as the separator and using the given `StringSplitOptions`,
    /// returning an array of substrings.
    let splitLinesWithOpts (opts: StringSplitOptions) (text: string) =
        text.Split(newLine, opts)


    /// Returns a new string in which all invalid path characters for the current OS
    /// have been replaced by the specified replacement character.
    /// Throws if the replacement character is an invalid path character.
    let replaceInvalidPathChars
        (replaceWith: char option)
        (customInvalidChars: char list option)
        (text: string)
        : string =

        let replaceWith = defaultArg replaceWith '_'
        let custom = defaultArg customInvalidChars []

        let invalidChars =
            seq {
                yield! Path.GetInvalidFileNameChars()
                yield! Path.GetInvalidPathChars()
                yield  Path.PathSeparator
                yield  Path.DirectorySeparatorChar
                yield  Path.AltDirectorySeparatorChar
                yield  Path.VolumeSeparatorChar
                yield! custom
            }
            |> Set.ofSeq

        if invalidChars |> Set.contains replaceWith  then
            invalidArg "replaceWith" $"The replacement char ('%c{replaceWith}') must be a valid path character."

        Set.fold
            (fun (sb: StringBuilder) ch -> sb.Replace(ch, replaceWith))
            (StringBuilder text)
            invalidChars
        |> _.ToString()

    let trim (text: string) : string =
        text.Trim()

    let trimTerminalLineBreak (text: string) =
        text.TrimEnd(newLine.ToCharArray())

    /// <summary>
    /// Trims leading and trailing whitespace from each line in a line break–separated string,
    /// then rejoins the lines using line breaks.
    /// </summary>
    /// <param name="combinedLines">Input string containing lines separated by line breaks.</param>
    /// <returns>String where each line is trimmed and concatenated with line breaks.</returns>
    let trimCombinedLines (combinedLines: string) : string =
        combinedLines
        |> _.Split(newLine)
        |> Array.map trim
        |> String.concat newLine

    /// Formats a number of any type to a comma-formatted string, rounding any decimals automatically.
    let inline formatNumber (i: ^a) : string
        when ^a : (member ToString : string * IFormatProvider -> string) =
        (^a : (member ToString : string * IFormatProvider -> string) (i, "#,##0", CultureInfo.InvariantCulture))

    /// Formats an integer to a comma-separated numeric string. Example: 1000 -> "1,000".
    let formatInt (i: int) : string =
        i.ToString("#,##0", CultureInfo.InvariantCulture)

    /// Formats a 64-bit integer to a comma-separated numeric string. Example: 1000 -> "1,000".
    let formatInt64 (i: int64) : string =
        i.ToString("#,##0", CultureInfo.InvariantCulture)

    /// Formats a float to a comma-separated numeric string. Example: 1000.00 -> "1,000.00".
    let formatFloat (f: float) : string =
        f.ToString("#,##0.00", CultureInfo.InvariantCulture)

    /// Formats a float to a percentage string with a custom number of decimal places.
    /// Note that 1 equals 100%.
    let formatPercent (decimalPlaces: int) (n: float) : string =
        let decimalPlaces' = if decimalPlaces < 0 then 0 else decimalPlaces
        let pct = n * 100.0
        pct.ToString("F" + decimalPlaces'.ToString(), CultureInfo.InvariantCulture) + "%"

    /// Formats a byte count into a human-friendly size representation using KB, MB, GB, or TB.
    let formatBytes (bytes: int64) =
        let kilobyte  = 1024L
        let megabyte  = kilobyte * 1024L
        let gigabyte  = megabyte * 1024L
        let terabyte  = gigabyte * 1024L
        let petabyte  = terabyte * 1024L
        let exabyte   = petabyte * 1024L

        if bytes < 0 then
            invalidArg (nameof bytes) $"Bytes cannot be negative, but %d{bytes} was passed."
        else
            match bytes with
            | _ when bytes >= exabyte  -> sprintf "%sEB" (float bytes / float exabyte  |> formatFloat)
            | _ when bytes >= petabyte -> sprintf "%sPB" (float bytes / float petabyte |> formatFloat)
            | _ when bytes >= terabyte -> sprintf "%sTB" (float bytes / float terabyte |> formatFloat)
            | _ when bytes >= gigabyte -> sprintf "%sGB" (float bytes / float gigabyte |> formatFloat)
            | _ when bytes >= megabyte -> sprintf "%sMB" (float bytes / float megabyte |> formatFloat)
            | _ when bytes >= kilobyte -> sprintf "%sKB" (float bytes / float kilobyte |> formatFloat)
            | _ -> sprintf "%s bytes" (bytes |> formatInt64)

    /// Formats a TimeSpan to "h:mm:ss" format, where the hours ('h') are optional.
    let formatTimeSpan (timeSpan: TimeSpan) : string =
        match timeSpan.Hours with
        | 0 -> sprintf "%dm%ds" timeSpan.Minutes timeSpan.Seconds
        | _ -> sprintf "%dh%dm%ds" timeSpan.Hours timeSpan.Minutes timeSpan.Seconds

    /// Serializes text to a JSON string, returning a Result.
    /// If an exception is thrown during the underlying operation,
    /// the Error only includes its message.
    let private serializeToJson (writeIndented: bool) (x: 'a) : Result<string, string> =
        let options =
            JsonSerializerOptions(
                WriteIndented = writeIndented,
                Encoder = JavaScriptEncoder.Create UnicodeRanges.All)

        ofTry (fun _ -> JsonSerializer.Serialize(x, options))

    /// Serializes text to a formatted JSON string, returning a Result.
    /// If an exception is thrown during the underlying operation,
    /// the Error only includes its message.
    let toJson (x: 'a) : Result<string, string> =
        serializeToJson true x

    /// Serializes text to a raw, unformatted JSON string, returning a Result.
    /// If an exception is thrown during the underlying operation,
    /// the Error only includes its message.
    let toRawJson (x: 'a) : Result<string, string> =
        serializeToJson false x

    /// Removes all instances of multiple substrings from a given string.
    let stripSubstrings (substrings: string seq) (text: string) : string =
        Seq.fold
            (fun acc x -> acc.Replace(x, String.Empty, StringComparison.InvariantCultureIgnoreCase))
            text
            substrings

    /// Various whitespace characters.
    let whiteSpaces: char list =
        [
            '\u0020' // space
            '\u00A0' // non-breaking space
            '\u1680' // Ogham space mark
            '\u180E' // Mongolian vowel separator
            '\u2000' // en quad
            '\u2001' // em quad
            '\u2002' // en space
            '\u2003' // em space
            '\u2004' // three-per-em space
            '\u2005' // four-per-em space
            '\u2006' // six-per-em space
            '\u2007' // figure space
            '\u2008' // punctuation space
            '\u2009' // thin space
            '\u200A' // hair space
            '\u200B' // zero-width space
            '\u200D' // zero-width joiner (emoji)
            '\u202F' // narrow non-breaking space
            '\u205F' // medium mathematical space
            '\u2063' // invisible separator
            '\u3000' // ideographic space (i.e., Japanese full-width space)
            '\u3164' // Hangul filler
            '\uFEFF' // zero-width non-breaking space
        ]

    /// Whitespace characters converted to strings.
    let whiteSpaceStrs = whiteSpaces |> List.map _.ToString()

    let stripWhiteSpace (text: string) : string =
        text.ToCharArray()
        |> Array.filter (not << fun ch -> List.contains ch whiteSpaces)
        |> String

    let stripPunctuation (text: string) : string =
        text.ToCharArray()
        |> Array.filter (not << Char.IsPunctuation)
        |> String

    /// Strips diacritics from strings -- e.g., "Ñ" -> "N".
    /// Only works on diacritics that exist as separate characters.
    /// Will not work on on characters where the diacritic is
    /// an integral part of the letter's identity, like "ł".
    /// Returns text in normalization form C.
    let stripDiacritics (text: string) =
        text.Normalize NormalizationForm.FormD
        |> _.EnumerateRunes()
        |> Seq.filter (fun r -> Rune.GetUnicodeCategory r <> UnicodeCategory.NonSpacingMark)
        |> String.Concat
        |> _.Normalize(NormalizationForm.FormC)

    /// Pluralize text conditionally using a specified count.
    let inline pluralize (ifOne: 'a) (ifNotOne: 'a) (count: ^b) : 'a =
        if Num.isOne count then ifOne else ifNotOne

    /// Pluralize text conditionally with "s" via a specified count.
    let inline pluralizeS (word: string) (count: ^a) : string =
        pluralize word $"{word}s" count

    /// Pluralize text conditionally with "es" via a specified count.
    let inline pluralizeEs (word: string) (count: ^a) : string =
        pluralize word $"{word}es" count

    /// Pluralize text conditionally including its count, such as "1 file", "30 URLs".
    let inline pluralizeWithCount (ifOne: string) (ifNotOne: string) (count: ^a) : string =
        sprintf "%d %s" count (pluralize ifOne ifNotOne count)

    /// Pluralize text conditionally with "s" including its count, such as "1 file", "30 URLs".
    let inline pluralizeSWithCount (word: string) (count: ^a) : string =
        sprintf "%d %s" count (pluralizeS word count)

    /// Pluralize text conditionally with "es" including its count, such as "1 file", "30 URLs".
    let inline pluralizeEsWithCount (word: string) (count: ^a) : string =
        sprintf "%d %s" count (pluralizeEs word count)

    let inline private fileLabeller (description: string option) (count: int) : string =
        match description with
        | None   -> $"""%s{formatNumber count} %s{pluralize "file" "files" count}"""
        | Some d -> $"""%s{formatNumber count} %s{d} {pluralize "file" "files" count}"""

    /// Returns a file-count string, such as "0 files" or 1 file" or "140 files".
    let fileLabel (count: int) : string =
        fileLabeller None count

    /// Returns a file-count string with a descriptor, such as "0 audio files" or "140 deleted files".
    let fileLabelWithDesc (description: string) (count: int) : string =
        fileLabeller (Some (description.Trim())) count

    /// If a string contains non-whitespace text, encloses it in Some. Otherwise, returns None.
    let toOption (x: string) : string option =
        if hasText x then Some x else None

    /// If the string has non-whitespace text, encloses it in Ok.
    /// Otherwise, returns the specified error.
    let toResult (err: 'err) (x: string) : Result<string, 'err> =
        if hasText x then Ok x else Error err

    /// Concatentates a collection to a string using line breaks.
    let concatNL (x: string seq) : string =
        x |> String.concat nl

    /// Concatenates each tuple's sequence of strings using innerSeparator, then joins those concatenated strings
    /// using outerSeparator, producing a single combined string. The tuple keys are ignored; only the
    /// string sequences are used.
    let tupleValuesToNestedPairs
        (innerSeparator: string)
        (outerSeparator: string)
        (tuples: ('k * string seq) seq)
        : string =

        tuples
        |> Seq.map (fun (_, vs) -> vs |> String.concat innerSeparator)
        |> String.concat outerSeparator
