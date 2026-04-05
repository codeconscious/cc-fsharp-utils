namespace CCFSharpUtils.Library

[<RequireQualifiedAccess>]
module Tuple =

    /// Return the first element of a 3-tuple.
    let fst3 (a, _, _) = a

    /// Return the second element of a 3-tuple.
    let snd3 (_, b, _) = b

    /// Return the third element of a 3-tuple.
    let trd3 (_, _, c) = c

    /// Return the first element of a 4-tuple.
    let fst4 (a, _, _, _) = a

    /// Return the second element of a 4-tuple.
    let snd4 (_, b, _, _) = b

    /// Return the third element of a 4-tuple.
    let trd4 (_, _, c, _) = c

    /// Return the fourth element of a 4-tuple.
    let fth4 (_, _, _, d) = d

    /// Return the first element of a 5-tuple.
    let fst5 (a, _, _, _, _) = a

    /// Return the second element of a 5-tuple.
    let snd5 (_, b, _, _, _) = b

    /// Return the third element of a 5-tuple.
    let trd5 (_, _, c, _, _) = c

    /// Return the fourth element of a 5-tuple.
    let fth5 (_, _, _, d, _) = d

    /// Return the fifth element of a 5-tuple.
    let fif5 (_, _, _, _, e) = e
