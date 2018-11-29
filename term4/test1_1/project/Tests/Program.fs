// Дополнительные сведения о F# см. на http://fsharp.org
// Дополнительную справку см. в проекте "Учебник по F#".
open FsUnit
open NUnit.Framework
open Task3


[<Test>]
let ``Palindrome Test``() =
    biggestPalindrome () |> should equal 906609


[<Test>]
let ``Stack test`` () =
    let stack = MyStack<int>()
    stack.isEmpty |> should equal true

    stack.Push(5)
    stack.isEmpty |> should equal false

    stack.Push(3)
    stack.isEmpty |> should equal false

    let y = stack.Pop()
    y |> should equal 3

    let x = stack.Pop()
    x |> should equal 5

    stack.isEmpty |> should equal true 

    (fun() -> stack.Pop() |> ignore) |> should throw typeof<System.Exception>

[<EntryPoint>]
let main argv = 
    printfn "%A" argv

    System.Console.ReadKey() |> ignore
    0 // возвращение целочисленного кода выхода
