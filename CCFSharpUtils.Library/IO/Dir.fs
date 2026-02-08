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

    /// If the directory exists, returns it wrapped in Success.
    /// Otherwise, returns the error wrapped in Failure.
    /// Intended to be used in applicative validation chains.
    let validateExists (err: 'err) (directoryName: string) : Validation<'err, string> =
        if Directory.Exists directoryName
        then Success directoryName
        else Failure err

    /// If the directory exists, returns its DirectoryInfo wrapped in Success.
    /// Otherwise, returns the error wrapped in Failure.
    /// Intended to be used in applicative validation chains.
    let validateToDirInfo (err: 'err) (directoryName: string) : Validation<'err, DirectoryInfo> =
        if Directory.Exists directoryName
        then Success (DirectoryInfo directoryName)
        else Failure err
