module UtilTestSuite

open Das.Test
open Das.Test.Util

let run () =
    testSuite
        "UtilTestSuite.fs"
        [
            unitTest 
                "Test strings are equal"
                (fun () -> 
                    let input = """
                    Hello There!                
                    Hi There!
                    """

                    let expected = "Hello There! Hi There!"

                    let actual = input |> trimWhitespace

                    let result1 = 
                        verify
                            "'actual' string is equal to 'expected' string"
                            (val' actual |> is equalTo (val' expected))

                    let input = """

                    Hello There!      

5

                    Hi There!

                    """

                    let expected = "Hello There! 5 Hi There!"

                    let actual = input |> trimWhitespace

                    let result2 = 
                        verify
                            "'actual' string is equal to 'expected' string"
                            (val' actual |> is equalTo (val' expected))
                        
                    [
                        result1
                        
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