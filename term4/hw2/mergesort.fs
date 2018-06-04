// Task 2.4 - Merge Sort

let rec mergeSort ls =

    //Разбивает список на два почти равных по длине
    let split ls =
        let rec splitRest restOfList (firstHalf, secondHalf) = 
            match restOfList with
            | [] -> (firstHalf, secondHalf)
            | [last] -> (last::firstHalf, secondHalf)
            | first::second::rest -> splitRest rest (first::firstHalf, second::secondHalf)
        splitRest ls ([], [])
    
    //Склеивает два отсортированных списка в один
    let merge first second =
        let rec mergeRest restOfFirst restOfSecond merged =
            match restOfFirst, restOfSecond with
            | [], [] -> merged
            | f, [] | [], f -> mergeRest (f |> List.tail) [] ((f |> List.head) :: merged)
            | head1::tail1, head2::tail2 ->
                if head1 < head2 then 
                    mergeRest tail1 restOfSecond (head1::merged)
                else mergeRest restOfFirst tail2 (head2::merged)
        List.rev (mergeRest first second [])

    let first, second = ls |> split
    match first, second with
    | f, [] | [], f -> f
    | _ -> merge (mergeSort first) (mergeSort second)