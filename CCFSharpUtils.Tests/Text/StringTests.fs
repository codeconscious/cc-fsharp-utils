namespace CCFSharpUtils.Tests.Text

open Xunit
open System
open CCFSharpUtils.Text

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

    module TrimCombinedLinesTests =

        let nl = String.nl

        [<Fact>]
        let ``Trims leading and trailing spaces from each line`` () =
            let input = $"  a  {nl} b {nl}c   "
            let expected = $"a{nl}b{nl}c"
            let result = String.trimCombinedLines input

            Assert.Equal(expected, result)

        [<Fact>]
        let ``Preserves internal spaces and tabs but trims line ends`` () =
            let input = $"  a  b\tc  {nl}\t d e\t "
            let expected = $"a  b\tc{nl}d e"
            let result = String.trimCombinedLines input

            Assert.Equal(expected, result)

        [<Fact>]
        let ``Empty input returns empty string`` () =
            Assert.Equal(nl, String.trimCombinedLines nl)

        [<Fact>]
        let ``Lines containing only whitespace become empty lines`` () =
            let input = $"   {nl}\t{nl}x"
            let expected = $"{nl}{nl}x"
            let actual = String.trimCombinedLines input

            Assert.Equal(expected, actual)

    module RemoveDiacriticsTests =
        let input = "áéíóúÁÉÍÓÚ ăĂ çÇ ñÑ ěĚ šŠ žŽ řŘ ďĎ ťŤ ůŮ"
        let actual = String.stripDiacritics input
        let expected = "aeiouAEIOU aA cC nN eE sS zZ rR dD tT uU"
        Assert.Equal(expected, actual)

    module FormatNumberTests =

        /// A tiny custom type that implements the required ToString signature.
        type MyCustomNum(i: int) =
            member _.ToString(fmt: string, provider: IFormatProvider) =
                i.ToString(fmt, provider)

        [<Fact>]
        let ``format int`` () =
            let actual = String.formatNumber 123456
            Assert.Equal("123,456", actual)

        [<Fact>]
        let ``format negative int`` () =
            let actual = String.formatNumber -1234
            Assert.Equal("-1,234", actual)

        [<Fact>]
        let ``format zero`` () =
            let actual = String.formatNumber 0
            Assert.Equal("0", actual)

        [<Fact>]
        let ``format int64`` () =
            let actual = String.formatNumber 1234567890L
            Assert.Equal("1,234,567,890", actual)

        [<Fact>]
        let ``format decimal rounds to integer display`` () =
            let actual = String.formatNumber 123456.78M
            Assert.Equal("123,457", actual)

        [<Fact>]
        let ``format float rounds to integer display`` () =
            let actual = String.formatNumber 123456.78
            Assert.Equal("123,457", actual)

        [<Fact>]
        let ``format negative float rounds to integer display`` () =
            let actual = String.formatNumber -1234.56
            Assert.Equal("-1,235", actual)

        [<Fact>]
        let ``format custom numeric type`` () =
            let myNum = MyCustomNum 1234
            let actual = String.formatNumber myNum
            Assert.Equal("1,234", actual)

    module PluralTests =

        [<Fact>]
        let ``plur with S adds 's' for zero and plural counts`` () =
            Assert.True <| (String.plur "cat" S 0 = "cats")
            Assert.True <| (String.plur "cat" S 2 = "cats")
            Assert.True <| (String.plur "cat" S 1_000_000 = "cats")

        [<Fact>]
        let ``plur with S returns singular for one`` () =
            Assert.True <| (String.plur "cat" S 1 = "cat")

        [<Fact>]
        let ``plur with Es adds 'es' for zero and plural counts`` () =
            Assert.True <| (String.plur "box" Es 0 = "boxes")
            Assert.True <| (String.plur "box" Es 2 = "boxes")
            Assert.True <| (String.plur "box" Es 1_000_000 = "boxes")

        [<Fact>]
        let ``plur with Es returns singular for one`` () =
            Assert.True <| (String.plur "box" Es 1 = "box")

        [<Fact>]
        let ``plur with Same returns the word unchanged`` () =
            Assert.True <| (String.plur "sheep" Same 0 = "sheep")
            Assert.True <| (String.plur "sheep" Same 1 = "sheep")
            Assert.True <| (String.plur "sheep" Same 2 = "sheep")

        [<Fact>]
        let ``plur with Irreg returns the irregular form for zero and plural counts`` () =
            Assert.True <| (String.plur "child" (Irreg "children") 0 = "children")
            Assert.True <| (String.plur "child" (Irreg "children") 2 = "children")
            Assert.True <| (String.plur "child" (Irreg "children") 1_000_000 = "children")

        [<Fact>]
        let ``plur with Irreg returns the singular word for one`` () =
            Assert.True <| (String.plur "child" (Irreg "children") 1 = "child")

        [<Fact>]
        let ``plur treats negative one as singular and other negatives as plural`` () =
            Assert.True <| (String.plur "cat" S (-1) = "cat")
            Assert.True <| (String.plur "box" Es (-1) = "box")
            Assert.True <| (String.plur "child" (Irreg "children") (-1) = "child")
            Assert.True <| (String.plur "cat" S (-2) = "cats")
            Assert.True <| (String.plur "box" Es (-2) = "boxes")
            Assert.True <| (String.plur "child" (Irreg "children") (-2) = "children")

        [<Fact>]
        let ``plur works with int64 counts`` () =
            Assert.True <| (String.plur "file" S 1L = "file")
            Assert.True <| (String.plur "file" S 3_000_000_000L = "files")

        [<Fact>]
        let ``plur' formats count and pluralizes with S`` () =
            Assert.True <| (String.plur' "file" S 0 = "0 files")
            Assert.True <| (String.plur' "file" S 1 = "1 file")
            Assert.True <| (String.plur' "file" S 2 = "2 files")
            Assert.True <| (String.plur' "file" S 1_000_000 = "1,000,000 files")

        [<Fact>]
        let ``plur' formats count and pluralizes with Es`` () =
            Assert.True <| (String.plur' "box" Es 0 = "0 boxes")
            Assert.True <| (String.plur' "box" Es 1 = "1 box")
            Assert.True <| (String.plur' "box" Es 1_000 = "1,000 boxes")

        [<Fact>]
        let ``plur' formats count and leaves Same words unchanged`` () =
            Assert.True <| (String.plur' "sheep" Same 0 = "0 sheep")
            Assert.True <| (String.plur' "sheep" Same 1 = "1 sheep")
            Assert.True <| (String.plur' "sheep" Same 1_000 = "1,000 sheep")

        [<Fact>]
        let ``plur' formats count and uses irregular form`` () =
            Assert.True <| (String.plur' "child" (Irreg "children") 0 = "0 children")
            Assert.True <| (String.plur' "child" (Irreg "children") 1 = "1 child")
            Assert.True <| (String.plur' "child" (Irreg "children") 1_500_000 = "1,500,000 children")

        [<Fact>]
        let ``plur' formats negative counts with a leading minus sign`` () =
            Assert.True <| (String.plur' "file" S (-1) = "-1 file")
            Assert.True <| (String.plur' "file" S (-2) = "-2 files")
            Assert.True <| (String.plur' "file" S (-1_000) = "-1,000 files")

        [<Fact>]
        let ``plur' works with int64 counts`` () =
            Assert.True <| (String.plur' "file" S 1L = "1 file")
            Assert.True <| (String.plur' "file" S 3_000_000_000L = "3,000,000,000 files")

        [<Fact>]
        let ``plur works with uint16 counts`` () =
            Assert.True <| (String.plur "cat" S 1us = "cat")
            Assert.True <| (String.plur "cat" S 2us = "cats")
            Assert.True <| (String.plur' "file" S 1_000us = "1,000 files")
