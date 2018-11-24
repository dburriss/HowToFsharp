open Contacts

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
    Input.printMenu()
    let mutable selection = Input.readKey()
    while(selection <> "0") do
        Input.routeMenuOption selection getContacts insertContact 
        Input.printMenu()
        selection <- Input.readKey()
    0
