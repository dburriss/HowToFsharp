namespace Contacts

open System

type Contact = {
    Id:Guid
    Firstname:string
    Lastname:string
    Email:string
}

[<RequireQualifiedAccess>]
module Contact =
    let private isValidEmail (email:string) =
        try
            new System.Net.Mail.MailAddress(email) |> ignore
            true
        with 
        | _ -> false

    let validate contact =
        let errors = seq {
            if(String.IsNullOrEmpty(contact.Firstname)) then yield "First name should not be empty"
            if(String.IsNullOrEmpty(contact.Lastname)) then yield "Last name should not be empty"
            if(String.IsNullOrEmpty(contact.Email)) then yield "Email should not be empty"
            if(isValidEmail contact.Email |> not) then yield "Not a valid email"
        }

        if(Seq.isEmpty errors) then Ok contact else Error errors

    let create fname lname email =
        let c = { Id = Guid.NewGuid(); Firstname = fname; Lastname = lname; Email = email }
        validate c