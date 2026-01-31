namespace CCFSharpUtils.Library

open FSharpPlus.Data
open System
open System.IO

/// Functions pertaining to directories.
[<RequireQualifiedAccess>]
module Directory =
    let verifyExists dir =
        if Directory.Exists dir
        then Success dir
        else Failure ()

/// Functions pertaining to files.
[<RequireQualifiedAccess>]
module File =
    let verifyExists dir =
        if File.Exists dir
        then Success dir
        else Failure ()
