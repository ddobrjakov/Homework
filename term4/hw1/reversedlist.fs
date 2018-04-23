//Task 1.3 - Reversed List

//  startToRev   ending
//  [1,2,3,4,5]  []
//  [2,3,4,5]    [1]
//  [3,4,5]      [2,1]
//  [4,5]        [3,2,1] 
//  [5]          [4,3,2,1]
//  []           [5,4,3,2,1]

let reversed list =
    let rec revAndConc startToRev ending =
        match startToRev with
        | [] -> ending
        | [a] -> a :: ending
        | _ -> revAndConc (startToRev |> List.tail) ((startToRev |> List.head) :: ending)
    revAndConc list []