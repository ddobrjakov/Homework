module Computer
open OpSystem

/// <summary>
/// Represents the computer
/// </summary>
type Computer(computerOS: OpSystem, connectedTo: List<Computer>, name:string) =

    let mutable _OS = computerOS
    let mutable _Infected = false
    let mutable _Connected = connectedTo

    /// <summary>
    /// Creates computer with no connections and no name
    /// </summary>
    new (computerOS: OpSystem) =
        Computer(computerOS, ([]: List<Computer>), "Comp")
    
    /// <summary>
    /// Creates computer with no connections
    /// </summary>
    new (computerOS: OpSystem, name: string) =
        Computer(computerOS, ([]: List<Computer>), name)
    
    /// <summary>
    /// Returns the name of computer
    /// </summary>
    member val Name = name with get
    
    /// <summary>
    /// Returns the OS of computer
    /// </summary>
    member this.OS
        with get() = _OS
        and private set(value) = _OS <- value
       
    /// <summary>
    /// Returns if computer is infected
    /// </summary>
    member this.Infected
        with get() = _Infected
        and private set(value) = _Infected <- value
    
    /// <summary>
    /// Returns list of connected computers
    /// </summary>
    member this.Connected
        with get() = _Connected
        and private set(value) = _Connected <- value

    /// <summary>
    /// Connects to another computer
    /// </summary>
    /// <param name="computer">Computer to connect to</param>
    member this.Connect computer =
        _Connected <- computer :: _Connected
    
    /// <summary>
    /// Connects to multiple comptures at once
    /// </summary>
    /// <param name="computers">Computers to connect to</param>
    member this.Connect computers =
        _Connected <- computers @ _Connected

    /// <summary>
    /// Conducts a malware-attack on this computer, it depends on the OS whether it's actually going to be infected
    /// </summary>
    member this.AttackWithInfections () =
        //Computer gets infected in case it's connected to at least one infected computer and OS doesn't defend
        if (_Connected |> List.filter(fun comp -> comp.Infected) |> List.isEmpty |> not && not (_OS.CanDefend())) then
            this.Infected <- true
    
    /// <summary>
    /// Attacks computer with only viruses the given list of computers has
    /// </summary>
    /// <param name="from"> Computers trying to infect this one </param>
    member this.AttackWithInfections from =
        if (_Connected |> List.filter(fun comp -> comp.Infected && from |> List.contains comp) |> List.isEmpty |> not && not (_OS.CanDefend())) then
            this.Infected <- true



    /// <summary>
    /// Infects the computer independently of operational system defending level
    /// </summary>
    member this.ForceInfect () =
        this.Infected <- true

    override this.ToString () =
        name + " (" + this.OS.Name + ")"
