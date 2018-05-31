module Task3_PhoneBook

(* Task 5.3 *)
//Телефонный справочник

open System;

type Record =
    {
        Name : string
        Phone : string
    }

module Interface =
    let rec printData (records:List<Record>) =
        match records with
        | [] -> ()
        | head::tail ->
            printfn "%s: %s" head.Name head.Phone
            printData tail
    
    let rec printStrData (text:List<string>) =
        match text with
        | [] -> ()
        | head::tail ->
            printfn "%s" head
            printStrData tail

    let getCommand (description:string) =
        Console.ForegroundColor <- ConsoleColor.Green
        printf "%s" description
        Console.ResetColor()
        Console.ReadLine()

    let printMessage (message:string) =
        printfn "%s" message
    
    let printError (error:string) =
        Console.ForegroundColor <- ConsoleColor.Red
        printfn "%s" error
        Console.ResetColor()

module DataHandling =
    open Interface
    open System.IO

    let addRecord (name:string) (phone:string) (records:List<Record>) =
        let newRecord = { Name = name; Phone = phone }
        newRecord::records

    let findPhoneByName (name:string) (records:List<Record>) =
        records |> List.filter (fun record -> record.Name = name) |> List.map (fun record -> record.Phone)

    let findNameByPhone (phone:string) (records:List<Record>) =
        records |> List.filter (fun record -> record.Phone = phone) |> List.map (fun record -> record.Name)
    
    let codeToString (record:Record) =
        record.Name + ":" + record.Phone

    let saveDataToFile (path:string) (records:List<Record>) =
        use writer = new StreamWriter(File.OpenWrite(path))
        List.iter (fun record -> writer.WriteLine(record |> codeToString)) records
        writer.Close()

    let decodeFromString (str:string) =
        try 
            { Name = str.Split(':').[0]; Phone = str.Split(':').[1] }
        with 
            | _ -> failwith "Couldn't parse the string"

    let loadDataFromFile (path:string) =
        File.ReadAllLines(path) |> Seq.map (fun line -> decodeFromString line) |> Seq.toList
        
    let rec handleCommand (command:string) (records:List<Record>) =
        match command with
        | "exit" ->
            Interface.printMessage "Приходите еще!"
        | "add" -> 
            let name = Interface.getCommand "Введите имя: "
            let phone = Interface.getCommand "Введите номер: "
            Interface.printMessage "Запись добавлена." 
            handleCommand (Interface.getCommand "Введите команду: ") (addRecord name phone records)
        | "show" ->
            Interface.printMessage "Записи в справочнике:"
            Interface.printData records
            handleCommand (Interface.getCommand "Введите команду: ") records
        | "find number" | "find phone" ->
            let name = Interface.getCommand "Введите имя: "
            Interface.printMessage "Телефоны людей с указанным именем: "
            Interface.printStrData (findPhoneByName name records)
            handleCommand (Interface.getCommand "Введите команду: ") records
        | "find name" ->
            let phone = Interface.getCommand "Введите номер: "
            Interface.printMessage "Имена людей с указанным номером: "
            Interface.printStrData (findNameByPhone phone records)
            handleCommand (Interface.getCommand "Введите команду: ") records
        | "save" ->
            let path = Interface.getCommand "Введите путь для сохранения: "
            try
                saveDataToFile path records
                Interface.printMessage "Информация сохранена."
            with
                | _ as ex -> Interface.printError ("Не удалось сохранить данные:\n" + ex.Message)
            handleCommand (Interface.getCommand "Введите команду: ") records
        | "load" ->
            let path = Interface.getCommand "Введите путь исходного файла: "
            try
                let loadedRecords = loadDataFromFile path
                Interface.printMessage "Информация загружена."
                handleCommand (Interface.getCommand "Введите команду: ") loadedRecords
            with
                | _ as ex -> Interface.printError ("Не удалось загрузить данные:\n" + ex.Message)
                             handleCommand (Interface.getCommand "Введите команду: ") records
        | _ -> 
            Interface.printMessage "Неверная команда." 
            handleCommand (Interface.getCommand "Введите команду: " ) records
     
