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

    /// If the file exists, returns it wrapped in Success.
    /// Otherwise, returns the error wrapped in Failure.
    /// Intended to be used in applicative validation chains.
    let validateExists (err: 'err) (fileName: string) : Validation<'err, string> =
        if File.Exists fileName
        then Success fileName
        else Failure err

    /// If the file exists, returns its FileInfo wrapped in Success.
    /// Otherwise, returns the error wrapped in Failure.
    /// Intended to be used in applicative validation chains.
    let validateToFileInfo (err: 'err) (fileName: string) : Validation<'err, FileInfo> =
        if File.Exists fileName
        then Success (FileInfo fileName)
        else Failure err
