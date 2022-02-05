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
                        NestedSuite.TestSuite1.run()
                        NestedSuite.TestSuite2.run()
                        AnotherSuite.run()
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
