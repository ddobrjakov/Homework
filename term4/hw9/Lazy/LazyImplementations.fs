namespace Lazy

module LazyImplementations =
    open Interface
    open System.Threading

    /// <summary>
    /// Represents class performing single-threaded lazy evaluations
    /// <param> 
    type SingleThreadedLazy<'a> (supplier: unit -> 'a) =
        let mutable calculated = None:'a option
        interface ILazy<'a> with
            member this.Get () =
                match calculated with
                    | None ->
                        let result = supplier ()
                        calculated <- Some result
                        result
                    | Some result -> result

    /// <summary>
    /// Represents class performing multi-threaded lazy evaluations using locks
    /// <param> 
    type MultiThreadedLazy<'a> (supplier: unit -> 'a) =
        let mutable calculated = None:'a option
        let obj = new obj()
        interface ILazy<'a> with
            member this.Get () =
                lock (obj) (fun () -> if calculated.IsNone then (calculated <- Some (supplier ())))
                calculated.Value

    /// <summary>
    /// Represents class performing multi-threaded lazy evaluations lock-free
    /// <param> 
    type LockFreeLazy<'a when 'a: not struct> (supplier: unit -> 'a) =
        let mutable result = Unchecked.defaultof<'a>
        let mutable calculated: bool = false

        let mutable startRes = Unchecked.defaultof<'a>
        let mutable calculatedStart: bool = false

        interface ILazy<'a> with
            member this.Get () =
                while not calculated do
                    startRes <- result
                    let funcValue = supplier()

                    // If (result != startRes) — it means someone has changed it so we shouldn't
                    // Otherwise we do it 
                    Interlocked.CompareExchange(&result, funcValue, startRes) |> ignore

                    //At this point either we calculated result, or someone else did
                    calculated <- true
                result