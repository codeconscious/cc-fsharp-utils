namespace CCFSharpUtils

open FSharpPlus.Data
open System

[<RequireQualifiedAccess>]
module NonEmptySet =

    let doesNotContain (x: 'a) : 'a nset -> bool =
        not << NonEmptySet.contains x

    let hasOne (xs: 'a nset) : bool =
        Num.isOne xs.Count

    let hasMultiple (xs: 'a nset) : bool =
        xs |> NonEmptySet.count |> (<) 1

    let ensureOne (multipleErr: 'a) (xs: 'b nset) : Result<'b nset, 'a> =
        if Num.isOne xs.Count
        then Ok xs
        else Error multipleErr

    let ensureSize (targetSize: int) (tooSmallErr: 'err) (tooLargeErr: 'err) (xs: 'a nset): Result<'a nset, 'err> =
        if Num.isNeg targetSize then
            invalidArg (nameof targetSize) "Target size cannot be negative."

        match xs.Count |> compareWith targetSize with
        | EQ -> Ok xs
        | LT -> Error tooSmallErr
        | GT -> Error tooLargeErr

    let tryGetSingle(multipleErr: 'err) (xs: 'a nset) : Result<'a, 'err> =
        if Num.isOne xs.Count
        then Ok xs.MinimumElement
        else Error multipleErr

    let containsIgnoreCase (txt: string) (xs: string nset) : bool =
        xs |> NonEmptySet.exists (fun x -> String.Equals(x, txt, StringComparison.OrdinalIgnoreCase))

    let anyContainsIgnoreCase (txt: string) : string nset nset -> bool =
        NonEmptySet.exists (containsIgnoreCase txt)
