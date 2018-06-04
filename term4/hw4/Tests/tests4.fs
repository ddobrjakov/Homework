module tests4

open FsUnit
open NUnit.Framework
open Reduction

[<Test>]
let ``Beta-reduction test 1``() =
    Var 'a' |> betaReduce |> should equal <| Var 'a'
    
[<Test>]
let ``Beta-reduction test 2``() =
    Application (Abstraction('a', Var 'a' ), Var 'b') |> betaReduce |> should equal <| Var 'b'

[<Test>]
let ``Beta-reduction test 3``() =
    Application (Abstraction('a', Application(Var 'a', Var 'c')), Abstraction('x', Application(Var 'b', Var 'x'))) |> betaReduce |> 
        should equal <| Application (Var 'b', Var 'c')

[<Test>]
let ``I I = I test``()=
    let I = Abstraction('x', Var 'x')
    Application(I, I) |> betaReduce |> should equal I

[<Test>]
let ``Getting free name test`` () =
    getFreeName (Abstraction('c', Application(Var 'c', Var 'a'))) (Var 'b') |> should equal 'c'

[<Test>]
let ``Getting free variables test``() =
    getFV (Application(Abstraction('x', Application(Var 'b', Var 'x')), Var 'a')) |> should equal ['b'; 'a']

[<Test>]
let ``Substitution test``() =
    let A = Abstraction('x', Application(Var 'x', Var 'x'))
    let B = Abstraction('y', Var 'z')
    let result = Abstraction('x', Application (Abstraction ('y', Var 'z'), Abstraction ('y', Var 'z')))
    substitute A 'x' B |> should equal result