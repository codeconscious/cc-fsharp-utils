namespace CCFSharpUtils.Library

open FSharpPlus.Data
open System

[<RequireQualifiedAccess>]
module List =

    let isNotEmpty lst =
        not <| List.isEmpty lst

    let anyNotEmpty lsts =
        lsts |> List.exists isNotEmpty

    let allNotEmpty lsts =
        lsts |> List.forall isNotEmpty

    let doesNotContain x =
        not << List.contains x

    let headElse alt =
        List.tryHead >> Option.defaultValue alt

    let takeLast count lst =
        if count <= 0 then
            List.empty
        else
            lst
            |> List.rev
            |> List.truncate count
            |> List.rev

    let hasOne lst =
        lst |> List.length |> Num.isOne

    let hasMultiple lst =
        lst |> List.length |> (<) 1

    let ensureOne emptyErr multipleErr lst =
        match lst with
        | []  -> Error emptyErr
        | [x] -> Ok [x]
        | _   -> Error multipleErr

    let ensureSize targetSize tooSmallErr tooLargeErr (lst: 'b list): Result<'b list,'a> =
        match compareWith targetSize lst.Length with
        | EQ -> Ok lst
        | LT -> Error tooSmallErr
        | GT -> Error tooLargeErr

    let tryGetSingle emptyErr multipleErr lst =
        match lst with
        | []  -> Error emptyErr
        | [x] -> Ok x
        | _   -> Error multipleErr

    let containsIgnoreCase txt (lst: string list) : bool =
        lst |> List.exists (fun x -> String.Equals(x, txt, StringComparison.OrdinalIgnoreCase))

    let anyContainsIgnoreCase txt =
        List.exists (containsIgnoreCase txt)

    /// If the list is empty, returns None. Otherwise, wraps the list in Some.
    let toOption lst = if List.isEmpty lst then None else Some lst

    /// If the list is empty, returns the specified Error. Otherwise, wraps the list in Ok.
    let toResult err lst =
        if List.isEmpty lst then Error err else Ok lst

    /// If the list is empty, returns the specified Error.
    /// Otherwise, converts it to a NonEmptyList wrapped in Ok.
    let toNonEmptyListResult err = function
        | [] -> Error err
        | lst -> Ok (NonEmptyList.ofList lst)

    /// If the list is empty, returns the specified Error.
    /// Otherwise, converts it to a NonEmptySeq wrapped in Ok.
    let toNonEmptySeqResult err = function
        | [] -> Error err
        | lst -> Ok (NonEmptySeq.ofList lst)

    /// If the list is empty, returns the specified Error.
    /// Otherwise, converts it to a NonEmptySet wrapped in Ok.
    let toNonEmptySetResult err = function
        | [] -> Error err
        | lst -> Ok (NonEmptySet.ofList lst)
