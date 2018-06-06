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
      