// Дополнительные сведения о F# см. на http://fsharp.org
// Дополнительную справку см. в проекте "Учебник по F#".
open FsUnit
open NUnit.Framework
open Task3

[<Test>]
let ``Palindrome Test``() =
    biggestPalindrome () |> should equal 906609





[<EntryPoint>]
let main argv = 
    printfn "%A" argv
    0 // возвращение целочисленного кода выхода
