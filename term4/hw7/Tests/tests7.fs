module Tests7
open FsUnit
open NUnit.Framework
open RoundWorkflow
open StringWorkflow

let calculateRound accuracy = RoundBuilder(accuracy)
let calculateString = StringBuilder()

(* 7.1 Tests *)
[<Test>]
let ``Rounding workflow test`` () =
    calculateRound 3 
        {
            let! a = 2.0 / 12.0
            let! b = 3.5
            return a / b
        } |> should equal 0.048

[<Test>]
let ``Round with accuracy 5 test ``() =
    calculateRound 5 
        {
            let! x = 5.0 / 3.0
            return x
        } |> should equal 1.66667

[<Test>]
let ``Round with accuracy 3 test``() =
    calculateRound 3
        {
            let! x = 5.0 / 2.0
            let! y = x * 4.37849
            return x + y
        } |> should equal 13.446

[<Test>]
let ``Round with division by zero test``() =
    calculateRound 3
        {
            let! x = 5.0 / 0.0
            let! y = x * 42.53789
            return x * y
        } |> should equal System.Double.PositiveInfinity

(* 7.2 Tests *)
[<Test>]
let ``String calculations test 1``() =
    calculateString
        {
            let! x = "1"
            let! y = "2"
            let z = x + y
            return z
        } |> should equal <| Number 3

[<Test>]
let ``String calculations test Error``() =
    calculateString 
        {
            let! x = "1"
            let! y = "Ъ"
            let z = x + y
            return z
        } |> should equal Error

[<Test>]
let ``String calculations division by 0 test Error``() =
    calculateString 
        {
            let! x = "5"
            let! y = "0"
            let z = x / y
            return z
        } |> should equal Error