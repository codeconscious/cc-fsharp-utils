namespace CCFSharpUtils

open FSharpPlus.Data

[<AutoOpen>]
module Types =

    /// A non-empty list that guarantees at least one element.
    /// Alias of FSharpPlus.Data.NonEmptyList.
    type nlist<'a> = NonEmptyList<'a>

    /// A non-empty sequence that guarantees at least one element.
    /// Alias of FSharpPlus.Data.NonEmptySeq.
    type nseq<'a> = NonEmptySeq<'a>

    /// A non-empty map that guarantees at least one element.
    /// Alias of FSharpPlus.Data.NonEmptyMap.
    type nmap<'a, 'b  when 'a: comparison> = NonEmptyMap<'a, 'b>

    /// A non-empty set that guarantees at least one element.
    /// Alias of FSharpPlus.Data.NonEmptySet.
    type nset<'a when 'a: comparison> = NonEmptySet<'a>

    /// Alias of System.Text.StringBuilder.
    type SB = System.Text.StringBuilder
