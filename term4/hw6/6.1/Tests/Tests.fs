module Tests6
open LocalNetwork
open Computer
open OpSystem
open FsUnit
open NUnit.Framework


[<Test>]
let ``Infected / Not Infected count test``() =
    let computerToGetInfectedFirst = Computer(OpSystem("Windows", SimpleDefendingTools(0.1)))
    computerToGetInfectedFirst.ForceInfect()

    let computersInfo =
        [
            computerToGetInfectedFirst, 1, [2; 3; 4];
            Computer(OpSystem("Linux", SimpleDefendingTools(0.4))), 2, [1; 3; 4];
            Computer(OpSystem("MacOs", SimpleDefendingTools(0.8))), 3, [1; 2];
            Computer(OpSystem("MacOs", SimpleDefendingTools(0.9))), 4, [1; 2];
            Computer(OpSystem("WindowsXP", SimpleDefendingTools(0.05))), 5, [3] 
        ]
    let Net = Network(computersInfo)
    Net.InfectedComputers |> List.length |> should equal 1
    Net.NotInfectedComputers |> List.length |> should equal 4

[<Test>]
let ``Wrong connections``()=
    let defendingTools = SimpleDefendingTools(0.5)

    let computersInfo =
        [
            Computer(OpSystem("OS1", defendingTools)), 1, [2; 3]
            Computer(OpSystem("OS2", defendingTools)), 2, [6; 3]
            Computer(OpSystem("OS3", defendingTools)), 3, [0; 5; 1]
            Computer(OpSystem("OS4", defendingTools)), 4, [1; 5]
            Computer(OpSystem("OS5", defendingTools)), 5, [3]
        ]
    (fun () -> Network(computersInfo) |> ignore) |> should throw typeof<System.Exception>


let getNetwork defendingTools =
    let computerToGetInfectedFirst = Computer(OpSystem("OS1", defendingTools))
    computerToGetInfectedFirst.ForceInfect()

    let computersInfo =
        [
            computerToGetInfectedFirst, 1, [2; 4]
            Computer(OpSystem("OS2", defendingTools)), 2, [1; 3]
            Computer(OpSystem("OS3", defendingTools)), 3, [2; 5; 1]
            Computer(OpSystem("OS4", defendingTools)), 4, [1; 5]
            Computer(OpSystem("OS5", defendingTools)), 5, [3]
        ]
    Network(computersInfo)

[<Test>]
let ``Defenceless Network Test``() =
    let uselessDefendingTools = SimpleDefendingTools(0.0)
    let defencelessNet = getNetwork <| uselessDefendingTools
    defencelessNet.SpreadInfection()
    defencelessNet.InfectedComputers |> List.length |> should equal (defencelessNet.Computers |> List.length)




[<Test>]
let ``Protected Network`` ()=
    let strongDefendingTools = SimpleDefendingTools(1.0)
    let net = getNetwork strongDefendingTools
    net.Launch()
    net.InfectedComputers |> List.length |> should equal 1