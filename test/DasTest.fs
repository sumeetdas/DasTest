module DasTest

open Das.Test.Core

testSuite 
    "Das.Test"
    [
        unitTest
            "Test that the test results are as expected"
            (fun () ->
                let expectedResult = ""

                let actualResult = 
                    [
                        NumberTests.IntegerTestSuite.run()
                        NumberTests.FloatTestSuite.run()
                        StringTestSuite.run()
                        MainSuite.run()
                    ] 
                    |> String.concat "\n"
                
                [
                    verify
                        "Test result is same as expected"
                        ((_string actualResult) |> is equalTo (_string expectedResult))
                ]
            )
    ]
|> printfn "%s"
