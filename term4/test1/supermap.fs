open NUnit.Framework
open FsUnit

let superMap list f =
    let rec supermapAcc resAcc listRest =
        match listRest with
        | [] -> resAcc
        | head::tail -> supermapAcc ((List.rev <| (f head)) @ resAcc) tail
    List.rev <| supermapAcc [] list


[<Test>]
superMap [1;2;3;4;5] (fun x -> [x; x * x; x * x * x]) |> should equal [1; 1; 1; 2; 4; 8; 3; 9; 27; 4; 16; 64; 5; 25; 125]
superMap [1;0] (fun x -> [x; x * x; x * x * x]) |> should equal [1; 1; 1; 0; 0; 0]
superMap [1;2] (fun x -> [x + 1; x; x - 1]) |> should equal [2; 1; 0; 3; 2; 1]
        
