namespace CCFSharpUtils.Tests

open System
open System.IO
open Xunit
open CCFSharpUtils.IO

module DirTests =

    let private withTempDirectory test =
        let path = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N"))

        Directory.CreateDirectory(path) |> ignore

        try
            test path
        finally
            Directory.Delete(path, true)

    [<Fact>]
    let ``isEmpty returns true for an empty directory`` () =
        withTempDirectory (fun path ->
            Assert.True <| Dir.isEmpty path)

    [<Fact>]
    let ``isEmpty returns false when a subdirectory contains an entry`` () =
        withTempDirectory (fun path ->
            let nestedPath = Path.Combine(path, "nested")
            Directory.CreateDirectory(nestedPath) |> ignore
            File.WriteAllText(Path.Combine(nestedPath, "file.txt"), "content")

            Assert.False <| Dir.isEmpty path)

    [<Fact>]
    let ``isEmpty' returns true for an empty directory`` () =
        withTempDirectory (fun path ->
            Assert.True <| Dir.isEmpty' path)

    [<Fact>]
    let ``isEmpty' returns false when a subdirectory contains an ordinary entry`` () =
        withTempDirectory (fun path ->
            let nestedPath = Path.Combine(path, "nested")
            Directory.CreateDirectory(nestedPath) |> ignore
            File.WriteAllText(Path.Combine(nestedPath, "file.txt"), "content")

            Assert.False <| Dir.isEmpty' path)

    [<Fact>]
    let ``isEmpty' returns true when the only file is hidden`` () =
        withTempDirectory (fun path ->
            let filePath = Path.Combine(path, "hidden.txt")
            File.WriteAllText(filePath, "content")
            File.SetAttributes(filePath, FileAttributes.Hidden)

            Assert.True <| Dir.isEmpty' path)
