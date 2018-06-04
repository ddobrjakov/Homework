module Tests9
open Lazy
open Lazy.Factory
open Lazy.LazyImplementations
open Lazy.Interface
open FsUnit
open NUnit.Framework
open System.Threading


type Counter() =
    let mutable count = 0
    member this.Supplier () =
        count <- count + 1
        1
    member this.SupplierObject () =
        count <- count + 1
        box 1
    member this.Count with get() = count

[<Test>]
let ``Single-threaded Lazy is calculated only once``() =
    let counter = Counter()
    let singleThreaded = LazyFactory.CreateSingleThreadedLazy(fun () -> counter.Supplier())

    //Calling Get() 10 times
    seq{1..10} |> Seq.iter (fun i -> singleThreaded.Get() |> ignore)

    //Checking if supplier was called only once in total
    counter.Count |> should equal 1

[<Test>]
let ``Multi-threaded Lazy is calculated only once``() =
    let counter = Counter()
    let multiThreaded = LazyFactory.CreateMultiThreadedLazy(fun () -> counter.Supplier())

    //Initialzing and running threads
    let threads = seq{1..10} |> Seq.map (fun s -> new Thread(fun () -> multiThreaded.Get() |> ignore)) |> (Seq.toArray)
    threads |> Array.iter (fun t -> t.Start())
    threads |> Array.iter (fun t -> t.Join())

    //Checking if supplier was called only once in total
    counter.Count |> should equal 1

[<Test>]
let ``Lock-free Lazy is calculated only once in one-thread mode``() =
    let counter = Counter()
    let lockFree = LazyFactory.CreateLockFreeLazy<obj>(fun () -> counter.SupplierObject())
    
    //Calling Get() 10 times
    seq{1..10} |> Seq.iter (fun i -> lockFree.Get() |> ignore)

    //Checking if supplier was called only once in total
    counter.Count |> should equal 1

[<Test>]
let ``Lock-free Lazy returns correct value``()=
    let lF = LazyFactory.CreateLockFreeLazy<obj>(fun () -> box 1)
    lF.Get() |> should equal <| box 1

[<Test>]
let ``Lock-free Lazy returns correct value with many threads``() =
    let counter = Counter()
    let lockFree = LazyFactory.CreateLockFreeLazy<obj>(fun () -> box 1)
    let threads = seq{1..10} |> Seq.map (fun s -> new Thread(fun () -> unbox (lockFree.Get()) |> should equal 1)) |> Seq.toArray
    threads |> Array.iter (fun t -> t.Start())
    threads |> Array.iter (fun t -> t.Join())

[<Test>]
let ``Different threads return the same object (lock-free)`` () =
    let lockFree = LazyFactory.CreateLockFreeLazy<obj>(fun () -> box 3)

    //Concurrent bag to save results of Get() from different threads
    let bag = System.Collections.Concurrent.ConcurrentBag<obj>()
    
    //Initializing and running threads
    let threads = seq{1..10} |> Seq.map (fun s -> new Thread(fun () -> bag.Add (lockFree.Get()) )) |> (Seq.toArray)
    threads |> Array.iter (fun t -> t.Start())
    threads |> Array.iter (fun t -> t.Join())
    
    //Checking equality (by reference)
    bag |> Seq.pairwise |> Seq.iter(fun (a, b) -> Assert.AreSame(a, b))