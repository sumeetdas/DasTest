# Das.Test
An opinionated unit testing library written in F# for F#.

# Motivation

I tried setting up testing project using [official dotnet link](https://github.com/dotnet/samples/blob/main/core/getting-started/unit-testing-with-fsharp/MathService.Tests/MathService.Tests.fsproj) but got some obscure error for which I couldn't find any solution on the internet. Many other sources like [Scott Wlaschin's blog](https://fsharpforfunandprofit.com/posts/low-risk-ways-to-use-fsharp-at-work-3) are too difficult to follow for someone like me who has never worked with C# or dotnet before.

So I said to myself to heck with it, let me create my own unit testing library.

# Goals

* The unit testing library should work and should not exit giving some obscure error.

* The library should not expect user to know C# and dotnet (except for basic stuff to make your F# project run), and should document everything that's required to use the library in the docs itself.

* The unit tests written should be easy to understand and reason with.

* The library should not have any dependency with another dotnet library or framework.

# Docs

* To install the library, execute the following command:
    ```bash
    $ dotnet add package Das.Test
    ```

* To use the library, use `open` statement:
    ```fsharp
    open Das.Test.Core
    ```

* To understand how tests are written, let us look at how to test whether two numbers are equal:

    ```fsharp
    let num = 4

    verify
        "num is equal to 4"
        ((_int num) |> is equalTo (_int 4))
    ```

    * `Das.Test` provides a function called `verify` which takes verification description as the first argument and the second argument as the test you want to perform.

    * At the most granular level, tests are written in the following format:
        ```fsharp
        (_int num) |> is equalTo (_int 4)
        ```
    
    * The first argument, here `_int num` is the actual result, while as the second argument (`_int 4`) is the expected result.

    * `Das.Test` provides certain functions like `_int`, `_string` and `_list` to describe the data type of actual and expected values. These are <u>required</u>.

* If you were to test something like `"num is not equal to 50"`, then you could write the test like:
    ```fsharp
    verify
        "num is not equal to 4"
        ((_int num) |> isNot equalTo (_int 50))
    ```

    * Notice `isNot` function. These are what I call **glue functions** which helps making test sound like proper English sentence. 

    * Moreover, they alter the output of functions like `equalTo`. For instance, `is` would allow the default behavior of `equalTo`, while as `isNot` would invert its behavior, i.e. `equalTo` would then check whether the expected and actual results are not the same.

    * There are other similar glue functions like `has`, `does` and `doesNotHave`.

* Sometimes, the expected value is not required (like when you just want to check whether a string is empty). In such a case, you would use `__` in place of expected value. For example, if you want to check whether string `name` is empty:

    ```fsharp
    (_string name) |> is empty __)
    ```

* Other test examples:

    ```fsharp
    (_float 3.2) |> isNot lessThan (_float 3.19)

    (_list [1; 2; 3]) |> has length (_int 3)

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

    (_value pointA) |> is lessThan (_value pointB)
    ```

    For more examples, checkout `test` folder.

* To write unit tests, use `unitTest` function which accepts unit test description as first argument and a lambda returning list of `verify` function results as second argument:

    ```fsharp
    unitTest 
        "Test an integer is equal to another integer"
        (fun () -> 
            let num = 4

            [
                verify
                    "3 is equal to 3"
                    ((_int 3) |> is equalTo (_int 3))
                
                verify
                    "3 is not equal to 4"
                    ((_int 3) |> isNot equalTo (_int 4))
                
                verify
                    "num is equal to 4"
                    ((_int num) |> is equalTo (_int 4))
            ]
        )
    ```

* `unitTest`s can further be grouped into test suites via `testSuite` function which are expected to be one per file:

    ```fsharp
    testSuite
        "NumberTests.IntegerTestSuite.fs"
        [
            unitTest 
                "Test an integer is equal to another integer"
                (fun () -> 
                    let num = 4

                    [
                        verify
                            "3 is equal to 3"
                            ((_int 3) |> is equalTo (_int 3))
                        
                        verify
                            "3 is not equal to 4"
                            ((_int 3) |> isNot equalTo (_int 4))
                        
                        verify
                            "num is equal to 4"
                            ((_int num) |> is equalTo (_int 4))
                    ]
                )

            unitTest 
                "Test an integer is greater than another integer"
                (fun () -> 
                    [
                        verify
                            "3 is greater than 2"
                            ((_int 3) |> is greaterThan (_int 2))
                        
                        verify
                            "3 is not greater than than 4"
                            ((_int 3) |> isNot greaterThan (_int 4))
                    ]
                )
        ]
    ```

    * This function accepts test suite description as first argument and list of `unitTest` function results as second argument.

    * This function would return test execution details if all tests successful, or fail with details on unit test failing.

* The tests are run in sequential order (as that's how I wanted), but if someone wants random order then let me know.

# License

AGPL v3 License, Copyright (c) 2022 Sumeet Das
