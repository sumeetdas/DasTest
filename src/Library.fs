namespace Das.Test

module Core = 
    type 'a TestType = 
        | TestString of string
        | TestList of 'a list
        | TestListItem of 'a
        | TestVariable of 'a
        | TestValue of 'a
        | TestBoolean of bool
        | TestInteger of int
        | TestFloat of float
        | TestNone

    let private xor a b = if a then not b else b
    let private isOrNot is = if is then "is" else "is not"
    let private doesOrNot is = if is then "does" else "does not"
    let private notSupported actual expected = 
        sprintf "%A and/or %A are not supported" actual expected
    let formatString (str:string): string = 
        if str.Contains "\n" && (str.Trim().Length <> 0)
        then sprintf "\"\"\"\n%s\n\"\"\"\n" str
        else sprintf "\"%s\"" str 

    let _string (input: string) = TestString input
    let _list (input: 'a list) = TestList input
    let _listItem (input: 'a) = TestListItem input
    let _variable (input: 'a) = TestVariable input
    let _value (input: 'a) = TestValue input
    let _bool (input: bool) = TestBoolean input
    let _int (input: int) = TestInteger input
    let _float (input: float) = TestFloat input
    let __  = TestNone

    let is func y x = func false y x
    let isNot func y x = func true y x
    let has func y x = func false y x
    let have func y x = func false y x
    let does func y x = func false y x
    let doesNot func y x = func true y x
    let doesNotHave func y x = func true y x
    
    let greaterThan 
        (negate: bool) 
        (y: 'a TestType) 
        (x: 'a TestType)
        : Result<bool, string> = 

        let resultOk = xor (x > y) negate
        if resultOk then Ok (true)
        else 
            let is = negate
            let text = 
                sprintf "%A %s greater than %A" 
                    x (isOrNot is) y
            Error (text)

    let greaterThanOrEqualTo 
        (negate: bool) 
        (y: 'a TestType) 
        (x: 'a TestType)
        : Result<bool, string> = 

        let resultOk = xor (x >= y) negate
        if resultOk then Ok (true)
        else
            let is = negate
            let text = 
                sprintf "%A %s greater than or equal to %A" 
                    x (isOrNot is) y
            Error (text)

    let lessThan 
        (negate: bool) 
        (y: 'a TestType) 
        (x: 'a TestType) 
        : Result<bool, string> = 

        let resultOk = xor (x < y) negate
        if resultOk then Ok (true)
        else 
            let is = negate
            let text = 
                sprintf "%A %s less than %A" 
                    x (isOrNot is) y
            Error (text)

    let lessThanOrEqualTo 
        (negate: bool) 
        (y: 'a TestType) 
        (x: 'a TestType)
        : Result<bool, string> = 

        match x, y with 
        | TestInteger xInt, TestInteger yInt -> 
            let resultOk = xor (xInt <= yInt) negate
            if resultOk then Ok (true)
            else 
                let is = negate
                let text = 
                    sprintf "%A %s less than or equal to %A" 
                        xInt (isOrNot is) yInt
                Error (text)
        | TestFloat xFloat, TestFloat yFloat -> 
            let resultOk = xor (xFloat <= yFloat) negate
            if resultOk then Ok (true)
            else 
                let is = negate
                let text = 
                    sprintf "%A %s less than or equal to %A" 
                        xFloat (isOrNot is) yFloat
                Error (text)
        | _ -> Error (notSupported x y)

    let True 
        (negate: bool) 
        (_: 'a TestType) 
        (x: 'a TestType)
        : Result<bool, string> =

        match x with 
        | TestBoolean boolInput ->   
            let resultOk = xor (boolInput = true) negate
            if resultOk then Ok (true)
            else 
                let is = negate
                let text = 
                    sprintf "%b %s is not true" boolInput (isOrNot is)
                Error (text)
        | _ -> Error (notSupported x x)
           
    let False 
        (negate: bool) 
        (_: 'a TestType) 
        (x: 'a TestType)
        : Result<bool, string> = 

        match x with 
        | TestBoolean boolInput -> 
            let resultOk = xor (boolInput = false) negate
            if resultOk then Ok (true)
            else 
                let is = negate
                let text = 
                    sprintf "%b %s is not true" boolInput (isOrNot is)
                Error (text)
        | _ -> Error (notSupported x x)
        
    let startsWith 
        (negate: bool) 
        (text: 'a TestType) 
        (x: 'a TestType)
        : Result<bool, string> = 

        match x, text with
        | TestString strX, TestString strText -> 
            let resultOk = xor (strX.StartsWith(strText)) negate
            if resultOk then Ok (true)
            else 
                let does = negate
                let message = sprintf "\"%s\" %s ends with \"%s\"" 
                                strX (doesOrNot does) strText
                Error (message)
        | _ -> Error (notSupported x text)

    let endsWith 
        (negate: bool) 
        (text: 'a TestType) 
        (x: 'a TestType)
        : Result<bool, string> = 

        match x, text with 
        | TestString strX, TestString strText -> 
            let resultOk = xor (strX.EndsWith(strText)) negate
            if resultOk then Ok (true)
            else 
                let does = negate
                let message = sprintf "\"%s\" %s ends with \"%s\"" 
                                strX (doesOrNot does) strText
                Error (message)
        | _ -> Error (notSupported x text)

    let contain 
        (negate: bool) 
        (y: 'a TestType) 
        (input: 'a TestType)
        : Result<bool, string> = 

        match input, y with 
        | TestString strInput, TestString strY -> 
            let resultOk = xor (strInput.Contains(strY)) negate
            if resultOk then Ok (true)
            else 
                let does = negate
                let text = sprintf "\"%s\" %s contain string \"%s\"" 
                            strInput (doesOrNot does) strY
                Error (text)
        | _ -> 
            Error (sprintf "Input %A is not supported." input)

    let equalTo 
        (negate: bool) 
        (expected: 'a TestType) 
        (actual: 'a TestType)
        : Result<bool, string> = 

        match actual, expected with 
        | TestString strActual, TestString strExpected -> 
            let resultOk = xor (strActual = strExpected) negate
            if resultOk then Ok (true) 
            else 
                let is = negate
                let text = 
                    sprintf "Actual string %s %s equal to expected string %s" 
                        (formatString strActual) 
                        (isOrNot is) 
                        (formatString strExpected) 
                Error (text)
        | TestList listActual, TestList listExpected -> 
            let resultOk = xor (listActual = listExpected) negate
            if resultOk then Ok (true)
            else 
                let is = negate
                let text = sprintf "List %A %s equal to %A" 
                            listActual (isOrNot is) listExpected
                Error (text)
        | _ -> Error (notSupported actual expected)

    let empty 
        (negate: bool) 
        (_: 'a TestType) 
        (x: 'a TestType)
        : Result<bool, string> = 

        match x with 
        | TestString str -> 
            let resultOk = xor (str.Length = 0) negate
            if resultOk then Ok (true)
            else 
                let is = negate
                let text = sprintf "%s %s an empty string" 
                            str (isOrNot is)
                Error (text)
        | TestList list -> 
            if list.Length = 0 then Ok (true)
            else 
                let is = negate
                let text = sprintf "%A %s an empty list" 
                            list (isOrNot is)
                Error (text)
        | _ -> 
            Error (sprintf "Input %A is not supported" x)

    let length 
        (negate: bool) 
        (len: int) 
        (x: 'a TestType)
        : Result<bool, string> = 

        match x with 
        | TestString str -> 
            let resultOk = xor (str.Length = len) negate
            if resultOk then Ok (true)
            else 
                let is = negate
                let text = sprintf "The length of %A %s %d" 
                            x (isOrNot is) len
                Error (text)
        | TestList list -> 
            let resultOk = xor (list.Length = len) negate
            if resultOk then Ok (true)
            else 
                let is = negate
                let text = sprintf "The length of %A %s %d" 
                            x (isOrNot is) len
                Error (text)
        | _ -> 
            Error (sprintf "Input %A is not supported" x)

    let containsElem 
        (negate: bool) 
        (elem: 'a TestType) 
        (list: 'a TestType)
        : Result<bool, string> = 

        match list, elem with 
        | TestList list, TestListItem item -> 
            let resultOk = xor (list |> List.contains item) negate
            if resultOk then Ok (true)
            else 
                let does = negate
                let text = sprintf "List %A %s contain not element %A" 
                            list (doesOrNot does) elem
                Error (text)
        | _ -> 
            Error (notSupported list elem)
        
    let verify 
        (testName: string) 
        (testResult: Result<bool, string>)
        : Result<bool, string> = 

        match testResult with 
        | Ok (_) -> Ok (true)
        | Error (message) ->
            let text = 
                sprintf "\n>> Verify `%s` failed. Reason:\n%s" 
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
                        "\n> Unit Test `%s` failed because of the following error:\n%s"
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
                            accMessage + "\n" + message
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

    let p = 
        (_string "sdsd") |> does contain (_string "sd")
    
    let q = 
        (_string "sdsd") |> doesNot contain (_string "sd")

    let r = 
        ((_list [1, 2, 3, 5]) |> is empty __)
    
    let r1 = 
        (_string "actualResult") |> is equalTo (_string "expectedResult")

    let r2 = 
        ((_bool true) |> is True __)
