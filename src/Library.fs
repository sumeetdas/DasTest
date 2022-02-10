namespace Das

module Test = 
    (*
        Active patterns
    *)

    /// Active pattern for matching boolean values
    let (|IsBool|_|) x =
        match box x with 
        | :? bool as x -> Some x
        | _ -> None

    /// Active pattern for matching integers
    let (|IsInt|_|) x =
        match box x with 
        | :? int as x -> Some x
        | _ -> None

    let (|IsInt64|_|) x =
        match box x with 
        | :? System.Int64 as x -> Some x
        | _ -> None

    /// Active pattern for matching floats
    let (|IsFloat|_|) x =
        match box x with 
        | :? float as x -> Some x
        | _ -> None
        
    /// Active pattern for matching strings
    let (|IsString|_|) x =
        match box x with 
        | :? string as x -> Some x
        | _ -> None

    type TestType<'a, 'k, 'v when 'k : comparison> = 
        | TestSequence of 'a seq
        | TestArray of 'a array
        | TestList of 'a list
        | TestValue of 'a
        | TestOption of 'a option
        | TestMapKey of 'k
        | TestMapValue of 'v
        | TestMap of Map<'k , 'v>
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

    let val' (input: 'a) = TestValue input
    let value' (input: 'a) = val' input
    let list' (input: 'a list) = TestList input
    let array' (input: 'a array) = TestArray input
    let seq' (input: 'a seq) = TestSequence input
    let map' (input: Map<'a, 'b>) = TestMap input
    let key' (input: 'a) = TestMapKey input
    let __  = TestNone
    let none' = TestOption (None)
    let some' (input: 'a) = TestOption (Some input)

    let is func y x = func false y x
    let isNot func y x = func true y x
    let has func y x = func false y x
    let have func y x = func false y x
    let does func y x = func false y x
    let doesNot func y x = func true y x
    let doesNotHave func y x = func true y x
    
    let greaterThan (negate: bool) y x : Result<bool, string> = 
        let is = negate
        let testGreaterThan x y = 
            let resultOk = xor (x > y) negate
            if resultOk then Ok (true)
            else 
                Error (sprintf "%A %s greater than %A" 
                        x (isOrNot is) y)

        match x, y with 
        | TestValue x, TestValue y -> testGreaterThan x y
        | _ -> Error (notSupported x y)

    let greaterThanOrEqualTo (negate: bool) y x : Result<bool, string> = 
        let is = negate
        let testGreaterThanOrEqualTo x y = 
            let resultOk = xor (x >= y) negate
            if resultOk then Ok (true)
            else 
                Error (sprintf "%A %s greater than or equal to %A" 
                        x (isOrNot is) y)

        match x, y with 
        | TestValue x, TestValue y -> testGreaterThanOrEqualTo x y
        | _ -> Error (notSupported x y)

    let lessThan (negate: bool) y x : Result<bool, string> = 
        let is = negate
        let testLessThan x y = 
            let resultOk = xor (x < y) negate
            if resultOk then Ok (true)
            else 
                Error (sprintf "%A %s less than %A" 
                        x (isOrNot is) y)

        match x, y with 
        | TestValue x, TestValue y -> testLessThan x y
        | _ -> Error (notSupported x y)

    let lessThanOrEqualTo (negate: bool) y x
        : Result<bool, string> = 

        let testLessThanOrEqualTo x y = 
            let is = negate
            let resultOk = xor (x <= y) negate
            if resultOk then Ok (true)
            else 
                Error (sprintf "%A %s less than or equal to %A" 
                        x (isOrNot is) y)

        match x, y with 
        | TestValue x, TestValue y -> testLessThanOrEqualTo x y
        | _ -> Error (notSupported x y)
            
    let True (negate: bool) _ x
        : Result<bool, string> =

        match x with 
        | TestValue tval ->   
            match tval with 
            | IsBool boolInput -> 
                let resultOk = xor (boolInput = true) negate
                if resultOk then Ok (true)
                else 
                    let is = negate
                    let text = 
                        sprintf "%b %s is not true" boolInput (isOrNot is)
                    Error (text)
            | _ -> Error (notSupported x x)
        | _ -> Error (notSupported x x)
           
    let False (negate: bool) _ x
        : Result<bool, string> = 

        match x with 
        | TestValue tval ->   
            match tval with 
            | IsBool boolInput -> 
                let resultOk = xor (boolInput = false) negate
                if resultOk then Ok (true)
                else 
                    let is = negate
                    let text = 
                        sprintf "%b %s is not false" boolInput (isOrNot is)
                    Error (text)
            | _ -> Error (notSupported x x)
        | _ -> Error (notSupported x x)
        
    let startWith (negate: bool) text x
        : Result<bool, string> = 

        match x, text with 
        | TestValue xVal, TestValue textVal -> 
            match xVal, textVal with 
            | IsString strX, IsString strText -> 
                let resultOk = xor (strX.StartsWith(strText)) negate
                if resultOk then Ok (true)
                else 
                    let does = negate
                    let message = sprintf "\"%s\" %s starts with \"%s\"" 
                                    strX (doesOrNot does) strText
                    Error (message)
            | _ -> Error (notSupported x text)
        | _ -> Error (notSupported x text)

    let endWith (negate: bool) text x
        : Result<bool, string> = 

        match x, text with 
        | TestValue xVal, TestValue textVal -> 
            match xVal, textVal with 
            | IsString strX, IsString strText -> 
                let resultOk = xor (strX.EndsWith(strText)) negate
                if resultOk then Ok (true)
                else 
                    let does = negate
                    let message = sprintf "\"%s\" %s ends with \"%s\"" 
                                    strX (doesOrNot does) strText
                    Error (message)
            | _ -> Error (notSupported x text)
        | _ -> Error (notSupported x text)

    let contain (negate: bool) y input
        : Result<bool, string> = 

        match input, y with 
        | TestValue inputVal, TestValue yVal -> 
            match inputVal, yVal with 
            | IsString strInput, IsString strY -> 
                let resultOk = xor (strInput.Contains(strY)) negate
                if resultOk then Ok (true)
                else 
                    let does = negate
                    let text = sprintf "\"%s\" %s contain string \"%s\"" 
                                strInput (doesOrNot does) strY
                    Error (text)
            | _ -> Error (sprintf "Input %A is not supported." input)
        | _ -> 
            Error (sprintf "Input %A is not supported." input)

    let equalTo (negate: bool) expected actual
        : Result<bool, string> = 
        
        let testEqual a b = 
            let resultOk = (xor (a = b) negate)
            if resultOk then Ok (true) 
            else 
                Error (sprintf "List %A %s equal to %A" 
                        actual (isOrNot negate) expected)

        match actual, expected with 
        | TestValue actual, TestValue expected -> testEqual actual expected
        | TestList actual, TestList expected -> testEqual actual expected
        | _ -> Error (notSupported actual expected)

    let empty (negate: bool) (_) x : Result<bool, string> = 
        match x with 
        | TestValue x -> 
            match box x with
            | IsString str -> 
                let resultOk = xor (str.Length = 0) negate
                if resultOk then Ok (true)
                else 
                    let is = negate
                    let text = sprintf "%s %s an empty string" 
                                str (isOrNot is)
                    Error (text)
            | _ -> Error (notSupported x x)
        | TestList list -> 
            let resultOk = xor (list.Length = 0) negate

            if resultOk then Ok (true)
            else 
                let is = negate
                let text = sprintf "%A %s an empty list" 
                            list (isOrNot is)
                Error (text)
        | _ -> 
            Error (sprintf "Input %A is not supported" x)

    let length (negate: bool) (len: int) x : Result<bool, string> = 
        match x with 
        | TestValue tval -> 
            match tval with 
            | IsString str -> 
                let resultOk = xor (str.Length = len) negate
                if resultOk then Ok (true)
                else 
                    let is = negate
                    let text = sprintf "The length of %A %s %d" 
                                x (isOrNot is) len
                    Error (text)
            | _ -> Error (notSupported x x)
        | TestList list -> 
            let resultOk = xor (list.Length = len) negate
            if resultOk then Ok (true)
            else 
                let is = negate
                let text = sprintf "The length of %A %s %d" 
                            list (isOrNot is) len
                Error (text)
        | _ -> 
            Error (notSupported x len)

    let containsElem (negate: bool) elem collection : Result<bool, string> = 

        match collection, elem with 
        | TestList list, item -> 
            let resultOk = xor (list |> List.contains item) negate
            if resultOk then Ok (true)
            else 
                let does = negate
                let text = sprintf "List %A %s contain not element %A" 
                            list (doesOrNot does) elem
                Error (text)
        | _ -> 
            Error (notSupported collection elem)
        
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
        |> List.filter (fun result -> 
            match result with 
            | Error (_) -> true
            | _ -> false)
        |> (fun list -> if list.Length = 0 then [ Ok (true) ] else list)
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

        if numFailedUnitTests = 0 then header
        else 
            let failedTestDetails =
                sprintf "Failed unit tests: %s" failedUnitTestResults
            failwith ("\n" + header + "\n" + failedTestDetails)
