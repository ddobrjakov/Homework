module OpSystem
open System

/// <summary>
/// Represents interface of system defending tools which can determinine if the system is infected after virus attack  
/// </summary>
type IDefendingTools =
    /// <summary>
    /// Returns if system can be prevented from getting infected
    /// </summary>
    abstract member CanDefend : unit -> bool

/// <summary>
/// Represents class of defending tools based on simple probability
/// </summary>
type SimpleDefendingTools(probabilityOfDefenfing: double) =  
    let Probability = probabilityOfDefenfing
    let R = new Random()
    interface IDefendingTools with
        member this.CanDefend() = 
            R.NextDouble() <= Probability


/// <summary>
/// Represents the operational system
/// </summary>
type OpSystem(name: string, tools: IDefendingTools) =

    let Tools = tools

    /// <summary>
    /// Returns system name
    /// </summary>
    member this.Name = name

    /// <summary>
    /// Returns if system can survive the virus attack
    /// </summary>
    member this.CanDefend 
        with get() = Tools.CanDefend