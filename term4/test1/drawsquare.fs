let drawSquare n =
    let rec draw index =
        if (index = n * n) then ()
        else match index with
                | index when (index % n = 0 || index % n = n - 1 || index < n || index > n * (n - 1)) -> 
                    printf "*"
                    if index % n = n - 1 then printfn ""
                | _ -> printf " "
             draw (index + 1)
    if (n < 0) then ()
    else draw 0
            



