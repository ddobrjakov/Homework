module Tree

open System.Collections
open System.Collections.Generic

/// <summary>
/// Represents node of tree
/// </summary>
type TreeNode<'a when 'a: comparison> =
    | Empty
    | Node of 'a * TreeNode<'a> * TreeNode<'a>

    /// <summary>
    /// Quick simple way of node string representation
    /// </summary>
    override this.ToString() =
        match this with
            | Empty -> "Empty"
            | Node(a, left, right) when not (left = Empty && right = Empty) -> 
                a.ToString() + ": (" + left.ToString() + ", " + right.ToString() + ")"
            | Node(a, left, right) -> a.ToString()

(* Short form of writing node without sub-nodes *)
let Tip a = Node(a, Empty, Empty)
       
/// <summary>
/// Represents class of tree
/// </summary>
type BinaryTree<'a when 'a: comparison>(rootNode: TreeNode<'a>) =
    
    let mutable root = rootNode

    /// <summary>
    /// Returns root node of the tree
    /// </summary>
    member this.RootNode
        with get() = root
    
    /// <summary>
    /// Returns left sub-tree
    /// </summary>
    member this.LeftSubTree
        with get() = 
            match root with
                | Empty -> None
                | Node(rootValue, left, right) -> Some(BinaryTree<'a>(left))

    /// <summary>
    /// Returns right sub-tree
    /// </summary>    
    member this.RightSubTree
        with get() = 
            match root with
                | Empty -> None
                | Node(rootValue, left, right) -> Some(BinaryTree<'a>(right))
    
    /// <summary>
    /// Creates tree with one node storing given value 
    /// </summary>
    new (a:'a) =
        BinaryTree<'a>(Tip a)

    /// <summary>
    /// Creates empty tree
    /// </summary>
    new() =
        BinaryTree<'a>(Empty: TreeNode<'a>)
    
    /// <summary>
    /// Inserts value into the tree
    /// </summary>
    member this.Insert value =
        let rec insertIntoNode node value =
            match node with
                | Empty -> Tip(value)
                | Node(nodeValue, left, right) when nodeValue < value -> 
                    Node(nodeValue, left, value |> insertIntoNode right)
                | Node(nodeValue, left, right) ->
                    Node(nodeValue, insertIntoNode left value, right)
        root <- (value |> insertIntoNode root)
        this

    /// <summary>
    /// Deletes value from the tree if exists
    /// </summary>
    member this.Delete value =
        let rec getMinNode (node: TreeNode<'a>) = 
            match node with
            | Node(_, left, _) -> 
                match left with
                    | Empty -> node                                   
                    | _ -> getMinNode left 
            | _ -> node

        let rec removeFromNode node value =
            match node with
                | Empty -> Empty
                | Node(a, Empty, Empty) -> 
                    if (a = value) then Empty
                    else node
                | Node(a, left, right) when a < value ->
                    Node(a, left, removeFromNode right value)
                | Node(a, left, right) when a > value ->
                    Node(a, removeFromNode left value, right)
                | Node(a, left, right) ->
                    match right with
                        | Empty -> left
                        | Node(b, Empty, rightRight) -> Node(b, left, rightRight)
                        | Node(b, rightLeft, rightRight) ->
                            let minRightLeft = getMinNode rightLeft
                            match minRightLeft with
                                | Node(c, _, _) -> Node(c, left, c |> removeFromNode right)
                                | _ -> failwith "Min node should exist and be not empty because rightLeft is not empty"
                            
        root <- removeFromNode root value
        this
 
    /// <summary>
    /// Returns if given value exists inside the tree
    /// </summary>
    member this.Contains value =
        let rec nodeContains node value =
            match node with
            | Empty -> false
            | Node(a, left, right) -> 
                match value with
                    | _ when a = value -> true
                    | _ when a < value -> nodeContains right value
                    | _ -> nodeContains left value               
        nodeContains root value

    interface IEnumerable<'a> with
        member this.GetEnumerator() =
            let rec valuesSeq node = 
                seq
                    {
                        match node with
                            | Empty -> ()
                            | Node(a, left, right) -> 
                                yield! valuesSeq left
                                yield a
                                yield! valuesSeq right
                    }
            (valuesSeq root).GetEnumerator()

        member this.GetEnumerator(): IEnumerator = 
            (this :> IEnumerable<'a>).GetEnumerator() :> IEnumerator

    /// <summary>
    /// Prints tree to console
    /// </summary>
    member this.Print () =
        printfn "%s" <| root.ToString()

 