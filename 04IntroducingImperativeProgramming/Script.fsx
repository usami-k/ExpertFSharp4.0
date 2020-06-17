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

// Hiding Mutable Data

let generateStamp =
    let mutable count = 0
    (fun () ->
    count <- count + 1
    count)

// Working with Arrays

let arr0 = [| 1.0; 1.0; 1.0 |]

arr0.[1]
arr0.[1] <- 3.0
arr0 // val it : float [] = [|1.0; 3.0; 1.0|]

// Generating and Slicing Arrays

let arr1 =
    [| for i in 0..5 -> (i, i * i) |]

arr1.[1..3]
arr1.[..2]
arr1.[3..]

// Two-Dimensional Arrays

let arr2 =
    [| [| 1; 2; 3 |]
       [| 4; 5; 6 |] |]

arr2.[1].[0]
arr2.[1].[0] <- 7
arr2 // val it : int [] [] = [|[|1; 2; 3|]; [|7; 5; 6|]|]

// Introducing the Imperative .NET Collections
// Using Resizable Arrays

type ResizableArray<'T> = System.Collections.Generic.List<'T>

let names = new ResizableArray<string>()

for name in [ "Claire"; "Sophie"; "Jane" ] do
    names.Add(name)

names.Count
names.[0]

let squares =
    new ResizableArray<int>(seq {
                                for i in 0..100 -> i * i
                            })

squares.Count
squares.[100]

// Using Dictionaries

open System.Collections.Generic

let capitals = new Dictionary<string, string>(HashIdentity.Structural)

capitals.["USA"] <- "Washington"
capitals.["Bangladesh"] <- "Dhaka"
capitals.ContainsKey("USA")
capitals.ContainsKey("Australia")
capitals.Keys
capitals.["USA"]
for kvp in capitals do
    printfn "%s has capital %s" kvp.Key kvp.Value

let lookupName nm (dict : Dictionary<string, string>) =
    let mutable res = ""
    let foundIt = dict.TryGetValue(nm, &res)
    if foundIt then res
    else failwithf "Didn't find %s" nm

capitals |> lookupName "USA"
capitals |> lookupName "Australia"

let sparseMap = new Dictionary<int * int, float>()

sparseMap.[(0, 2)] <- 4.0
sparseMap.[(1021, 1847)] <- 9.0
sparseMap.Keys

// Having an Effect : Basic I/O

open System.IO

let tmpFile = Path.Combine(__SOURCE_DIRECTORY__, "temp.txt")

File.WriteAllLines(tmpFile, [| "This is a test file."; "It is easy to read" |])
seq {
    for line in File.ReadLines tmpFile do
        let words = line.Split [| ' ' |]
        if words.Length > 3 && words.[2] = "easy" then yield line
}

// .NET I/O via Streams

let playlistFile = Path.Combine(__SOURCE_DIRECTORY__, "playlist.txt")
let outp = File.CreateText playlistFile

outp.WriteLine "Enchanted"
outp.WriteLine "Put your records on"
outp.Close()

let inp = File.OpenText playlistFile

inp.ReadLine()
inp.ReadLine()
inp.Close()

// Using System.Console

System.Console.WriteLine "Hello World"
System.Console.ReadLine()
