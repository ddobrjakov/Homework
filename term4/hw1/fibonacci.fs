//Task 1.2 - Fibonacci
//Возвращает все элементы последовательности Фибоначчи до числа till
//При till = 0 выводит все до переполнения int64
let fibonacci till =
    (int64(0), int64(1)) |> Seq.unfold (function
    | (x, y) when (y < int64(0)) || (y > till && till <> int64(0)) -> None
    | (x, y) -> Some(y, (y, x+y)))