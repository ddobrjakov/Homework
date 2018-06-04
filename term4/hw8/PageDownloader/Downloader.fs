module Downloader

open System.Text.RegularExpressions
open System.Net
open System


/// <summary>
/// Возвращаемое значение при скачивании страницы (первая строка это адрес, вторая - содержимое)
/// </summary>
type DataResult =
    | SomeData of string * string
    | NoData of string

(* Возвращает все ссылки в заданном тексте страницы *)
let findAllLinks pageText =
    //let urlMask = Regex("<a href=\"(https?:\/\/(www\.)?[-a-zA-Z0-9@:%._\+~#=]{2,256}\.[a-z]{2,6}\b[-a-zA-Z0-9@:%_\+.~#?&\/=]*)\"")
    let urlMask = Regex("<a href=\"(https?://[^\"]+)\"")
    let matches = urlMask.Matches(pageText)
    let Links = seq {
                        for m in matches do
                            yield m.Groups.[1].Value
                    }
    Links

(* Возвращает для страницы ее адрес и содержимое *)
let getPageDataAsync url =
        async
            {
                try
                    let webClient = new WebClient()
                    let! pageData = webClient.AsyncDownloadString(Uri(url))
                    return SomeData (url, pageData)
                with ex -> return NoData (url)
            }    

(* Возвращает список из адреса и содержимого для каждой страницы включая начальную *)
let getPagesData mainPage =
    let pageData = getPageDataAsync mainPage |> Async.RunSynchronously
    match pageData with
    | NoData url -> [NoData url]
    | SomeData (url, data) ->
        let pageLinks = findAllLinks data
        let pageDownloads = pageLinks |> Seq.map(fun link -> getPageDataAsync link)
        pageData :: (Async.Parallel pageDownloads |> Async.RunSynchronously |> Array.toList)

(* Печатает результат скачивания одной страницы *)
let printInfo pageData =
    match pageData with
        | NoData url -> printfn "%s" <| url + " - Error (Не удалось загрузить)"
        | SomeData (url, text) -> printfn "%s" <| url + " - " + (text |> Seq.length |> string) + " символов"

(* Печатает результат скачивания всех страниц *)
let handleData pagesData =
    match pagesData with
        | mainPageData::otherData ->
            match mainPageData with
                | NoData mainURL -> printf "%s" <| mainURL + " - Не удалось скачать данные" 
                | d -> () //printInfo d
            if (otherData |> List.isEmpty) then printfn "%s" "Нет ссылок с заданной страницы" 
            for data in otherData do printInfo data
        | [] -> ()
