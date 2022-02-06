module NumberTests.IntegerTestSuite

open Das.Test.Core

let run () =
    testSuite
        "NumberTests.IntegerTestSuite.fs"
        [
            unitTest 
                "Test an integer is equal to another integer"
                (fun () -> 
                    [
                        verify
                            "3 is equal to 3"
                            ((_int 3) |> is equalTo (_int 3))
                        
                        verify
                            "3 is not equal to 4"
                            ((_int 3) |> isNot equalTo (_int 4))
                    ]
                )

            unitTest 
                "Test an integer is greater than another integer"
                (fun () -> 
                    [
                        verify
                            "3 is greater than 2"
                            ((_int 3) |> is greaterThan (_int 2))
                        
                        verify
                            "3 is not greater than than 4"
                            ((_int 3) |> isNot greaterThan (_int 4))
                    ]
                )

            unitTest 
                "Test an integer is less than or equal to another integer"
                (fun () -> 
                    [
                        verify
                            "3 is less than or equal to 3"
                            ((_int 3) |> is lessThanOrEqualTo (_int 3))
                        
                        verify
                            "3 is less than or equal to 4"
                            ((_int 3) |> is lessThanOrEqualTo (_int 4))
                        
                        verify
                            "3 is not less than or equal to 2"
                            ((_int 3) |> isNot lessThanOrEqualTo (_int 2))
                    ]
                )
        ]