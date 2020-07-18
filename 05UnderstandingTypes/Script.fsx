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

let averageSpeed (tr: Transport) =
    match tr with
    | Car _ -> 35
    | Bicycle -> 16
    | Bus _ -> 24

type Proposition =
    | True
    | And of Proposition * Proposition
    | Or of Proposition * Proposition
    | Not of Proposition

let rec eval (p: Proposition) =
    match p with
    | True -> true
    | And (p1, p2) -> eval p1 && eval p2
    | Or (p1, p2) -> eval p1 || eval p2
    | Not p1 -> not (eval p1)

type 'T option =
    | None
    | Some of 'T

type 'T list =
    | op_Nil
    | op_ColonColon of 'T * 'T list

type Tree<'T> =
    | Tree of 'T * Tree<'T> * Tree<'T>
    | Tip of 'T

let rec sizeOfTree tree =
    match tree with
    | Tree (_, l, r) -> 1 + sizeOfTree l + sizeOfTree r
    | Tip _ -> 1

let smallTree = Tree("1", Tree("2", Tip "a", Tip "b"), Tip "c")

// Using Discriminated Unions as Records

type Point3D = Vector3D of float * float * float

let origin = Vector3D(0., 0., 0.)
let unitX = Vector3D(1., 0., 0.)
let unitY = Vector3D(0., 1., 0.)
let unitZ = Vector3D(0., 0., 1.)

let length (Vector3D (dx, dy, dz)) = sqrt (dx * dx + dy * dy + dz * dz)
