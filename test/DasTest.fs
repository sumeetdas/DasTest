module DasTest

open Das.Test.Core

testSuite 
    "Das.Test"
    [
        unitTest
            "Test that the test results are as expected"
            (fun () ->
                let expectedResult = 
                    """
                    """

                let actualResult = 
                    [
                        NestedSuite.TestSuite1.run()
                        NestedSuite.TestSuite2.run()
                        AnotherSuite.run()
                        MainSuite.run()
                    ]
                
                [
                    verify
                        "Test result is same as expected"
                        (actualResult |> _string is equalTo expectedResult)
                ]
            )
    ]
|> printfn "%s"
