module ListTestSuite

open Das.Test.Core

let run() =
    testSuite
        "ListTestSuite.fs"
        [
            unitTest 
                "Test lists are equal"
                (fun () -> 
                    [
                        verify
                            "List [1; 2; 3] is equal to list [1; 2; 3]"
                            ((list' [1; 2; 3]) |> is equalTo (list' [1; 2; 3]))
                        
                        verify
                            "List [1; 2; 3] is not equal to list [2; 3]"
                            ((list' [1; 2; 3]) |> isNot equalTo (list' [2; 3]))
                    ]
                )

            unitTest 
                "Test list is empty"
                (fun () -> 
                    [
                        verify
                            "List [] is empty"
                            ((list' []) |> is empty __)
                        
                        verify
                            "List [1; 2; 3] is not empty"
                            ((list' [1; 2; 3]) |> isNot empty __)
                    ]
                )

            unitTest 
                "Test list length is as expected"
                (fun () -> 
                    [
                        verify
                            "The length of list [1; 2; 3] is 3"
                            ((list' [1; 2; 3]) |> has length (int' 3))
                        
                        verify
                            "The length of list [1; 2; 3] is not 4"
                            ((list' [1; 2; 3]) |> doesNotHave length (int' 4))
                    ]
                )
        ]
