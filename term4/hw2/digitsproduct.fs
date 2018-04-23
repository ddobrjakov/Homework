// Task 2.1 - Digits Product
let rec digitsProduct n =
    let d = n / 10
    if d = 0
        then abs n
    else (n % 10) * digitsProduct d
