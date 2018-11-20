open System
open Contacts

let captureInput(label:string) = 
        Console.Write(label)
        Console.ReadLine()

let captureContact() =
    {
        Id = Guid.NewGuid()
        Firstname = captureInput "First name: "
        Lastname = captureInput "Last name: "
        Email = captureInput "Email: "
    }

let captureContactChoice saveContact =
    let contact = captureContact()
    saveContact contact
    let another = captureInput "Continue (Y/N)?"
    match another.ToUpper() with
    | "Y" -> Choice1Of2 ()
    | _ -> Choice2Of2 ()

let rec captureContacts saveContact =
    match captureContactChoice saveContact with
    | Choice1Of2 _ -> 
        captureContacts saveContact
    | Choice2Of2 _ -> ()

let printMenu() =
    Console.WriteLine("====================")
    Console.WriteLine("MENU")
    Console.WriteLine("====================")
    Console.WriteLine("1. Print Contacts")
    Console.WriteLine("2. Capture Contacts")
    Console.WriteLine("0. Quit")

let saveContact contact = printfn "%A" contact

let routeMenuOption i =
    match i with
    | "1" -> printfn "Contacts"
    | "2" -> captureContacts saveContact
    | _ -> printMenu()

let readKey() =
    let k = Console.ReadKey()
    Console.WriteLine()
    k.KeyChar |> string

[<EntryPoint>]
let main argv =
    printMenu()
    let mutable selection = readKey()
    while(selection <> "0") do
        routeMenuOption selection
        printMenu()
        selection <- readKey()
    0
