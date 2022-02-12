# Das.Test

An opinionated unit testing library written in F# for F#.

# Goals

* The unit testing library should work and should not exit giving some obscure error.

* The library should not expect user to know C# and dotnet (except for basic stuff to make your F# project run), and should document everything that's required to use the library in the docs itself.

* The unit tests written should be easy to understand and should read like an English sentence.

* The library should not have any dependency with another dotnet library or framework.

# Install 

To install the library, run the following command:
```bash
$ dotnet add package Das.Test
```

# Docs

* To use the library, use `open` statement:
    ```fsharp
    open Das.Test
    ```

* To understand how tests are written, let us look at how to test whether two numbers are equal:

    ```fsharp
    let num = 4

    verify
        "num is equal to 4"
        (num |> is equalTo (4)
    ```

    * `Das.Test` provides a function called `verify` which takes verification description as the first argument and the second argument as the test you want to perform.

    * At the most granular level, tests are written in the following format:
        ```fsharp
        val' num |> is equalTo (val' 4)
        ```
    
    * The first argument, here `val' num` is the actual result, while as the second argument (`val' 4`) is the expected result.

    * `val'` is what I call **test type functions**, which are essentially functions which convert values into an internally defined test type. This conversion is required as the test functions operate on the test types. 

    * This approach allows in reusing same function for multiple tests involving different data types. For instance, `contain` test can be used with both `string` and `list` types:

        ```fsharp
        (list' [1; 2; 3] |> does contain (val' 2))

        (val' "some text" |> does contain (val' "ext"))    
        ```

    * Following test type functions are available:
        * `val'` to convert primitive types like `int`, `float` and `string`.
        * `list'` to convert lists.
        * `seq'` to convert sequences
        * `array'` to convert arrays
        * `map'` to convert maps
        * `option'` to convert `option` type

* If you were to test something like `"num is not equal to 50"`, then you could write the test like:
    ```fsharp
    verify
        "num is not equal to 4"
        ((val' num) |> isNot equalTo (val' 50))
    ```

    * Notice `isNot` function. These are what I call **glue functions** which helps making test sound like proper English sentence. 

    * Moreover, they alter the output of functions like `equalTo`. For instance, `is` would allow the default behavior of `equalTo`, while as `isNot` would invert its behavior, i.e. `equalTo` would then check whether the expected and actual results are not the same.

    * There are other similar glue functions like `has`, `does` and `doesNotHave`.

* Sometimes, the expected value is not required (like when you just want to check whether a string is empty). In such a case, you would use `__` in place of expected value. For example, if you want to check whether string `name` is empty:

    ```fsharp
    (val' name |> is empty __)
    ```

* Other test examples:

    ```fsharp
    (val' 3.2 |> isNot lessThan (val' 3.19)

    (list' [1; 2; 3] |> has length (val' 3)

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

    (val' pointA |> is lessThan (val' pointB)
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
                    (val' 3 |> is equalTo (val' 3))
                
                verify
                    "3 is not equal to 4"
                    (val' 3 |> isNot equalTo (val' 4))
                
                verify
                    "num is equal to 4"
                    (val' num |> is equalTo (val' 4))
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
                            (val' 3 |> is equalTo (val' 3))
                        
                        verify
                            "3 is not equal to 4"
                            (val' 3 |> isNot equalTo (val' 4))
                        
                        verify
                            "num is equal to 4"
                            (val' num |> is equalTo (val' 4))
                    ]
                )

            unitTest 
                "Test an integer is greater than another integer"
                (fun () -> 
                    [
                        verify
                            "3 is greater than 2"
                            (3 |> is greaterThan (2))
                        
                        verify
                            "3 is not greater than than 4"
                            (3 |> isNot greaterThan (4))
                    ]
                )
        ]
    ```

    * This function accepts test suite description as first argument and list of `unitTest` function results as second argument.

    * This function would return test execution details if all tests successful, or fail with details on unit test failing.

* The tests are run in sequential order (as that's how I wanted), but if someone wants random order then let me know.

# License

AGPL v3 License, Copyright (c) 2022 Sumeet Das
