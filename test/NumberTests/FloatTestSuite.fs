module NumberTests.FloatTestSuite

open Das.Test.Core

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
                            ((_float 3.2) |> is greaterThan (_float 3.12))
                        
                        verify
                            "3.2 is not greater than 3.21"
                            ((_float 3.2) |> isNot greaterThan (_float 3.21))
                    ]
                )

            unitTest 
                "Test float number is less than another float number"
                (fun () -> 
                    [
                        verify
                            "3.2 is less than than 3.21"
                            ((_float 3.2) |> is lessThan (_float 3.21))
                        
                        verify
                            "3.2 is not less than 3.201"
                            ((_float 3.2) |> isNot lessThan (_float 3.201))
                    ]
                )
        ]