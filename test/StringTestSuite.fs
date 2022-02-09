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
                            ("jpeg" |> is equalTo "jpeg")
                        
                        verify
                            "\"jpeg\" isNot equal to \"peg\""
                            ("jpeg" |> isNot equalTo "peg")
                    ]
                )

            unitTest 
                "Test string contains another string"
                (fun () -> 
                    [
                        verify
                            "\"banana\" contains \"ana\""
                            ("banana" |> does contain "ana")

                        verify
                            "\"banana\" does not contains \"xyz\""
                            ("banana" |> doesNot contain "xyz")
                    ]
                )
            
            unitTest 
                "Test string starts with another string"
                (fun () -> 
                    [
                        verify
                            "\"banana\" does start with \"ban\""
                            ("banana" |> does startWith "ban")
                        
                        verify
                            "\"banana\" does not start with \"ana\""
                            ("banana" |> doesNot startWith "ana")
                    ]
                )

            unitTest 
                "Test string ends with another string"
                (fun () -> 
                    [
                        verify
                            "\"banana\" contains \"ana\""
                            ("banana" |> does endWith "ana")
                        
                        verify
                            "\"banana\" does not end with \"xyz\""
                            ("banana" |> does endWith "ana")
                    ]
                )

            unitTest 
                "Test string is empty"
                (fun () -> 
                    [
                        verify
                            "\"\" is empty"
                            ("" |> is empty __)
                        
                        verify
                            "\"banana\" is not empty"
                            ("banana" |> isNot empty __)
                    ]
                )
        ]