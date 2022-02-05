module NestedSuite.TestSuite1

open Das.Test.Core

let run () =
    testSuite
        "NestedSuite.TestSuite1.fs"
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
                            ("banana" |> _string (containsString "ana"))
                    ]
                )
        ]
    |> printfn "%s"