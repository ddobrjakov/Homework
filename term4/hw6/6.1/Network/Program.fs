// Дополнительные сведения о F# см. на http://fsharp.org
// Дополнительную справку см. в проекте "Учебник по F#".
open LocalNetwork
open Computer
open OpSystem

[<EntryPoint>]
let main argv = 
    let windowsDefTools = SimpleDefendingTools(0.1)
    let windowsOS = OpSystem("Windows", windowsDefTools)
    let georgeComputer = Computer(windowsOS, "George-PC")

    let linuxDefTools = SimpleDefendingTools(0.4)
    let linuxOS = OpSystem("Linux", linuxDefTools)
    let patrickComputer = Computer(linuxOS, "Patrick-PC")

    let computer3 = Computer(OpSystem("MacOs", SimpleDefendingTools(0.8)), "WellProtectedComp")
    let computer4 = Computer(OpSystem("MacOs", SimpleDefendingTools(0.9)), "VeryWellProtectedComp")
    let computer5 = Computer(OpSystem("WindowsXP", SimpleDefendingTools(0.05)))

    georgeComputer.ForceInfect()
    let computersInfo =
        [
            georgeComputer, 1, [2; 3];
            patrickComputer, 2, [1; 4];
            computer3, 3, [1];
            computer4, 4, [2];
            computer5, 5, [3] 
        ]
    let net = Network(computersInfo)
    net.Launch()

    System.Console.ReadKey() |> ignore
    0 // возвращение целочисленного кода выхода
