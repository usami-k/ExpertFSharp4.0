// Combining Functional and Imperative : Functional Programming with Side Effects

// Consider Replacing Mutable Locals and Loops with Recursion

let factorizeImperative n =
    let mutable factor1 = 1
    let mutable factor2 = n
    let mutable i = 2
    let mutable fin = false
    while (i < n && not fin) do
        if (n % i = 0) then
            factor1 <- i
            factor2 <- n / i
            fin <- true
        i <- i + 1
    if (factor1 = 1) then None
    else Some(factor1, factor2)

let factorizeRecursive n =
    let rec find i =
        if i >= n then None
        elif (n % i = 0) then Some(i, n / i)
        else find (i + 1)
    find 2

// Separating Mutable Data Structures

open System.Collections.Generic

let divideIntoEquivalenceClasses keyf seq =
    let dict = new Dictionary<'key, ResizeArray<'T>>()
    seq
    |> Seq.iter (fun v ->
        let key = keyf v
        let ok, prev = dict.TryGetValue(key)
        if ok then prev.Add(v)
        else
            let prev = new ResizeArray<'T>()
            dict.[key] <- prev
            prev.Add(v))
    dict |> Seq.map (fun group -> group.Key, Seq.readonly group.Value)

divideIntoEquivalenceClasses (fun n -> n % 3) [ 0..10 ]
