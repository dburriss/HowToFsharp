namespace Contacts

module Input = 

    open System
    
    let captureInput(label:string) = 
            printf "%s" label
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
        printfn "===================="
        printfn "MENU"
        printfn "===================="
        printfn "1. Print Contacts"
        printfn "2. Capture Contacts"
        printfn "0. Quit"

    let saveContact contact = printfn "%A" contact

    let routeMenuOption i getContacts saveContact =
        match i with
        | "1" -> 
            printfn "Contacts"
            getContacts() |> List.iter (fun c -> printfn "%s %s (%s)" c.Firstname c.Lastname c.Email)
        | "2" -> captureContacts saveContact
        | _ -> printMenu()

    let readKey() =
        let k = Console.ReadKey()
        Console.WriteLine()
        k.KeyChar |> string