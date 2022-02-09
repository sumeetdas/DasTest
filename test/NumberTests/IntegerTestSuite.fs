module NumberTests.IntegerTestSuite

open Das.Test.Core

let run () =
    testSuite
        "NumberTests.IntegerTestSuite.fs"
        [
            unitTest 
                "Test an integer is equal to another integer"
                (fun () -> 
                    let num = 4

                    [
                        verify
                            "3 is equal to 3"
                            ((int' 3) |> is equalTo (int' 3))
                        
                        verify
                            "3 is not equal to 4"
                            ((int' 3) |> isNot equalTo (int' 4))
                        
                        verify
                            "num is equal to 4"
                            ((int' num) |> is equalTo (int' 4))
                    ]
                )

            unitTest 
                "Test an integer is greater than another integer"
                (fun () -> 
                    [
                        verify
                            "3 is greater than 2"
                            ((int' 3) |> is greaterThan (int' 2))
                        
                        verify
                            "3 is not greater than than 4"
                            ((int' 3) |> isNot greaterThan (int' 4))
                    ]
                )

            unitTest 
                "Test an integer is less than or equal to another integer"
                (fun () -> 
                    [
                        verify
                            "3 is less than or equal to 3"
                            ((int' 3) |> is lessThanOrEqualTo (int' 3))
                        
                        verify
                            "3 is less than or equal to 4"
                            ((int' 3) |> is lessThanOrEqualTo (int' 4))
                        
                        verify
                            "3 is not less than or equal to 2"
                            ((int' 3) |> isNot lessThanOrEqualTo (int' 2))
                    ]
                )
        ]