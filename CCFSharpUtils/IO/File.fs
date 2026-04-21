namespace CCFSharpUtils

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

    /// Reads the last n lines from a text file without loading the entire file into memory at once.
    /// (It reads through the entire file sequentially, but only the most current n lines are in memory at any given time.)
    /// The last n lines are returned in their original order. Returns an empty list if count <= 0.
    /// If count exceeds the number of lines in the file, all lines are returned.
    /// If the file does not exist or cannot be read, returns the exception wrapped in an Error.
    let readLastNLines (filePath: string) (lineCount: int) : Result<string list, exn> =
        try Ok <|
                if Num.isNeg lineCount then
                    []
                else
                    let queue = Collections.Generic.Queue<string>()
                    use reader = File.OpenText filePath

                    let rec processLines () =
                        let line = reader.ReadLine()
                        if line <> null then
                            queue.Enqueue line
                            if queue.Count > lineCount then
                                queue.Dequeue() |> ignore
                            processLines ()

                    processLines ()
                    queue |> Seq.toList
        with ex -> Error ex

    let readLastNLines' (fileInfo: FileInfo) (count: int) : Result<string list,exn> =
        readLastNLines fileInfo.FullName count

    let writeText (path: string) (text: string) : Result<unit, string> =
        try Ok <| File.WriteAllText(path, text)
        with ex -> Error ex.Message

    let writeText' (path: FileInfo) (text: string) : Result<unit, string> =
        try Ok <| File.WriteAllText(path.FullName, text)
        with ex -> Error ex.Message

    let writeLines (path: string) (lines: string seq) : Result<unit, string> =
        try Ok <| File.WriteAllLines(path, lines)
        with ex -> Error ex.Message

    let writeLines' (path: FileInfo) (lines: string seq) : Result<unit, string> =
        try Ok <| File.WriteAllLines(path.FullName, lines)
        with ex -> Error ex.Message

    /// If the file exists, returns its FileInfo wrapped in Ok.
    /// Otherwise, returns the error wrapped in Error.
    let toFileInfoR (err: 'err) (fileName: string) : Result<FileInfo,'err> =
        if File.Exists fileName
        then Ok (FileInfo fileName)
        else Error err

    /// If the file exists, returns its FileInfo wrapped in Success.
    /// Otherwise, returns an error list wrapped in Failure.
    /// Intended to be used with FsToolkit.ErrorHandling's validation.
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
