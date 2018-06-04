// Task 2.3 - IsPalindrome
let isPalindrome (s:string) =
    let ln = s |> String.length
    if (ln = 0) then true
    else Seq.forall(fun i -> (s.[i] = s.[ln - (i + 1)])) {0 .. ln / 2}