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

[<EntryPoint>]
let main argv =
    let save c = printfn "%A" c
    captureContacts save
    0
