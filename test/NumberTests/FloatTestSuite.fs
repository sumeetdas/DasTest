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
                            ((float' 3.2) |> is greaterThan (float' 3.12))
                        
                        verify
                            "3.2 is not greater than 3.21"
                            ((float' 3.2) |> isNot greaterThan (float' 3.21))
                    ]
                )

            unitTest 
                "Test float number is less than another float number"
                (fun () -> 
                    [
                        verify
                            "3.2 is less than than 3.21"
                            ((float' 3.2) |> is lessThan (float' 3.21))
                        
                        verify
                            "3.2 is not less than 3.201"
                            ((float' 3.2) |> isNot lessThan (float' 3.19))
                    ]
                )
        ]