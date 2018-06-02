// Дополнительные сведения о F# см. на http://fsharp.org
// Дополнительную справку см. в проекте "Учебник по F#".
open LocalNetwork
open Computer
open OpSystem

[<EntryPoint>]
let main argv = 
    let WindowsDefTools = SimpleDefendingTools(0.1)
    let WindowsOS = OpSystem("Windows", WindowsDefTools)
    let GeorgeComputer = Computer(WindowsOS, "George-PC")

    let LinuxDefTools = SimpleDefendingTools(0.4)
    let LinuxOS = OpSystem("Linux", LinuxDefTools)
    let PatrickComputer = Computer(LinuxOS, "Patrick-PC")

    let Computer3 = Computer(OpSystem("MacOs", SimpleDefendingTools(0.8)), "WellProtectedComp")
    let Computer4 = Computer(OpSystem("MacOs", SimpleDefendingTools(0.9)), "VeryWellProtectedComp")
    let Computer5 = Computer(OpSystem("WindowsXP", SimpleDefendingTools(0.05)))

    GeorgeComputer.ForceInfect()
    let computersInfo =
        [
            GeorgeComputer, 1, [2; 3];
            PatrickComputer, 2, [1; 4];
            Computer3, 3, [1];
            Computer4, 4, [2];
            Computer5, 5, [3] 
        ]
    let Net = Network(computersInfo)
    Net.Launch()

    System.Console.ReadKey() |> ignore
    0 // возвращение целочисленного кода выхода
