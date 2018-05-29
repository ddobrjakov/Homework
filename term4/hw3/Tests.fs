module Tests

open NUnit.Framework;
open FsUnit;
open FsCheck;
open HW3;
open HW3.Task1;
open HW3.Task2;
open HW3.Task3;
open HW3.Task4;

    (* Task 3.1 *)
[<Test>]
let ``Filter method``() =
    evenCountFilter [7; 0; 3; 4; 8; -2; 10] |> should equal 5
    
[<Test>]
let ``Check functions equivalence``() = 
    Check.Quick(fun list -> (evenCountFilter (list) = evenCountMap (list)) && (evenCountMap (list) = evenCountFold (list)))

[<TestFixture>]
module ``Even count functions tests`` =
    let Data = 
        [
            TestCaseData([]:List<int>).Returns(0);
            TestCaseData([1; 2; 3; 4; 5]).Returns(2);
            TestCaseData([-2; 4; 6; 0; 1; 3; 5; 8]).Returns(5);
            TestCaseData([2]).Returns(1);
            TestCaseData([3]).Returns(0);
        ]

    [<TestCaseSource("Data")>]
    let testsFilter list = 
        evenCountFilter list

    [<TestCaseSource("Data")>]
    let testsMap list = 
        evenCountMap list

    [<TestCaseSource("Data")>]
    let testsFold list = 
        evenCountFold list

    (* Task 3.2 *)
[<Test>]
let ``Only one node``() = 
    treeMap (Tip(3), fun x -> x * 53) |> should equal <| Tip(159)

[<Test>]
let ``x + 1``() =
    treeMap (Node(1, Node(2, Tip(3), Tip(4)), Node(5, Tip(6), Tip(7))), fun x -> x + 1) |> should equal <| Node(2, Node(3, Tip(4), Tip(5)), Node(6, Tip(7), Tip(8)))

[<Test>]
let ``x % 3``() =
    treeMap (Node(5, Node(2, Tip(0), Tip(4)), Node(5, Tip(6), Tip(7))), fun x -> x % 3) |> should equal <| Node(2, Node(2, Tip(0), Tip(1)), Node(2, Tip(0), Tip(1)))

[<Test>]
let ``x ^ 2``() =
    treeMap (Node(1, Node(0, Tip(3), Tip(4)), Tip(-7)), fun x -> x * x) 
    |> should equal <| Node(1, Node(0, Tip(9), Tip(16)), Tip(49))

    (* Task 3.3 *)
[<Test>]
let ``Calculating sum``() =
    Calculate(Sum(None(5), None(-2))) |> should equal 3

[<Test>]
let ``Calculating expression``() =
    Calculate(Sum(Mult(None(5), Div(None(10), None(2))) , None(7))) |> should equal 32

[<Test>]
let ``Division by zero``() =
    (fun () -> Calculate(Div(None(2), None(0))) |> ignore) |> should throw typeof<System.Exception>

    (* Task 3.4 *)
[<Test>]
let ``Generate primes``() =
    Primes() |> Seq.take(5) |> Seq.toList |> should equal [2; 3; 5; 7; 11] 