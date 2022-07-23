namespace Das.Test

open System.Text.RegularExpressions

module Util = 
    let trimWhitespace (str: string) = Regex.Replace(str.Trim(), "\\s+", " ")