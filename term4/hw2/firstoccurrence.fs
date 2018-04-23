// Task 2.2 - First Occurrence
let firstocc n ls =
    let rec index n ls i =
        if ls |> List.head = n
            then Some(i)
        else
            let tail = ls |> List.tail
            if tail = [] 
                then None
            else index n tail (i + 1)
    index n ls 0

