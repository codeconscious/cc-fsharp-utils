namespace CCFSharpUtils.Library

open FSharpPlus.Data
open System.IO

/// Functions pertaining to directories.
[<RequireQualifiedAccess>]
module Dir =

    /// If the directory exists, returns its DirectoryInfo wrapped in Ok.
    /// Otherwise, returns the error wrapped in Error.
    let toDirInfoResult (err: 'err) (directoryName: string) : Result<DirectoryInfo,'err> =
        if Directory.Exists directoryName
        then Ok (DirectoryInfo directoryName)
        else Error err
