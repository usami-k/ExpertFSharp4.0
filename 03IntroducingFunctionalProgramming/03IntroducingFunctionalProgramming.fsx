// definition of http function from Chapter 2

open System.IO
open System.Net

let http (url : string) =
    let req = WebRequest.Create(url)
    let resp = req.GetResponse()
    let stream = resp.GetResponseStream()
    let reader = new StreamReader(stream)
    let html = reader.ReadToEnd()
    resp.Close()
    html

// Defining Recursive Functions

let rec factorial num =
    if num <= 1 then 1
    else num * factorial (num - 1)

let rec length xs =
    match xs with
    | [] -> 0
    | _ :: tail -> 1 + length tail

// Mutually recursive functions
let rec even num = (num = 0u) || odd (num - 1u)

and odd num = (num <> 0u) && even (num - 1u)

let rec even2 num =
    if num = 0u then true
    else odd2 (num - 1u)

and odd2 num =
    if num <> 0u then even2 (num - 1u)
    else false
//  if num = 0u then false
//  else even2 (num - 1u)

// Getting Started with Pattern Matching

let printFirst xs =
    match xs with
    | head :: _ -> printfn "The first item in the list is %A" head
    | [] -> printfn "No items in the list"

let people =
    [ ("Adam", None)
      ("Eve", None)
      ("Cain", Some("Adam", "Eve"))
      ("Abel", Some("Adam", "Eve")) ]

let showParents (name, parents) =
    match parents with
    | Some(dad, mum) -> printfn "%s has father %s and mother %s" name dad mum
    | None -> printfn "%s has no parents!" name

let urlFilter url agent =
    match url, agent with
    | "http://www.control.org", 86 -> true
    | "http://www.kaos.org", _ -> false
    | _ -> failwith "unexpected input"

let sign num =
    match num with
    | _ when num < 0 -> -1
    | _ when num > 0 -> 1
    | _ -> 0

// Introducing Function Values

let urls = [ "http://www.bing.com"; "http://www.google.com" ]

let fetch url = (url, http url)

let resultsOfFetch = List.map fetch urls

let lengthsOfFetch = List.map (fun (_, p) -> String.length p) resultsOfFetch
