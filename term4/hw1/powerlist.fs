// Task 1.4 - Power List
let powers n m =
    let rec genNext count next =
        match count with
        | _ when count < 0 -> []
        | _ -> next :: genNext (count - 1) (2 * next)
    genNext m (pown 2 n)

