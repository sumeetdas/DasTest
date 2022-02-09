module NumberTests.FloatTestSuite

open Das.Test

let run () =
    testSuite
        "NumberTests.FloatTestSuite.fs"
        [
            unitTest 
                "Test float number is greater than another float number"
                (fun () -> 
                    [
                        verify
                            "3.2 is greater than 3.12"
                            (3.2 |> is greaterThan 3.12)
                        
                        verify
                            "3.2 is not greater than 3.21"
                            (3.2 |> isNot greaterThan 3.21)
                    ]
                )

            unitTest 
                "Test float number is less than another float number"
                (fun () -> 
                    [
                        verify
                            "3.2 is less than than 3.21"
                            (3.2 |> is lessThan 3.21)
                        
                        verify
                            "3.2 is not less than 3.201"
                            (3.2 |> isNot lessThan 3.19)
                    ]
                )
        ]