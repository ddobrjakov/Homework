// Дополнительные сведения о F# см. на http://fsharp.org
// Дополнительную справку см. в проекте "Учебник по F#".

[<EntryPoint>]
let main argv = 
    printfn "%s" "Test"
    System.Console.ReadKey() |> ignore
    0 // возвращение целочисленного кода выхода
