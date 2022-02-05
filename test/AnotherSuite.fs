module AnotherSuite

open Das.Test.Core

let run () =
    testSuite
        "AnotherSuite.fs"
        [
            unitTest 
                "Test number unit test functions"
                (fun () -> 
                    [
                        verify
                            "3 is greater than 2"
                            (3 |> is greaterThan 2)
                    ]
                )

            unitTest 
                "Test string unit test functions"
                (fun () -> 
                    [
                        verify
                            "\"banana\" contains \"ana\""
                            ((_string "banana") |> does contain (_string "ana"))
                    ]
                )
        ]