namespace CCFSharpUtils

open System
open FSharpPlus
open FSharpPlus.Data

[<AutoOpen>]
module Common =

    /// Represents the ordering relationship between two compared values.
    type Ordering =
        /// The value is less than the target.
        | LT
        /// The values are equal.
        | EQ
        /// The value is greater than the target.
        | GT

    /// Helper for try/with -> Result.
    let ofTry (f: unit -> 'a) : Result<'a, string> =
        try Ok (f())
        with exn -> Error exn.Message

    /// Helper for try/with -> Validation.
    let ofTry' (f: unit -> 'a) : Validation<string list, 'a> =
        try Success (f())
        with exn -> Failure [exn.Message]

    /// Compares a subject value against a target value and returns their ordering relationship.
    ///
    /// Returns:
    /// LT if subject is less than target, EQ if equal, GT if subject is greater than target.
    ///
    /// Examples:
    /// compare' 3 5  // Returns LT (3 < 5)
    /// compare' 5 5  // Returns EQ (5 = 5)
    /// compare' 7 5  // Returns GT (7 > 5)
    let compare' (subject: 'a) (target: 'a) : Ordering =
        match subject </compare/> target with
        | n when n < 0 -> LT
        | 0 -> EQ
        | _ -> GT
