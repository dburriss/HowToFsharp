namespace Contacts

open Contacts
open System
open System.Data.SQLite

module Data =

    type ContactEntity = { id:string; firstname:string; lastname:string; email:string }

    let private conn (dbname:string) = 
        let c = new SQLiteConnection(sprintf "Data Source=%s.sqlite" dbname)
        c.Open()
        c

    let private dbname = "contactsDB"

    let all() =
        let db = conn dbname
        Database.query db "SELECT id, firstname, lastname, email FROM contacts" None 
        |> Result.map
            (fun ss -> ss 
                    |> Seq.map (fun c -> {
                        Id = Guid.Parse(c.id); Firstname = c.firstname; Lastname = c.lastname; Email = c.email
                        }))

    let insert c =
        let db = conn dbname
        let entity = { id = c.Id.ToString(); firstname = c.Firstname; lastname = c.Lastname; email = c.Email }
        let sql = "INSERT INTO contacts (id, firstname, lastname, email) VALUES (@id, @firstname, @lastname, @email);"
        Database.execute db sql entity   