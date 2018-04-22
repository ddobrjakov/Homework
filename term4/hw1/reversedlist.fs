//Task 1.3 - Reversed List

//  starttorev   ending
//  [1,2,3,4,5]  []
//  [2,3,4,5]    [1]
//  [3,4,5]      [2,1]
//  [4,5]        [3,2,1] 
//  [5]          [4,3,2,1]
//  []           [5,4,3,2,1]

let reversed list =
    let rec revandconc starttorev ending =
        match starttorev with
        | [] -> ending
        | [a] -> a :: ending
        | _ -> revandconc (starttorev |> List.tail) ((starttorev |> List.head) :: ending)
    revandconc list []