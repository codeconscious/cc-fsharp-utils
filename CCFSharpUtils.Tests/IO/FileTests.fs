namespace CCFSharpUtils.Tests

open Xunit
open System
open System.IO
open CCFSharpUtils

module FileTests =

    module ReadLastNLines =

        let private createTestFile (lines: string list) : string =
            let tempFile = Path.GetTempFileName()
            File.WriteAllLines(tempFile, lines)
            tempFile

        let private cleanupTestFile (filePath: string) : unit =
            if File.Exists filePath then File.Delete filePath

        let private threeLines = [ "line1"; "line2"; "line3" ]

        [<Fact>]
        let ``returns last N lines when file has enough lines`` () =
            let lines = [ "line1"; "line2"; "line3"; "line4"; "line5" ]
            let testFile = createTestFile lines
            try
                let result = File.readLastNLines testFile 3
                let expected = [ "line3"; "line4"; "line5" ]
                Assert.Equal<string list>(expected, result)
            finally
                cleanupTestFile testFile

        [<Fact>]
        let ``returns all lines when count equals file line count`` () =
            let testFile = createTestFile threeLines
            try
                let result = File.readLastNLines testFile 3
                Assert.Equal<string list>(threeLines, result)
            finally
                cleanupTestFile testFile

        [<Fact>]
        let ``returns entire file when count exceeds number of lines`` () =
            let testFile = createTestFile threeLines
            try
                let result = File.readLastNLines testFile 10
                Assert.Equal<string list>(threeLines, result)
            finally
                cleanupTestFile testFile

        [<Fact>]
        let ``returns empty list when file is empty`` () =
            let testFile = createTestFile []
            try
                let result = File.readLastNLines testFile 5
                Assert.Empty result
            finally
                cleanupTestFile testFile

        [<Fact>]
        let ``handles blank lines correctly`` () =
            let lines = [ "line1"; ""; "line3"; ""; "line5" ]
            let testFile = createTestFile lines
            try
                let result = File.readLastNLines testFile 3
                let expected = [ "line3"; ""; "line5" ]
                Assert.Equal<string list>(expected, result)
            finally
                cleanupTestFile testFile

        [<Fact>]
        let ``handles UTF-8 and special characters`` () =
            let lines = [ "café"; "日本語"; "line with\ttabs" ]
            let testFile = createTestFile lines
            try
                let result = File.readLastNLines testFile 2
                let expected = [ "日本語"; "line with\ttabs" ]
                Assert.Equal<string list>(expected, result)
            finally
                cleanupTestFile testFile

        [<Fact>]
        let ``returns empty list when count is zero`` () =
            let testFile = createTestFile threeLines
            try
                let result = File.readLastNLines testFile 0
                Assert.Empty result
            finally
                cleanupTestFile testFile

        [<Fact>]
        let ``returns empty list when count is negative`` () =
            let testFile = createTestFile threeLines
            try
                let result = File.readLastNLines testFile -5
                Assert.Empty result
            finally
                cleanupTestFile testFile

        [<Fact>]
        let ``returns single line when count is one`` () =
            let testFile = createTestFile threeLines
            try
                let result = File.readLastNLines testFile 1
                let expected = [ "line3" ]
                Assert.Equal<string list>(expected, result)
            finally
                cleanupTestFile testFile

        [<Fact>]
        let ``throws when file does not exist`` () =
            let nonExistentFile = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString(), "nonexistent.txt")
            Assert.ThrowsAny<IOException>(fun () ->
                File.readLastNLines nonExistentFile 5 |> ignore)
