namespace Contacts

module Database =

    open Dapper
    open System.Data.Common
    open System.Collections.Generic

    // DbConnection -> string -> 'b -> Result<int,exn>
    let execute (connection:#DbConnection) (sql:string) (parameters:_) =
        try
            let result = connection.Execute(sql, parameters)
            Ok result
        with
        | ex -> Error ex

    // DbConnection -> string -> IDictionary<string,obj> -> Result<seq<'T>,exn>
    let query (connection:#DbConnection) (sql:string) (parameters:IDictionary<string, obj> option) : Result<seq<'T>,exn> =
        try
            let result =
                match parameters with
                | Some p -> connection.Query<'T>(sql, p)
                | None -> connection.Query<'T>(sql)
            Ok result
        with
        | ex -> Error ex

    // DbConnection -> string -> IDictionary<string,obj> -> Result<'T,exn>
    let querySingle (connection:#DbConnection) (sql:string) (parameters:IDictionary<string, obj> option) =
        try
            let result =
                match parameters with
                | Some p -> connection.QuerySingleOrDefault<'T>(sql, p)
                | None -> connection.QuerySingleOrDefault<'T>(sql)
            
            if isNull (box result) then Ok None
            else Ok (Some result)

        with
        | ex -> Error ex