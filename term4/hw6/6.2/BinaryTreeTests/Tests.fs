module Tests
open FsUnit
open NUnit.Framework
open Tree


let tree1 =
    BinaryTree().Insert(1.0).Insert(3.0).Insert(5.0).Insert(2.0).Insert(-3.0).Insert(4.0).Insert(13.0).Insert(8.0).Insert(6.0).Insert(-2.0).
        Insert(-5.0).Insert(-4.0).Insert(2.5)

[<Test>]
let ``Insert Test``() =
    tree1.RootNode |> should equal <| Node(1.0, 
                                    Node(-3.0, 
                                        Node(-5.0, 
                                            Empty, 
                                            Tip(-4.0)), 
                                        Tip(-2.0)), 
                                    Node(3.0, 
                                        Node(2.0, 
                                            Empty, 
                                            Tip(2.5)), 
                                        Node(5.0, 
                                            Tip(4.0), 
                                            Node(13.0, 
                                                Node(8.0, 
                                                    Tip(6.0), 
                                                    Empty), 
                                                Empty))))

[<Test>]
let ``Enumerator Test``() =
    tree1 |> Seq.toList |> should equal [-5.0; -4.0; -3.0; -2.0; 1.0; 2.0; 2.5; 3.0; 4.0; 5.0; 6.0; 8.0; 13.0]

[<Test>]
let ``Delete Test 1``() =
    let tree = BinaryTree<float>(tree1.RootNode)
    tree.Delete(5.0).RootNode |> should equal <| Node(1.0, 
                                                    Node(-3.0, 
                                                        Node(-5.0, 
                                                            Empty, 
                                                            Tip(-4.0)), 
                                                        Tip(-2.0)), 
                                                    Node(3.0, 
                                                        Node(2.0, 
                                                            Empty, 
                                                            Tip(2.5)), 
                                                        Node(6.0, 
                                                            Tip(4.0), 
                                                            Node(13.0, 
                                                                Tip(8.0), 
                                                                Empty))))

[<Test>]
let ``Delete Test 2``() =
    let tree = BinaryTree<float>(tree1.RootNode)
    tree.Delete(-5.0).Delete(1.0).Delete(2.5).Delete(-4.0).Delete(6.0).Delete(13.0).Delete(3.0).
        Delete(5.0).Delete(-3.0).Delete(2.0).Delete(-2.0).Delete(8.0).RootNode |> should equal <| Tip(4.0)   
    tree.Delete(4.0).RootNode |> should equal TreeNode<float>.Empty

[<Test>] 
let ``Insertion-Deletion test`` () =
    let R = System.Random()
    let tree = BinaryTree()
    let mutable listValues = []:List<int>
    let mutable value = 0
    for i in seq{1..50} do
        value <- R.Next(100)
        listValues <- value::listValues
        tree.Insert value |> ignore
    for v in (listValues |> List.sortBy (fun x -> R.Next(100))) do
        tree.Delete(v) |> ignore
    tree.RootNode |> should equal TreeNode<int>.Empty

[<Test>]
let ``Contains test``() =
    let tree = BinaryTree<float>(tree1.RootNode)
    tree.Contains(-5.0) |> should equal true
    tree.Contains(-3.0) |> should equal true
    tree.Contains(2.5) |> should equal true
    tree.Contains(0.0) |> should equal false
    tree.Contains(7.0) |> should equal false
    tree.Contains(15.0) |> should equal false
    tree.Contains(20.0) |> should equal false

    let emptyTree = BinaryTree()
    emptyTree.Contains(1.0) |> should equal false

let randomTree size min max =
    let R = System.Random()
    let tree = BinaryTree()
    let mutable listValues = []:List<int>
    let mutable value = 0
    for i in seq{1..size} do
        value <- min + R.Next(max - min)
        listValues <- value::listValues
        tree.Insert value |> ignore
    tree

[<Test>]
let ``Insertion / Enumerator test``() =
    let mutable tree = BinaryTree()
    for i in seq{1..20} do
        tree <- randomTree 50 -100 100
        tree |> Seq.toList |> should be ascending
