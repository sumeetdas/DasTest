module StringTestSuite

open Das.Test.Core

let run () =
    testSuite
        "StringTestSuite.fs"
        [
            unitTest 
                "Test strings are equal"
                (fun () -> 
                    [
                        verify
                            "\"jpeg\" is equal to \"jpeg\""
                            ((_string "jpeg") |> is equalTo (_string "jpeg"))
                        
                        verify
                            "\"jpeg\" isNot equal to \"peg\""
                            ((_string "jpeg") |> isNot equalTo (_string "peg"))
                    ]
                )

            unitTest 
                "Test string contains another string"
                (fun () -> 
                    [
                        verify
                            "\"banana\" contains \"ana\""
                            ((_string "banana") |> does contain (_string "ana"))

                        verify
                            "\"banana\" does not contains \"xyz\""
                            ((_string "banana") |> doesNot contain (_string "xyz"))
                    ]
                )
            
            unitTest 
                "Test string starts with another string"
                (fun () -> 
                    [
                        verify
                            "\"banana\" does start with \"ban\""
                            ((_string "banana") |> does startWith (_string "ban"))
                        
                        verify
                            "\"banana\" does not start with \"ana\""
                            ((_string "banana") |> doesNot startWith (_string "ana"))
                    ]
                )

            unitTest 
                "Test string ends with another string"
                (fun () -> 
                    [
                        verify
                            "\"banana\" contains \"ana\""
                            ((_string "banana") |> does endWith (_string "ana"))
                        
                        verify
                            "\"banana\" does not end with \"xyz\""
                            ((_string "banana") |> does endWith (_string "ana"))
                    ]
                )

            unitTest 
                "Test string is empty"
                (fun () -> 
                    [
                        verify
                            "\"\" is empty"
                            ((_string "") |> is empty __)
                        
                        verify
                            "\"banana\" is not empty"
                            ((_string "banana") |> isNot empty __)
                    ]
                )
        ]