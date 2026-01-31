namespace CCFSharpUtils.Library

open FSharpPlus.Data
open System
open System.IO

/// Functions pertaining to directories.
[<RequireQualifiedAccess>]
module Directory =
    let verifyExists err dir =
        if Directory.Exists dir
        then Success dir
        else Failure err

/// Functions pertaining to files.
[<RequireQualifiedAccess>]
module File =
    let verifyExists err dir =
        if File.Exists dir
        then Success dir
        else Failure err
