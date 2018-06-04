module Tests8

open FsUnit
open NUnit.Framework
open Downloader

[<Test>] 
let ``No links`` () =
    let url = "http://mahalex.net/151-153/algebra.pdf"
    let pagesData = getPagesData url
    pagesData |> List.length |> should equal 1

[<Test>]
let ``Some links`` () =
    let url = "http://hwproj.me/courses/9/terms/4"
    let pagesData = getPagesData url
    pagesData |> List.length |> should equal 29

[<Test>]
let ``Incorrect input``() =
    let url = "not an address"
    let pageData = getPageDataAsync url |> Async.RunSynchronously

    pageData |> should equal <| NoData url
    getPagesData url |> List.length |> should equal 1