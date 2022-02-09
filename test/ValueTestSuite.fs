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
                            ((value' pointA) |> is lessThan (value' pointB))
                        
                        verify
                            "Point B is not less than Point A"
                            ((value' pointB) |> isNot lessThan (value' pointA))
                    ]
                )
        ]
