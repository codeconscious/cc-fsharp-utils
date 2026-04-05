namespace CCFSharpUtils

open System
open FSharpPlus.Data

[<RequireQualifiedAccess>]
module Set =

    let isNotEmpty (s: 'a Set) : bool =
        not <| Set.isEmpty s

    let anyNotEmpty (ss: 'a Set Set) : bool =
        ss |> Set.exists isNotEmpty

    let allNotEmpty (ss: 'a Set Set) : bool =
        ss |> Set.forall isNotEmpty

    let doesNotContain (x: 'a) : 'a Set -> bool =
        not << Set.contains x

    let hasOne (s: 'a Set) : bool =
        s |> Set.count |> Num.isOne

    let hasMultiple (s: 'a Set) : bool =
        s |> Set.count |> (<) 1

    let ensureOne (emptyErr: 'err) (multipleErr: 'err) (s: 'a Set) :  Result<'a Set, 'err> =
        if Set.isEmpty s then
            Error emptyErr
        elif hasOne s then
            Ok s
        else Error multipleErr

    let ensureSize (targetSize: int) (tooSmallErr: 'err) (tooLargeErr: 'err) (s: 'a Set) : Result<'a Set, 'err> =
        if Num.isNeg targetSize then
            invalidArg (nameof targetSize) "Target size cannot be negative."

        let length = Set.count s
        match compareWith targetSize length with
        | EQ -> Ok s
        | LT -> Error tooSmallErr
        | GT -> Error tooLargeErr

    let tryGetSingle (emptyErr: 'err) (multipleErr: 'err) (s: 'a Set) : Result<'a, 'err> =
        if hasOne s then
            Ok (s |> Set.minElement)
        elif hasMultiple s then
            Error multipleErr
        else
            Error emptyErr

    let containsIgnoreCase (txt: string) (s: string Set) : bool =
        s |> Set.exists (fun x -> String.Equals(x, txt, StringComparison.OrdinalIgnoreCase))

    let anyContainsIgnoreCase (txt: string) : string Set Set -> bool =
        Set.exists (containsIgnoreCase txt)

    /// If the set is empty, returns None. Otherwise, wraps the set in Some.
    let toOption (s: 'a Set) : 'a Set option =
        if Set.isEmpty s then None else Some s

    /// If the set is empty, returns the specified Error. Otherwise, wraps the set in Ok.
    let toResult (err: 'err) (s: 'a Set) : Result<'a Set, 'err> =
        if Set.isEmpty s then Error err else Ok s

    /// If the set is empty, returns the specified Error.
    /// Otherwise, converts it to a NonEmptySet wrapped in Ok.
    let toNonEmptySetResult (err: 'err) (s: 'a Set) : Result<'a NonEmptySet, 'err> =
        if Set.isEmpty s
        then Error err
        else Ok (NonEmptySet.ofSet s)

    /// If the set is empty, returns the specified Error.
    /// Otherwise, converts it to a NonEmptySeq wrapped in Ok.
    let toNonEmptySeqResult (err: 'err) (s: 'a Set) : Result<'a NonEmptySeq, 'err> =
        if Set.isEmpty s
        then Error err
        else Ok (NonEmptySeq.ofSeq s)

    /// If the set is empty, returns the specified Error.
    /// Otherwise, converts it to a NonEmptyList wrapped in Ok.
    let toNonEmptyListResult (err: 'err) (s: 'a Set) : Result<'a NonEmptyList, 'err> =
        if Set.isEmpty s
        then Error err
        else Ok (NonEmptyList.ofSeq s)

    let toNonEmptySetOption (s: 'a Set) : Option<'a NonEmptySet> =
        if Set.isEmpty s
        then None
        else Some (NonEmptySet.ofSet s)

    let toNonEmptySeqOption (s: 'a Set) : Option<'a NonEmptySeq> =
        if Set.isEmpty s
        then None
        else Some (NonEmptySeq.ofSeq s)

    let toNonEmptyListOption (s: 'a Set) : Option<'a NonEmptyList> =
        if Set.isEmpty s
        then None
        else Some (NonEmptyList.ofSeq s)
