namespace CCFSharpUtils.Tests

open Xunit
open System
open CCFSharpUtils.Library

module StringTests =

    [<Fact>]
    let ``fileLabel formats correctly`` () =
        Assert.True <| (String.fileLabel 0 = "0 files")
        Assert.True <| (String.fileLabel 1 = "1 file")
        Assert.True <| (String.fileLabel 2 = "2 files")
        Assert.True <| (String.fileLabel 1_000_000 = "1,000,000 files")

    [<Fact>]
    let ``fileLabelWithDescriptor formats correctly`` () =
        Assert.True <| (String.fileLabelWithDesc "audio" 0 = "0 audio files")
        Assert.True <| (String.fileLabelWithDesc " temporary " 1 = "1 temporary file")
        Assert.True <| (String.fileLabelWithDesc "deleted" 2 = "2 deleted files")
        Assert.True <| (String.fileLabelWithDesc "image" 1_000_000 = "1,000,000 image files")

    module ReplaceInvalidPathCharsTests =
        open System.IO

        [<Fact>]
        let ``Default replacement '_' replaces invalid path chars`` () =
            let invalids = Path.GetInvalidFileNameChars() |> Array.except ['\000']
            if Array.isEmpty invalids then
                Assert.True(false, "Unexpected environment: not enough invalid filename chars")
            let invalid = invalids[0]
            let input = $"start%c{invalid}end"
            let result = String.replaceInvalidPathChars None None input

            Assert.DoesNotContain(invalid.ToString(), result)
            Assert.Contains("_", result)
            Assert.StartsWith("start", result)
            Assert.EndsWith("end", result)

        [<Fact>]
        let ``Custom invalid chars are replaced with provided replacement`` () =
            let custom = ['#'; '%']
            let input = "abc#def%ghi"
            let result = String.replaceInvalidPathChars (Some '-') (Some custom) input

            Assert.DoesNotContain("#", result)
            Assert.DoesNotContain("%", result)
            Assert.Equal("abc-def-ghi", result)

        [<Fact>]
        let ``Throws when replacement char itself is invalid`` () =
            let invalidReplacement = Array.head <| Path.GetInvalidFileNameChars()
            let input = "has-no-invalid-chars"
            Assert.Throws<ArgumentException>(fun () ->
                String.replaceInvalidPathChars (Some invalidReplacement) None input |> ignore)

        [<Fact>]
        let ``No invalid chars returns identical string`` () =
            let input = "HelloWorld123"
            let result = String.replaceInvalidPathChars None None input
            Assert.Equal(input, result)

        [<Fact>]
        let ``All invalid path and filename chars are replaced`` () =
            let fileInvalid = Path.GetInvalidFileNameChars() |> Array.truncate 3
            let pathInvalid = Path.GetInvalidPathChars() |> Array.truncate 3
            let extras = [| Path.PathSeparator
                            Path.DirectorySeparatorChar
                            Path.AltDirectorySeparatorChar
                            Path.VolumeSeparatorChar |]
            let charsToTest = Array.concat [ fileInvalid; pathInvalid; extras ]
            let input = String charsToTest

            let result = String.replaceInvalidPathChars None None input

            result.ToCharArray() |> Array.iter (fun ch -> Assert.Equal(ch, '_'))
            Assert.Equal(input.Length, result.Length)
