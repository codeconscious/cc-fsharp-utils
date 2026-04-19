namespace CCFSharpUtils

open FSharpPlus.Data
open FsToolkit.ErrorHandling
open System

[<RequireQualifiedAccess>]
module List =

    let isNotEmpty (lst: 'a list) : bool =
        not <| List.isEmpty lst

    let anyNotEmpty (lsts: 'a list list) : bool =
        lsts |> List.exists isNotEmpty

    let allNotEmpty (lsts: 'a list list) : bool =
        lsts |> List.forall isNotEmpty

    let doesNotContain (x: 'a) : 'a list -> bool =
        not << List.contains x

    let headElse (alt: 'a) : 'a list -> 'a =
        List.tryHead >> Option.defaultValue alt

    let takeLast (count: int) (lst: 'a list) : 'a list =
        if count <= 0 then
            List.empty
        else
            lst
            |> List.rev
            |> List.truncate count
            |> List.rev

    let hasOne (lst: 'a list) : bool =
        lst |> List.length |> Num.isOne

    let hasMultiple (lst: 'a list) : bool =
        lst |> List.length |> (<) 1

    let ensureOne (emptyErr: 'a) (multipleErr: 'a) (lst: 'b list) : Result<'b list, 'a> =
        match lst with
        | []  -> Error emptyErr
        | [x] -> Ok [x]
        | _   -> Error multipleErr

    /// If the list contains one item, returns it wrapped in Ok. Otherwise, returns the appropriate error.
    /// Intended to be used with FsToolkit.ErrorHandling's validation.
    let ensureOneV xs emptyErr multipleErr : Validation<'a, 'err> =
        match xs with
        | []  -> Error [emptyErr]
        | [x] -> Ok x
        | _   -> Error [multipleErr]

    let ensureSize (targetSize: int) (tooSmallErr: 'err) (tooLargeErr: 'err) (lst: 'a list): Result<'a list, 'err> =
        if Num.isNeg targetSize then
            invalidArg (nameof targetSize) "Target size cannot be negative."

        match compareWith targetSize lst.Length with
        | EQ -> Ok lst
        | LT -> Error tooSmallErr
        | GT -> Error tooLargeErr

    let ensureNotEmptyV xs err : Validation<'a list, 'b> =
        if isNotEmpty xs
        then Ok xs
        else Error [err]

    let tryGetSingle (emptyErr: 'err) (multipleErr: 'err) (lst: 'a list) : Result<'a, 'err> =
        match lst with
        | []  -> Error emptyErr
        | [x] -> Ok x
        | _   -> Error multipleErr

    let containsIgnoreCase (txt: string) (lst: string list) : bool =
        lst |> List.exists (fun x -> String.Equals(x, txt, StringComparison.OrdinalIgnoreCase))

    let anyContainsIgnoreCase (txt: string) : string list list -> bool =
        List.exists (containsIgnoreCase txt)

    /// If the list is empty, returns None. Otherwise, wraps the list in Some.
    let toOption (lst: 'a list) : 'a list option =
        if List.isEmpty lst then None else Some lst

    /// If the list is empty, returns the specified Error. Otherwise, wraps the list in Ok.
    let toResult (err: 'err) (lst: 'a list) : Result<'a list, 'err> =
        if List.isEmpty lst then Error err else Ok lst

    /// If the list is empty, returns the specified Error.
    /// Otherwise, converts it to a NonEmptyList wrapped in Ok.
    let toNonEmptyListResult (err: 'err) : 'a list -> Result<'a NonEmptyList, 'err> =
        function
        | [] -> Error err
        | lst -> Ok (NonEmptyList.ofList lst)

    /// If the list is empty, returns the specified Error.
    /// Otherwise, converts it to a NonEmptySeq wrapped in Ok.
    let toNonEmptySeqResult (err: 'err) : 'a list -> Result<'a NonEmptySeq, 'err> =
        function
        | [] -> Error err
        | lst -> Ok (NonEmptySeq.ofList lst)

    /// If the list is empty, returns the specified Error.
    /// Otherwise, converts it to a NonEmptySet wrapped in Ok.
    let toNonEmptySetResult (err: 'err) : 'a list -> Result<'a NonEmptySet, 'err> =
        function
        | [] -> Error err
        | lst -> Ok (NonEmptySet.ofList lst)

    let toNonEmptyListOption : 'a list -> Option<'a NonEmptyList> =
        function
        | []  -> None
        | lst -> Some (NonEmptyList.ofList lst)

    let toNonEmptySeqOption : 'a list -> Option<'a NonEmptySeq> =
        function
        | []  -> None
        | lst -> Some (NonEmptySeq.ofList lst)

    let toNonEmptySetOption : 'a list -> Option<NonEmptySet<'a>> =
        function
        | []  -> None
        | lst -> Some (NonEmptySet.ofList lst)

    /// Map over the first collection of each pair in a list of tuples, preserving each pair's second collection.
    let mapFst (f : 'a -> 'b) (xs: ('a list * 'c) list) : ('b list * 'c) list =
        xs |> List.map (fun (as_, y) -> List.map f as_, y)

    /// Map over the second collection of each pair in a list of tuples, preserving each pair's first collection.
    let mapSnd (f : 'b -> 'c) (xs: ('a * 'b list) list) : ('a * 'c list) list =
        xs |> List.map (fun (x, ys) -> x, List.map f ys)
