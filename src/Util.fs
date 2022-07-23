namespace Das.Test

open System.Text.RegularExpressions

module Util = 
    let removeWhitespace (str: string) = Regex.Replace(str.Trim(), "\\s+", " ")