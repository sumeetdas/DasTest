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
                            (val' "jpeg" |> is equalTo (val' "jpeg"))
                        
                        verify
                            "\"jpeg\" isNot equal to \"peg\""
                            (val' "jpeg" |> isNot equalTo (val' "peg"))
                    ]
                )

            unitTest 
                "Test string contains another string"
                (fun () -> 
                    [
                        verify
                            "\"banana\" contains \"ana\""
                            (val' "banana" |> does contain (val' "ana"))

                        verify
                            "\"banana\" does not contains \"xyz\""
                            (val' "banana" |> doesNot contain (val' "xyz"))
                    ]
                )
            
            unitTest 
                "Test string starts with another string"
                (fun () -> 
                    [
                        verify
                            "\"banana\" does start with \"ban\""
                            (val' "banana" |> does startWith (val' "ban"))
                        
                        verify
                            "\"banana\" does not start with \"ana\""
                            (val' "banana" |> doesNot startWith (val' "ana"))
                    ]
                )

            unitTest 
                "Test string ends with another string"
                (fun () -> 
                    [
                        verify
                            "\"banana\" contains \"ana\""
                            (val' "banana" |> does endWith (val' "ana"))
                        
                        verify
                            "\"banana\" does not end with \"xyz\""
                            (val' "banana" |> does endWith (val' "ana"))
                    ]
                )

            unitTest 
                "Test string is empty"
                (fun () -> 
                    [
                        verify
                            "\"\" is empty"
                            (val' "" |> is empty __)
                        
                        verify
                            "\"banana\" is not empty"
                            (val' "banana" |> isNot empty __)
                    ]
                )
        ]