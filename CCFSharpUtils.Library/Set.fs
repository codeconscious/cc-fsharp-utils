namespace CCFSharpUtils.Library

open System

[<RequireQualifiedAccess>]
module Set =

    let isNotEmpty s =
        not <| Set.isEmpty s

    let anyNotEmpty ss =
        ss |> Set.exists isNotEmpty

    let allNotEmpty ss =
        ss |> Set.forall isNotEmpty

    let doesNotContain x =
        not << Set.contains x

    let hasOne s =
        s |> Set.count |> Num.isOne

    let hasMultiple s =
        s |> Set.count |> (<) 1

    let ensureOne emptyErr multipleErr (s: 'b Set) :  Result<'b Set,'a> =
        if Set.isEmpty s then
            Error emptyErr
        elif hasOne s then
            Ok s
        else Error multipleErr

    let ensureSize targetSize tooSmallErr tooLargeErr (s: 'b Set) : Result<'b Set,'a> =
        let length = Set.count s
        match compareWith targetSize length with
        | EQ -> Ok s
        | LT -> Error tooSmallErr
        | GT -> Error tooLargeErr

    let tryGetSingle emptyErr multipleErr s =
        if hasOne s then
            Ok (s |> Set.minElement)
        elif hasMultiple s then
            Error multipleErr
        else
            Error emptyErr

    let containsIgnoreCase txt (s: string Set) : bool =
        s |> Set.exists (fun x -> String.Equals(x, txt, StringComparison.OrdinalIgnoreCase))

    let anyContainsIgnoreCase txt =
        Set.exists (containsIgnoreCase txt)

    /// If the set is empty, returns None. Otherwise, wraps the set in Some.
    let toOption s = if Set.isEmpty s then None else Some s

    /// If the set is empty, returns the specified Error. Otherwise, wraps the set in Ok.
    let toResult err s =
        if Set.isEmpty s then Error err else Ok s
