// Defining Recursive Functions

let rec factorial num =
    if num <= 1 then 1
    else num * factorial (num - 1)

let rec length xs =
    match xs with
    | [] -> 0
    | _ :: tail -> 1 + length tail
