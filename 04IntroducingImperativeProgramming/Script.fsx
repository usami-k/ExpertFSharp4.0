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
