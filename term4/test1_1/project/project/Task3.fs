module Task3



///Task 2: Palindrome

let biggestPalindrome () =
    let isPalindrome (s:string) =
        let ln = s |> String.length
        if (ln = 0) then true
        else Seq.forall(fun i -> (s.[i] = s.[ln - (i + 1)])) {0 .. ln / 2}

    let isPalindrome (i:int) =
        isPalindrome (i |> string)
        
    let numbers = seq{100..999}
    (Seq.allPairs numbers numbers)|> Seq.map(fun (a, b) -> a * b) |> Seq.filter(fun c -> isPalindrome c) |> Seq.max
      



//Task 3

type StackNode<'a> (v:'a, prev:StackNode<'a> option) =
    member this.Value = v
    member this.Previous:StackNode<'a> option = prev


type MyStack<'a> () =

    let mutable _Head:StackNode<'a> option = None

    member this.Push (value:'a) =
        let newNode = StackNode<'a> (value, _Head)
        _Head <- Some(newNode)
      
    member this.Pop () =
        match _Head with
        | Some(x) -> 
            let elem = x.Value
            _Head <- x.Previous
            elem
        | None -> failwith "No elements"

    member this.isEmpty =
        _Head = None