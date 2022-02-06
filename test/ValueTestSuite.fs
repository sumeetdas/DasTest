module ValueTestSuite

open Das.Test.Core

type Point = {
    x: int
    y: int
}

type Point with
    static member squareDistance point = 
        (point.x * point.x) + (point.y * point.y)
    static member op_GreaterThan (pointA: Point, pointB: Point) = 
        (pointA |> Point.squareDistance) > (pointB |> Point.squareDistance)

let pointA = {
    x = 1
    y = 1
}

let pointB = {
    x = 2
    y = 2
}

let run() =
    testSuite
        "ValueTestSuite.fs"
        [
            unitTest 
                "Test one value is less than the other"
                (fun () -> 
                   [
                        verify
                            "Point A is less than Point B"
                            ((_value pointA) |> is lessThan (_value pointB))
                        
                        verify
                            "Point B is not less than Point A"
                            ((_value pointB) |> isNot lessThan (_value pointA))
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
