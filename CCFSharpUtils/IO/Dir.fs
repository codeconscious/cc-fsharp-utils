namespace CCFSharpUtils.IO

open CCFSharpUtils.Collections
open FSharpPlus.Data
open System.IO

/// Functions pertaining to directories.
[<RequireQualifiedAccess>]
module Dir =

    let dirMissing str = not (Directory.Exists str)

    let private isEmptyRecursiveWith attributesToSkip dirPath =
        let options = EnumerationOptions()
        options.RecurseSubdirectories <- true
        options.AttributesToSkip <- attributesToSkip

        Directory.EnumerateFileSystemEntries(dirPath, "*", options)
        |> Seq.isEmpty

    /// <summary>
    /// Determines whether the directory and its subdirectories contain no file-system entries,
    /// including hidden and system entries.
    /// </summary>
    let isEmpty dirPath =
        isEmptyRecursiveWith FileAttributes.None dirPath

    /// <summary>
    /// Determines whether the directory and its subdirectories contain no entries other than
    /// hidden or system entries.
    /// </summary>
    let isEmpty' dirPath =
        isEmptyRecursiveWith
            (FileAttributes.Hidden ||| FileAttributes.System)
            dirPath

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
