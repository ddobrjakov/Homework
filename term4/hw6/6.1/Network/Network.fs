module LocalNetwork
open System
open Computer

/// <summary>
/// Represents the network class
/// </summary>   
type Network(computersInfo: List<Computer * int * List<int>>) =
    
    let parseCompsInfo (computersInfo: List<Computer * int * List<int>>) =
        //Creating Map to be able to get computer by it's identifier
        let compsMap = computersInfo |> List.map (fun (comp, id, connections) -> (id, comp)) |> Map.ofList
        //Connecting one computer to network - procedure
        let initComputer ((comp, id, connections):(Computer * int * List<int>)) =
            comp.Connect (connections |> List.map (fun connection -> compsMap.[connection]))
        //Connecting each computer
        try computersInfo |> List.iter (fun x -> initComputer x)
        with
            | _ -> failwith "Could't parse connections between computers"
        //Extracting and returning computers themselves
        computersInfo |> List.map (fun (comp, id, connections) -> comp)

    let mutable _Computers = parseCompsInfo computersInfo
    let maxStepsLimit = 500
    let mutable stepsCount = 0

    /// <summary>
    /// Returns all computers in network
    /// </summary>
    member this.Computers
        with get() = _Computers
        and set(value) = _Computers <- value

    /// <summary>
    /// Returns all infected computers in network
    /// </summary>   
    member this.InfectedComputers
        with get() = _Computers |> List.filter (fun comp -> comp.Infected)

    /// <summary>
    /// Returns all not infected computers in network
    /// </summary>   
    member this.NotInfectedComputers
        with get() = _Computers |> List.filter (fun comp -> comp.Infected |> not)
    
    /// <summary>
    /// Attempts to attack all the computers
    /// </summary>   
    member this.SpreadInfection () =
        for comp in this.Computers do
            comp.AttackWithInfections this.InfectedComputers      
        stepsCount <- stepsCount + 1
    
    /// <summary>
    /// Returns string representation of current network state
    /// </summary>   
    member this.GetStatus () =        
        let mutable state = "Current state: "
        state <- state + "\nComputers — " + (this.Computers |> List.length |> string)
        state <- state + "\nInfected — " + (this.InfectedComputers |> List.length |> string) + ":"
        for comp in this.InfectedComputers do
            state <- state + "\n\t" + comp.ToString()
        state

    /// <summary>
    /// Launches network - starts the infection process
    /// </summary>   
    member this.Launch() =
        if (this.InfectedComputers |> List.isEmpty) then 
            printfn "%s" "Can't launch infecting process: no computers are infected. Try to infect at least one computer first"
        else        
            Console.ForegroundColor <- ConsoleColor.Red
            printfn "%s" "Infecting process started...\n"
            Console.ResetColor()
    
            printfn "%s" <| this.GetStatus() + "\n"
            this.Continue()

        
    member private this.Continue () =
        if (this.NotInfectedComputers |> List.isEmpty) then
            printfn "%s" "Finished. All computers infected."
        else
            if (stepsCount > maxStepsLimit) then
                printfn "%s" "Reached limit of steps allowed. Finished."
            else
                this.SpreadInfection()
                Console.ForegroundColor <- ConsoleColor.Red
                printfn "%s" "Infection has spreaded\n"
                Console.ResetColor()

                printfn "%s" <| this.GetStatus() + "\n"
                this.Continue()
   
