//Task 1.1 - Factorial
let rec factorial n =
    if n < 2 then 1 else n * factorial(n - 1)