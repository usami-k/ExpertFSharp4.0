// Disable Structural Generic Hashing and Comparison

open System
open NonStructuralComparison

compare 4 1
compare DateTime.Now (DateTime.Now.AddDays(1.0))
// Error
//compare (1, 3) (5, 4)
