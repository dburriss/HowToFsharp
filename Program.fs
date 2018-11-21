open Contacts
open Contacts.Input

let getContacts() =
    Data.all()
    |> fun r -> match r with
                | Ok cs -> cs |> Seq.toList
                | Error e -> 
                        printfn "ERROR: %s" e.Message
                        List.empty

let insertContact c =
    Data.insert c 
    |> fun r -> match r with
                | Ok i -> printfn "%i records inserted" i
                | Error e -> printfn "ERROR: %s" e.Message

[<EntryPoint>]
let main argv =
    printMenu()
    let mutable selection = readKey()
    while(selection <> "0") do
        routeMenuOption selection getContacts insertContact 
        printMenu()
        selection <- readKey()
    0
