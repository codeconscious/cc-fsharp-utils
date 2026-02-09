namespace CCFSharpUtils.Library

open FSharpPlus.Data
open System.IO

/// Functions pertaining to files.
[<RequireQualifiedAccess>]
module File =

    /// If the file exists, returns its FileInfo wrapped in Ok.
    /// Otherwise, returns the error wrapped in Error.
    let toFileInfoResult (err: 'err) (fileName: string) : Result<FileInfo,'err> =
        if File.Exists fileName
        then Ok (FileInfo fileName)
        else Error err
