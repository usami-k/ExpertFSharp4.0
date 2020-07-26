// Generic Binary Serialization via the .NET Libraries

open System.IO
open System.Runtime.Serialization.Formatters.Binary

let writeValue outputStream x =
    let formatter = new BinaryFormatter()
    formatter.Serialize(outputStream, box x)

let readValue inputStream =
    let formatter = new BinaryFormatter()
    let res = formatter.Deserialize(inputStream)
    unbox res

let addresses =
    Map.ofList
        [ "Jeff", "123 Main Street, Redmond, WA 98052"
          "Fred", "987 Pine Road, Phila., PA 19116"
          "Mary", "PO Box 112233, Palo Alto, CA 94301" ]

let fsOut = new FileStream("Data.dat", FileMode.Create)
writeValue fsOut addresses
fsOut.Close()

let fsIn = new FileStream("Data.dat", FileMode.Open)
let res: Map<string, string> = readValue fsIn
fsIn.Close()
