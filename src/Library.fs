namespace Das.Test

module Core = 
    let is func x = func x
    let isNot func x = not (func x)
    let have func x = func x
    let doesNotHave func x = not (func x)

    let equalTo y x: Result<bool, string> = 
        if x = y then Ok (true) 
        else 
            let text = 
                sprintf "Expected = %A. Actual = %A" x y
            Error (text)

    let greaterThan y x: Result<bool, string> = 
        if x > y then Ok (true)
        else 
            let text = 
                sprintf "%A is not greater than %A" x y
            Error (text)

    let greaterThanOrEqualTo y x = 
        if x >= y then Ok (true)
        else
            let text = 
                sprintf "%A is not greater than or equal to %A" x y
            Error (text)

    let lessThan y x = 
        if x < y then Ok (true)
        else 
            let text = 
                sprintf "%A is not less than %A" x y
            Error (text)

    let lessThanOrEqualTo y x = 
        if x <= y then Ok (true)
        else 
            let text = 
                sprintf "%A is not less than or equal to %A" x y
            Error (text)

    let True x = x = true
    let False x = x = false

    let startsWith (text: string) (x: string): bool = 
        x.StartsWith(text)

    let endsWith (text: string) (x: string): Result<bool, string> = 
        if x.EndsWith(text) then Ok (true)
        else 
            let text = sprintf "%s does not ends with %s" x text
            Error (text)

    let containsString (text: string) (x: string): Result<bool, string> = 
        if x.Contains(text) then Ok (true)
        else 
            let text = sprintf "%s does not contain string %s" x text
            Error (text)

    let emptyString (x:string): Result<bool, string> = 
        if x.Length = 0 then Ok (true)
        else 
            let text = sprintf "%s is not an empty string" x
            Error (text)

    let stringLength (len: int) (x: string): Result<bool, string> = 
        if x.Length = len then Ok (true)
        else 
            let text = sprintf "The length of %s is not %d" x len
            Error (text)

    let listLength (len: int) (x: 'a list): Result<bool, string> = 
        if x.Length = len then Ok (true)
        else 
            let text = sprintf "The length of %A is not %d" x len
            Error (text)


    let verify 
        (testName: string) 
        (testResult: Result<bool, string>)
        : Result<bool, string> = 

        match testResult with 
        | Ok (_) -> Ok (true)
        | Error (message) ->
            let text = 
                sprintf "Test \"%s\" failed. Reason:\n\t\t\t%s" 
                    testName message
            Error(text)

    let unitTest 
        (testName: string) 
        (testFunc: unit -> Result<bool, string>)
        : Result<bool, string> = 

        let testResult = testFunc()

        match testResult with 
        | Ok (_) -> Ok (true)
        | Error (message) -> 
            let text = 
                sprintf 
                    "Unit Test %s failed because of the 
                    following error:\n\t\t%s"
                    testName message
            Error (text)

    let testSuite 
        (suiteName: string) 
        (unitTestResults: Result<bool, string> list)
        : string = 
        
        let totalUnitTests = unitTestResults.Length
        let passedUnitTests = 0
        
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
            sprintf "%d/%d unit tests passed in test suite \"%s\"."
                numFailedUnitTests totalUnitTests suiteName

        let failedTestResultMessages = 
            if numFailedUnitTests = 0 then ""
            else 
                sprintf "Failed unit tests: \n\n%s" failedUnitTestResults
        
        header + "\n" + failedTestResultMessages

