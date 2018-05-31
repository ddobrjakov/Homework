// Дополнительные сведения о F# см. на http://fsharp.org
// Дополнительную справку см. в проекте "Учебник по F#".

open Task3_PhoneBook

[<EntryPoint>]
let main argv = 
    DataHandling.handleCommand (Interface.getCommand "Введите команду: ") []
    printfn "\nAny key to exit..."
    System.Console.ReadKey() |> ignore
    0 // возвращение целочисленного кода выхода
