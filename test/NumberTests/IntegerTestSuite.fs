module NumberTests.IntegerTestSuite

open Das.Test

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
                            (3 |> is equalTo 3)
                        
                        verify
                            "3 is not equal to 4"
                            (3 |> isNot equalTo 4)
                        
                        verify
                            "num is equal to 4"
                            (num |> is equalTo 4)
                    ]
                )

            unitTest 
                "Test an integer is greater than another integer"
                (fun () -> 
                    [
                        verify
                            "3 is greater than 2"
                            (3 |> is greaterThan 2)
                        
                        verify
                            "3 is not greater than than 4"
                            (3 |> isNot greaterThan 4)
                    ]
                )

            unitTest 
                "Test an integer is less than or equal to another integer"
                (fun () -> 
                    [
                        verify
                            "3 is less than or equal to 3"
                            (3 |> is lessThanOrEqualTo 3)
                        
                        verify
                            "3 is less than or equal to 4"
                            (3 |> is lessThanOrEqualTo 4)
                        
                        verify
                            "3 is not less than or equal to 2"
                            (3 |> isNot lessThanOrEqualTo 2)
                    ]
                )
        ]