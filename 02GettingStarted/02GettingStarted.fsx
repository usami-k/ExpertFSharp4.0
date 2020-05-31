let splitAtSpaces (text : string) = text.Split ' ' |> Array.toList

let wordCount text =
    let words = splitAtSpaces text
    let numWords = words.Length
    let distinctWords = List.distinct words
    let numDups = numWords - distinctWords.Length
    (numWords, numDups)

let showWordCount text =
    let numWords, numDups = wordCount text
    printfn "--> %d words in the text" numWords
    printfn "--> %d duplicate words" numDups

// Using the .NET networking libraries from F#

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

// Using packages

#r "packages/System.Net.Http/lib/net46/System.Net.Http.dll"

open System.Net.Http

let client = new HttpClient()
let fetchContentAsync (url : string) =
    async { let! res = client.GetAsync(url) |> Async.AwaitTask
            let! content = res.Content.ReadAsStringAsync() |> Async.AwaitTask
            return content }

// To execute : fetchContentAsync ("http://example.com/") |> Async.RunSynchronously

// Accessing External Data Using F# Packages

#r "packages/FSharp.Data/lib/net45/FSharp.Data.dll"

open FSharp.Data

type Species = HtmlProvider<"https://en.wikipedia.org/wiki/The_world's_100_most_threatened_species">

let species =
    [ for x in Species.GetSample().Tables.``Species list``.Rows -> x.Type, x.``Common name`` ]

let speciesSorted =
    species
    |> List.countBy fst
    |> List.sortByDescending snd

// Starting a Web Server and Serving Data using F# Packages

#r "packages/Suave/lib/net461/Suave.dll"

open Suave

let html =
    [ yield "<html><body><ul>"
      for (category, count) in speciesSorted do
          yield sprintf "<li>Category <b>%s</b>: <b>%d</b></li>" category count
      yield "</ul></body></html>" ]
    |> String.concat "\n"

startWebServer defaultConfig (Successful.OK html)
