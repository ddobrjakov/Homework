module Tests
open Task1_Brackets;
open Task2_PointFree;
open Task3_PhoneBook;
open FsUnit;
open FsCheck;
open NUnit.Framework
open System.IO
open System

(* 5.1 Tests *)
[<TestFixture>]
module ``Brackets tests`` =
    let CorrectData =
        [
            TestCaseData("").Returns(true);
            TestCaseData("()").Returns(true);
            TestCaseData("([])").Returns(true);
            TestCaseData("{[acv]}").Returns(true);
            TestCaseData("a1[s]{bg}").Returns(true);
            TestCaseData("[()b{}]a").Returns(true);
            TestCaseData("{{{}}()}").Returns(true);
            TestCaseData("abc").Returns(true);
            TestCaseData("([[][]][])").Returns(true);
            TestCaseData("[-[--{ () () [{} {} {[]}+]}()]] ").Returns(true);
        ]
    let InCorrectData =
        [
            TestCaseData("(").Returns(false);
            TestCaseData("]").Returns(false);
            TestCaseData("(}").Returns(false);
            TestCaseData("ad)").Returns(false);
            TestCaseData("(()").Returns(false);
            TestCaseData("())").Returns(false);
            TestCaseData("[{]}").Returns(false);
            TestCaseData(")(").Returns(false);
            TestCaseData("([{abc}d)[]").Returns(false);
        ]

    [<TestCaseSource("CorrectData"); TestCaseSource("InCorrectData")>]
    let TestFunction str = 
        checkStringBrackets str
    


(* 5.2 Tests *)
[<Test>]
let ``Check source and result functions equivalence``() = 
    Check.Quick(fun x list -> resultFunc x list = sourceFunc x list)

[<Test>]
let ``Check source function and func'1 for equivalence``() =
    Check.Quick(fun x list -> func'1 x list = sourceFunc x list)

[<Test>]
let ``Check source function and func'2 for equivalence``() =
    Check.Quick(fun x list -> func'2 x list = sourceFunc x list)
    
[<Test>]
let ``Check source function and func'3 for equivalence``() =
    Check.Quick(fun x list -> func'3 x list = sourceFunc x list)



(* 5.3 Tests *)
[<Test>]
let ``Record adding test``() =
    DataHandling.addRecord "Michael" "89111111111" [{ Name = "John"; Phone = "+79819818181"}] 
        |> should equal [{ Name = "Michael"; Phone = "89111111111" }; { Name = "John"; Phone = "+79819818181" }]

[<Test>]
let ``Number finding test``() =
    let data = 
        [
            { Name = "Paul"; Phone = "+1 123 08 08 08" };
            { Name = "Jake"; Phone = "+135 987 654 32 10" };
            { Name = "Kevin"; Phone = "+3 141 592 65 35" };
            { Name = "Feliks"; Phone = "+2 718 281 82 84"};
            { Name = "Donald"; Phone = "+1 900 000 00 00" }
        ]
    DataHandling.findPhoneByName "Kevin" data |> should equal ["+3 141 592 65 35"]

[<Test>]
let ``Name finding test``() =
    let data = 
        [
            { Name = "Henry"; Phone = "+7 921 544 63 37" };
            { Name = "Harold"; Phone = "+14 500 01 01 01" };
            { Name = "Alisa"; Phone = "+2 345 678 90 12" };
            { Name = "Patrick"; Phone = "+5 101 101 10 01"};
            { Name = "Max"; Phone = "+5 101 101 10 01" }
        ]
    DataHandling.findNameByPhone "+5 101 101 10 01" data |> should equal ["Patrick"; "Max"]

[<Test>]
let ``Name finding test, empty data``() =
    DataHandling.findNameByPhone "+5 101 101 10 01" [] |> should equal []

[<Test>]
let ``Number finding test, empty result``() =
    let data = 
        [
            { Name = "Anthony"; Phone = "+1 123 08 08 08" };
        ]
    DataHandling.findPhoneByName "Vova" data |> should equal []

[<Test>]
let ``Save/load test``() =
    let data =
        [
            { Name = "A"; Phone = "1" };
            { Name = "B"; Phone = "2" };
            { Name = "B"; Phone = "3" };
            { Name = "C"; Phone = "3"};
            { Name = "D"; Phone = "4" }
        ]
    let path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "test.txt")
    DataHandling.saveDataToFile path data;
    let dataRestored = DataHandling.loadDataFromFile path
    data |> should equal dataRestored

[<Test>]
let ``Load from unexisting file``() =
    let path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ThisFileDoesNotExist.AtAll")
    (fun () -> DataHandling.loadDataFromFile path |> ignore) |> should throw typeof<System.IO.FileNotFoundException>
