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
