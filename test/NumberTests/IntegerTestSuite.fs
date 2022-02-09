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
                            (val' 3 |> is equalTo (val' 3))
                        
                        verify
                            "3 is not equal to 4"
                            (val' 3 |> isNot equalTo (val' 4))
                        
                        verify
                            "num is equal to 4"
                            (val' num |> is equalTo (val' 4))
                    ]
                )

            unitTest 
                "Test an integer is greater than another integer"
                (fun () -> 
                    [
                        verify
                            "3 is greater than 2"
                            (val' 3 |> is greaterThan (val' 2))
                        
                        verify
                            "3 is not greater than than 4"
                            (val' 3 |> isNot greaterThan (val' 4))
                    ]
                )

            unitTest 
                "Test an integer is less than or equal to another integer"
                (fun () -> 
                    [
                        verify
                            "3 is less than or equal to 3"
                            (val' 3 |> is lessThanOrEqualTo (val' 3))
                        
                        verify
                            "3 is less than or equal to 4"
                            (val' 3 |> is lessThanOrEqualTo (val' 4))
                        
                        verify
                            "3 is not less than or equal to 2"
                            (val' 3 |> isNot lessThanOrEqualTo (val' 2))
                    ]
                )
        ]