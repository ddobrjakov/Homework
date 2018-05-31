module Task1_Brackets

(* Task 5.1 *)
//Корректность скобочной последовательности в строке

let brackets = [('(', ')'); ('[', ']'); ('{', '}')]  
let isPair pair = brackets |> List.contains pair
let isBracket s = brackets |> List.filter(fun (a, b) -> a = s || b = s) |> List.isEmpty |> not
    
let checkStringBrackets str =
    let stringBrackets = str |> Seq.filter (fun symbol -> symbol |> isBracket) |> Seq.toList
    let rec checkBrackets bracketsBefore strChars =
        match strChars with
        | [] -> bracketsBefore |> List.isEmpty
        | strHead::strTail ->
            match bracketsBefore with
            | [] -> checkBrackets [strHead] strTail
            | head::tail -> 
                if isPair (head, strHead) then checkBrackets tail strTail
                else checkBrackets (strHead::bracketsBefore) strTail
    checkBrackets [] stringBrackets