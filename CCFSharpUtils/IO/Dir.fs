namespace CCFSharpUtils.IO

open FSharpPlus.Data
open System.IO

/// Functions pertaining to directories.
[<RequireQualifiedAccess>]
module Dir =

    /// If the directory exists, returns its DirectoryInfo wrapped in Ok.
    /// Otherwise, returns the error wrapped in Error.
    let toDirInfoR (err: 'err) (dirName: string) : Result<DirectoryInfo,'err> =
        if Directory.Exists dirName
        then Ok (DirectoryInfo dirName)
        else Error err

    /// If the directory exists, returns its DirectoryInfo wrapped in Success.
    /// Otherwise, returns the error wrapped in Failure.
    /// Intended to be used with FsToolkit.ErrorHandling's validation.
    let toDirInfoV (err: 'err) (dirName: string) : Validation<'err list, DirectoryInfo> =
        if Directory.Exists dirName
        then Success (DirectoryInfo dirName)
        else Failure [err]
