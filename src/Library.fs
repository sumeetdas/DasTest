namespace Das.Test

module Core = 
    type 'a TestType = 
        | TestString of string
        | TestList of 'a list

    let private xor a b = if a then not b else b
    let private isOrNot is = if is then "is" else "is not"

    let _string func y (x: string) = (TestString >> func) y (TestString x)
    let _list func y (x: 'z list) = (TestList >> func) y (TestList x)

    let is func x = func false x
    let isNot func x = func true x
    let has func x = func false x
    let have func x = func false x
    let doesNotHave func x = func true x
    
    let greaterThan y (negate: bool) x: Result<bool, string> = 
        let resultOk = xor (x > y) negate
        if resultOk then Ok (true)
        else 
            let is = negate
            let text = 
                sprintf "%A %s greater than %A" 
                    x (isOrNot is) y
            Error (text)

    let greaterThanOrEqualTo y (negate: bool) x = 
        let resultOk = xor (x >= y) negate
        if resultOk then Ok (true)
        else
            let is = negate
            let text = 
                sprintf "%A %s greater than or equal to %A" 
                    x (isOrNot is) y
            Error (text)

    let lessThan y (negate: bool) x = 
        let resultOk = xor (x < y) negate
        if resultOk then Ok (true)
        else 
            let is = negate
            let text = 
                sprintf "%A %s less than %A" 
                    x (isOrNot is) y
            Error (text)

    let lessThanOrEqualTo y (negate: bool) x = 
        let resultOk = xor (x <= y) negate
        if resultOk then Ok (true)
        else 
            let is = negate
            let text = 
                sprintf "%A %s less than or equal to %A" 
                    x (isOrNot is) y
            Error (text)

    let True x = x = true
    let False x = x = false

    let startsWith (text: string) (x: 'a TestType): Result<bool, string> = 
        match x with
        | TestString str -> 
            if str.StartsWith(text) then Ok (true)
            else 
                let text = sprintf "\"%s\" does not ends with \"%s\"" str text
                Error (text)
        | _ -> Error (sprintf "Input %A is not supported" x)

    let endsWith (text: string) (x: 'a TestType): Result<bool, string> = 
        match x with 
        | TestString str -> 
            if str.EndsWith(text) then Ok (true)
            else 
                let text = sprintf "\"%s\" does not ends with \"%s\"" str text
                Error (text)
        | _ -> Error (sprintf "Input %A is not supported" x)

    let containsString (text: string) (x: 'a TestType): Result<bool, string> = 
        match x with 
        | TestString str -> 
            if str.Contains(text) then Ok (true)
            else 
                let text = sprintf "\"%s\" does not contain string \"%s\"" str text
                Error (text)
        | _ -> 
            Error (sprintf "Input %A is not supported." x)

    let equalToString (y: string) (x: 'a TestType): Result<bool, string> = 
        match x with 
        | TestString str -> 
            if str = y then Ok (true) 
            else 
                let text = 
                    sprintf "Expected = %s. Actual = %s" str y
                Error (text)
        | _ -> Error (sprintf "Input %A is not supported" x)

    let empty (x: 'a TestType): Result<bool, string> = 
        match x with 
        | TestString str -> 
            if str.Length = 0 then Ok (true)
            else 
                let text = sprintf "%s is not an empty string" str
                Error (text)
        | TestList list -> 
            if list.Length = 0 then Ok (true)
            else 
                let text = sprintf "%A is not an empty list" list
                Error (text)
        // | _ -> 
        //     Error (sprintf "Input %A is not supported" x)

    let length (len: int) (x: 'a TestType): Result<bool, string> = 
        match x with 
        | TestString str -> 
            if str.Length = len then Ok (true)
            else 
                let text = sprintf "The length of %A is not %d" x len
                Error (text)
        | TestList list -> 
            if list.Length = len then Ok (true)
            else 
                let text = sprintf "The length of %A is not %d" x len
                Error (text)
        // | _ -> 
        //     Error (sprintf "Input %A is not supported" x)

    let equalToList (anotherList: 'a list) (x: 'a TestType): Result<bool, string> =
        match x with 
        | TestList list -> 
            if list = anotherList then Ok (true)
            else 
                let text = sprintf "List %A is not equal to %A" list anotherList
                Error (text)
        | _ -> Error (sprintf "Input %A is not supported." x)

    let containsElem (elem: 'a) (x: 'a TestType): Result<bool, string> = 
        match x with 
        | TestList list -> 
            if list |> List.contains elem then Ok (true)
            else 
                let text = sprintf "List %A does not contain not element %A" list elem
                Error (text)
        | _ -> 
            Error (sprintf "This function does not support input %A" x)
        
    let verify 
        (testName: string) 
        (testResult: Result<bool, string>)
        : Result<bool, string> = 

        match testResult with 
        | Ok (_) -> Ok (true)
        | Error (message) ->
            let text = 
                sprintf "Test `%s` failed. Reason:\n\t\t\t%s" 
                    testName message
            Error(text)

    let unitTest 
        (testName: string) 
        (testFunc: unit -> Result<bool, string> list)
        : Result<bool, string> = 

        let verifyResults = testFunc()

        verifyResults 
        |> List.map (fun result -> 
            match result with 
            | Ok (_) -> Ok (true)
            | Error (message) -> 
                let text = 
                    sprintf 
                        "Unit Test `%s` failed because of the following error:\n\t\t%s"
                        testName message
                Error (text)
        )
        |> List.head

    let testSuite 
        (suiteName: string) 
        (unitTestResults: Result<bool, string> list)
        : string = 
        
        let totalUnitTests = unitTestResults.Length
        
        let numFailedUnitTests, failedUnitTestResults =
            unitTestResults 
            |> List.fold
                (fun state result -> 
                    match result with 
                    | Ok (_) -> state
                    | Error (message) -> 
                        let accCount, accMessage = state
                        let updatedAccMessage = 
                            accMessage + "\n\n\t" + message
                        (accCount + 1, updatedAccMessage)
                )
                (0, "")
        
        let header = 
            sprintf "%d/%d unit tests passed in test suite `%s`."
                (totalUnitTests - numFailedUnitTests) 
                totalUnitTests 
                suiteName

        let failedTestResultMessages = 
            if numFailedUnitTests = 0 then ""
            else 
                sprintf "Failed unit tests: %s" failedUnitTestResults
        
        header + "\n" + failedTestResultMessages

