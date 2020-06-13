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

// Imperative Looping and Iterating

for i = 1 to 10 do
    printf "%d " i
printfn "Done!"

for (b, pj) in [ ("Banana 1", false)
                 ("Banana 2", true) ] do
    if pj then printfn "%s is in pyjamas today!" b

open System.Text.RegularExpressions

for m in Regex.Matches("All the Pretty Horses", "[a-zA-Z]+") do
    printfn "res = %s" m.Value

// Using Mutable Records

type DiscreteEventCounter =
    { mutable Total : int
      mutable Positive : int
      Name : string }

let newCounter nm =
    { Total = 0
      Positive = 0
      Name = nm }

let longPageCounter = newCounter "long page(s)"

let reportStatus (s : DiscreteEventCounter) = printfn "We have %d %s out of %d" s.Positive s.Name s.Total

let recordEvent (s : DiscreteEventCounter) isPositive =
    s.Total <- s.Total + 1
    if isPositive then s.Positive <- s.Positive + 1

let fetch url =
    let page = http url
    recordEvent longPageCounter (page.Length > 10000)
    page

// Avoiding Aliasing

type Cell =
    { mutable data : int }

let cell1 = { data = 3 }
let cell2 = cell1

cell1.data <- 7
cell2 // val it : Cell = { data = 7 }

// Using Mutable let Bindings

let sum m n =
    let mutable res = 0
    for i = m to n do
        res <- res + i
    res
