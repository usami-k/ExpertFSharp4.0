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

// Computing with Collection Functions

let delimiters = [| ' '; '\n'; '\t'; '<'; '>'; '=' |]

let getWords (s : string) = s.Split delimiters

let getStats site =
    let url = "http://" + site
    let html = http url
    let hwords = html |> getWords

    let hrefs =
        html
        |> getWords
        |> Array.filter (fun s -> s = "href")
    (site, html.Length, hwords.Length, hrefs.Length)

let sites = [ "www.bing.com"; "www.google.com" ]

let stats = sites |> List.map getStats

// Pipelining with |>

let (|>) x f = f x

[ 1; 2; 3 ] |> List.map (fun x -> x * x)

List.map (fun x -> x * x) [ 1; 2; 3 ]

// Composing Functions with >>

let (>>) f g x = g (f (x))

let composedFunc = List.map (fun x -> x * x) >> List.map (fun x -> x + 1)

[ 1; 2; 3 ] |> composedFunc

[ 1; 2; 3 ]
|> List.map (fun x -> x * x)
|> List.map (fun x -> x + 1)

let google = http "http://www.google.com"

let counterLinks =
    getWords
    >> Array.filter (fun s -> s = "href")
    >> Array.length

google |> counterLinks

// Building Functions with Partial Application

let shift (dx, dy) (x, y) = (x + dx, y + dy)

let shiftRight = shift (1, 0)
let shiftUp = shift (0, 1)
let shiftLeft = shift (-1, 0)
let shiftDown = shift (0, -1)

[ (0, 0)
  (1, 0)
  (1, 1)
  (0, 1) ]
|> List.map shiftRight

// Using Local Functions

open System.Drawing

let remap (r1 : RectangleF) (r2 : RectangleF) =
    let scalex = r2.Width / r1.Width
    let scaley = r2.Height / r1.Height
    let mapx x = r2.Left + (x - r1.Left) * scalex
    let mapy y = r2.Top + (y - r1.Top) * scaley
    let mapp (p : PointF) = PointF(mapx p.X, mapy p.Y)
    mapp

let rect1 = RectangleF(100.0f, 100.0f, 100.0f, 100.0f)
let rect2 = RectangleF(50.0f, 50.0f, 200.0f, 200.0f)

let mapp = remap rect1 rect2

let remapped1 = mapp (PointF(100.0f, 100.0f))
let remapped2 = mapp (PointF(150.0f, 50.0f))
let remapped3 = mapp (PointF(200.0f, 150.0f))

// Iterating with Functions

[ "http://www.bing.com"; "http://www.google.com" ]
|> List.iter (fun site -> printfn "%s, length %d" site (http site).Length)
