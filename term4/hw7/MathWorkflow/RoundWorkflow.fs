module RoundWorkflow

/// <summary>
/// Represents computation expression builder for calculations with given accuracy
/// </summary>
type RoundBuilder(accuracy: int) =
    let accuracy = accuracy

    member this.Bind(x: double, rest: double -> double) =
        rest <| System.Math.Round(x, accuracy)

    member this.Return (x: double) = 
        System.Math.Round(x, accuracy)