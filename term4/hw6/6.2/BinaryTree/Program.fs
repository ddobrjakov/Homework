﻿// Дополнительные сведения о F# см. на http://fsharp.org
// Дополнительную справку см. в проекте "Учебник по F#".
open Tree

[<EntryPoint>]
let main argv = 
    printfn "%A" argv
    System.Console.ReadKey() |> ignore
    0 // возвращение целочисленного кода выхода