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
    if num = 0u then false
    else even2 (num - 1u)
