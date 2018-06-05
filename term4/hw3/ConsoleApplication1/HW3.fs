module HW3

(* Task 3.1 *)
module Task1 =
    let evenCountFilter list =
        list |> List.filter (fun x -> (x % 2 = 0)) |> List.length

    let evenCountMap list =
        list |> List.map (fun x -> (abs x + 1) % 2) |> List.sum

    let evenCountFold list =
        list |> List.fold (fun acc x -> acc + (abs x + 1) % 2) 0

(* Task 3.2 *)
module Task2 =
    type Tree<'a> = 
        | Tip of 'a
        | Node of 'a * Tree<'a> * Tree<'a>

    let rec treeMap (tree:Tree<'a>) func =
        match tree with
        | Node(a, t1, t2) -> Node(func a, treeMap t1 func, treeMap t2 func)        
        | Tip(a) -> Tip(func(a))

(* Task 3.3 *)
module Task3 =
    type Operation<'a> =
        | None of 'a
        | Neg of 'a
        | Sum of Operation<'a> * Operation<'a>
        | Diff of Operation<'a> * Operation<'a>
        | Mult of Operation<'a> * Operation<'a>
        | Div of Operation<'a> * Operation<'a>
    
    let rec Calculate (op:Operation<int>) =
        match op with
        | None(value) -> value
        | Neg(value) -> -value 
        | Sum(value1, value2) -> Calculate(value1) + Calculate(value2)
        | Diff(value1, value2) -> Calculate(value1) - Calculate(value2)
        | Mult(value1, value2) -> Calculate(value1) * Calculate(value2)
        | Div(value1, value2) -> 
            if (Calculate(value2) = 0) then failwith "Division by zero is not allowed"
            Calculate(value1) / Calculate(value2)

(* Task 3.4 *)
module Task4 =
    let Primes () =
        let isPrime n =
            {2..((float n) |> sqrt |> floor |> int)} |> Seq.exists (fun x -> n % x = 0) |> not
        Seq.initInfinite (fun x -> x + 2) |> Seq.filter isPrime