// Task 1.4 - Power List
let powers n m =
    let rec gennext count next =
        match count with
        | _ when count < 0 -> []
        | _ -> next :: gennext (count - 1) (2 * next)
    gennext m (pown 2 n)

