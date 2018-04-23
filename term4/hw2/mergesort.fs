// Task 2.4 - Merge Sort

let rec mergesort ls =

    //Разбивает список на два почти равных по длине
    let split ls =
        let rec splitrest restOfList (firstHalf, secondHalf) = 
            match restOfList with
            | [] -> (firstHalf, secondHalf)
            | [last] -> (last::firstHalf, secondHalf)
            | first::second::rest -> splitrest rest (first::firstHalf, second::secondHalf)
        splitrest ls ([], [])
    
    //Склеивает два отсортированных списка в один
    let merge first second =
        let rec mergerest restOfFirst restOfSecond merged =
            match restOfFirst, restOfSecond with
            | [], [] -> merged
            | f, [] | [], f -> mergerest (f |> List.tail) [] ((f |> List.head) :: merged)
            | head1::tail1, head2::tail2 ->
                if head1 < head2 then 
                    mergerest tail1 restOfSecond (head1::merged)
                else mergerest restOfFirst tail2 (head2::merged)
        List.rev (mergerest first second [])

    let first, second = ls |> split
    match first, second with
    | f, [] | [], f -> f
    | _ -> merge (mergesort first) (mergesort second)