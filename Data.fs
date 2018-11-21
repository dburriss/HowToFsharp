namespace Contacts

open System.Data.SQLite

module Data =

    let private conn (dbname:string) = 
        let c = new SQLiteConnection(sprintf "Data Source=%s.sqlite" dbname)
        c.Open()
        c

    let private dbname = "contactsDB"

    let all()  =
        let db = conn dbname
        Database.query db "SELECT id, firstname, lastname, email FROM contacts" None 

    let insert contact =
        let db = conn dbname
        let sql = "INSERT INTO contacts (id, firstname, lastname, email) VALUES (@Id, @Firstname, @Lastname, @Email);"
        Database.execute db sql contact   