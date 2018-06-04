// Дополнительные сведения о F# см. на http://fsharp.org
// Дополнительную справку см. в проекте "Учебник по F#".
open Downloader
open System.Text.RegularExpressions

[<EntryPoint>]
let main argv = 
    printfn "%A" argv

    let url = "https://www.google.com/"
    let pagesData = getPagesData url
    handleData pagesData
    System.Console.ReadKey() |> ignore
    0 // возвращение целочисленного кода выхода
