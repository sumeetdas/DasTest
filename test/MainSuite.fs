module MainSuite

open Das.Test.Core

let run() =
    testSuite
        "Library.fs"
        [
            unitTest 
                "Test number unit test functions"
                (fun () -> 
                    [
                        verify
                            "1 is greater than 2"
                            (1 |> is greaterThan 2)
                    ]
                )

            unitTest 
                "Test string unit test functions"
                (fun () -> 
                    [
                        verify
                            "\"banana\" contains \"rna\""
                            ((_string "banana") |> does contain (_string "rna"))
                    ]
                )
        ]
