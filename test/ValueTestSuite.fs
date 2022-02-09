module ValueTestSuite

open Das.Test

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
                            (val' pointA |> is lessThan (val' pointB))
                        
                        verify
                            "Point B is not less than Point A"
                            (val' pointB |> isNot lessThan (val' pointA))
                    ]
                )
        ]
