module DasTest

open Das.Test

testSuite 
    "Das.Test"
    [
        unitTest
            "Test that the test results are as expected"
            (fun () ->
                let expectedResult = 
                    """
3/3 unit tests passed in test suite `NumberTests.IntegerTestSuite.fs`.

2/2 unit tests passed in test suite `NumberTests.FloatTestSuite.fs`.

5/5 unit tests passed in test suite `StringTestSuite.fs`.

3/3 unit tests passed in test suite `ListTestSuite.fs`.

1/1 unit tests passed in test suite `ValueTestSuite.fs`.
                    """

                let actualResult = 
                    [
                        NumberTests.IntegerTestSuite.run()
                        NumberTests.FloatTestSuite.run()
                        StringTestSuite.run()
                        ListTestSuite.run()
                        ValueTestSuite.run()
                    ] 
                    |> String.concat "\n"

                let processString (str: string) = 
                    str.Trim().Split "\n" 
                    |> Array.map (fun str -> str.Trim())
                    |> Array.filter (fun str -> not (str.Length = 0))
                    |> String.concat "\n"

                let expectedResult = expectedResult |> processString
                let actualResult = actualResult |> processString
                
                [
                    verify
                        "Test result is same as expected"
                        ((string' actualResult) |> is equalTo (string' expectedResult))
                ]
            )
    ]
|> printfn "%s"
