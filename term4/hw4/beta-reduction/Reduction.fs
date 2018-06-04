module Reduction


type Term =
    | Var of char
    | Application of Term * Term
    | Abstraction of char * Term


let rec getFV term =
    match term with
        | Var x -> [x]
        | Application (T1, T2) -> (getFV T1) @ (getFV T2)
        | Abstraction (x, T) -> getFV T |> List.except [x]


let enAlphabet = seq{'a'..'z'} |> Seq.toList
let ruAlphabet = seq{'а'..'я'} |> Seq.toList
let grAlphabet = seq{'α'..'ω'} |> Seq.toList
let korAlphabet = seq{'ㄱ'..'ㅢ'} |> Seq.toList
let allSymbols = enAlphabet @ grAlphabet @ ruAlphabet @ korAlphabet

let getFreeName T1 T2 =
    let freeSymbols = allSymbols |> List.except (getFV T1 @ getFV T2)
    match freeSymbols with
        | [] -> failwith "We've runned out of names"
        | _ -> freeSymbols |> List.head 
   
   
let rec substitute termIn varToReplace termBy =
    match termIn with
        | Var x -> 
            if (x = varToReplace) then termBy
            else Var(x)
        | Application (T1, T2) -> 
            Application(substitute T1 varToReplace termBy, substitute T2 varToReplace termBy)
        | Abstraction (x, T) ->
            match termBy with
                | Var _ when varToReplace = x -> Abstraction(x, T)
                | _ when not (getFV T |> List.contains varToReplace) || not (getFV termBy |> List.contains x) ->
                    Abstraction (x, substitute T varToReplace termBy)
                | _ ->
                    let xRenamed = getFreeName T termBy
                    let a = substitute T x (Var xRenamed)
                    Abstraction (xRenamed, substitute (substitute T x (Var xRenamed)) varToReplace termBy)
            
           
let rec betaReduce term =
    match term with
        | Var x -> Var x
        | Application (T1, T2) ->
            match T1 with
                | Abstraction (x, T) ->
                    betaReduce (substitute T x T2)
                | _ -> Application (T1, T2)
        | Abstraction (x, T) ->
            Abstraction (x, betaReduce T) 
