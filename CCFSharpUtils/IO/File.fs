namespace CCFSharpUtils.Library

open FSharpPlus.Data
open System
open System.IO

/// Functions pertaining to files.
[<RequireQualifiedAccess>]
module File =

    /// Reads all text from the specified file, returning a Result.
    /// If an exception is thrown during the underlying operation,
    /// the Error includes the exception itself.
    let readText (filePath: string) : Result<string, string> =
        ofTry (fun _ -> File.ReadAllText filePath)

    let readText' (fileInfo: FileInfo) : Result<string, string> =
        readText fileInfo.FullName

    let readLines (filePath: string) : Result<string array, string> =
        ofTry (fun _ -> File.ReadAllLines filePath)

    let readLines' (fileInfo: FileInfo) : Result<string array, string> =
        readLines fileInfo.FullName

    /// Write text to the file at the given path.
    let writeText (path: string) (text: string) : Result<FileInfo, string> =
        try
            File.WriteAllText(path, text) |> ignore
            Ok (FileInfo path)
        with ex -> Error ex.Message

    /// Write text to the file represented by the FileInfo.
    let writeText' (file: FileInfo) (text: string) : Result<FileInfo, string> =
        try
            File.WriteAllText(file.FullName, text) |> ignore
            Ok file
        with ex -> Error ex.Message

    /// Write a sequence of lines to the file at the given path.
    let writeLines (path: string) (lines: string seq) : Result<FileInfo, string> =
        try
            File.WriteAllLines(path, lines) |> ignore
            Ok (FileInfo path)
        with ex -> Error ex.Message

    /// Write sequence of lines to the file represented by the FileInfo.
    let writeLines' (file: FileInfo) (lines: string seq) : Result<FileInfo, string> =
        try
            File.WriteAllLines(file.FullName, lines) |> ignore
            Ok file
        with ex -> Error ex.Message


    /// If the file exists, returns its FileInfo wrapped in Ok.
    /// Otherwise, returns the error wrapped in Error.
    let toFileInfoR (err: 'err) (fileName: string) : Result<FileInfo,'err> =
        if File.Exists fileName
        then Ok (FileInfo fileName)
        else Error err

    /// If the file exists, returns its FileInfo wrapped in Success.
    /// Otherwise, returns an error list wrapped in Failure.
    /// Intended to be used in applicative validation chains. (Thus, the list.)
    let toFileInfoV (err: 'err) (file: string) : Validation<'err list, FileInfo> =
        if File.Exists file
        then Success (FileInfo file)
        else Failure [err]

    let backUpWithTimestamp (dateTimeFormat: string) (fileInfo: FileInfo) : Result<FileInfo, string> =
        if not fileInfo.Exists then
            Error "Source file does not exist, so it cannot be backed up."
        else
            let generateBackUpFilePath () : string =
                let baseName = Path.GetFileNameWithoutExtension fileInfo.Name
                let nowText = DateTimeOffset.Now.ToString dateTimeFormat
                let extension = fileInfo.Extension // Includes the initial period.
                let fileName = $"%s{baseName}.%s{nowText}_backup%s{extension}"
                Path.Combine(fileInfo.DirectoryName, fileName)

            try
                generateBackUpFilePath()
                |> fileInfo.CopyTo
                |> Ok
            with ex -> Error ex.Message
