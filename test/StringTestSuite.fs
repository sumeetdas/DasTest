module StringTestSuite

open Das.Test

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
                            ((string' "jpeg") |> is equalTo (string' "jpeg"))
                        
                        verify
                            "\"jpeg\" isNot equal to \"peg\""
                            ((string' "jpeg") |> isNot equalTo (string' "peg"))
                    ]
                )

            unitTest 
                "Test string contains another string"
                (fun () -> 
                    [
                        verify
                            "\"banana\" contains \"ana\""
                            ((string' "banana") |> does contain (string' "ana"))

                        verify
                            "\"banana\" does not contains \"xyz\""
                            ((string' "banana") |> doesNot contain (string' "xyz"))
                    ]
                )
            
            unitTest 
                "Test string starts with another string"
                (fun () -> 
                    [
                        verify
                            "\"banana\" does start with \"ban\""
                            ((string' "banana") |> does startWith (string' "ban"))
                        
                        verify
                            "\"banana\" does not start with \"ana\""
                            ((string' "banana") |> doesNot startWith (string' "ana"))
                    ]
                )

            unitTest 
                "Test string ends with another string"
                (fun () -> 
                    [
                        verify
                            "\"banana\" contains \"ana\""
                            ((string' "banana") |> does endWith (string' "ana"))
                        
                        verify
                            "\"banana\" does not end with \"xyz\""
                            ((string' "banana") |> does endWith (string' "ana"))
                    ]
                )

            unitTest 
                "Test string is empty"
                (fun () -> 
                    [
                        verify
                            "\"\" is empty"
                            ((string' "") |> is empty __)
                        
                        verify
                            "\"banana\" is not empty"
                            ((string' "banana") |> isNot empty __)
                    ]
                )
        ]