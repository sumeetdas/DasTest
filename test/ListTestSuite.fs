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
                            ((_list [1; 2; 3]) |> is equalTo (_list [1; 2; 3]))
                        
                        verify
                            "List [1; 2; 3] is not equal to list [2; 3]"
                            ((_list [1; 2; 3]) |> isNot equalTo (_list [2; 3]))
                    ]
                )

            unitTest 
                "Test list is empty"
                (fun () -> 
                    [
                        verify
                            "List [] is empty"
                            ((_list []) |> is empty __)
                        
                        verify
                            "List [1; 2; 3] is not empty"
                            ((_list [1; 2; 3]) |> isNot empty __)
                    ]
                )

            unitTest 
                "Test list length is as expected"
                (fun () -> 
                    [
                        verify
                            "The length of list [1; 2; 3] is 3"
                            ((_list [1; 2; 3]) |> has length (_int 3))
                        
                        verify
                            "The length of list [1; 2; 3] is not 4"
                            ((_list [1; 2; 3]) |> doesNotHave length (_int 4))
                    ]
                )
        ]
