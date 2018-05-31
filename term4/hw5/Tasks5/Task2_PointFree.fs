module Task2_PointFree

(* Task 5.2 *)
//Записать в point-free стиле func x l = List.map (fun y -> y * x) l

let sourceFunc x l = List.map (fun y -> y * x) l
let func'1 x = List.map (fun y -> y * x)
let func'2 x = List.map (fun y -> (*) x y)
let func'3 x = List.map << (*) <| x
let func'4 = List.map << (*)
let resultFunc = List.map << (*)