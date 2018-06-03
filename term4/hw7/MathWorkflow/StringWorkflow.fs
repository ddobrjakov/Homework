module StringWorkflow

type Result =
    | Error
    | Number of int

/// <summary>
/// Represents computation expression builder for calculations with strings
/// </summary>
type StringBuilder() =

    let stringToResult str =
        match System.Int32.TryParse str with
        | true, num -> Number num
        | _ -> Error

    member this.Bind(x: string, rest: int -> Result) =
        match stringToResult x with
            | Error -> Error
            | Number n -> 
                try rest n
                with :? System.DivideByZeroException -> Error
        
    member this.Return (x: int) = 
        Number x