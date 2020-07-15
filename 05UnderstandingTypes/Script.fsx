// Defining Discriminated Unions

type Route = int

type Make = string

type Model = string

type Transport =
    | Car of Make * Model
    | Bicycle
    | Bus of Route

let ian = Car("BMW", "360")

let don =
    [ Bicycle
      Bus 8 ]

let peter =
    [ Car("Ford", "Fiesta")
      Bicycle ]
