open System

// Функция для применения правил L-системы
let rec applyRules (input: string) : string =
    input.Replace("F", "F-F-F-F-F") // Правило: F -> F-F-F-F-F

// Функция для генерации L-системы на основе заданного количества итераций
let rec generateLSystem (axiom: string) (iterations: int) : string =
    if iterations = 0 then
        axiom
    else
        generateLSystem (applyRules axiom) (iterations - 1)

// Вывод результата L-системы для заданного количества итераций
let lSystemResult = generateLSystem "-FF+FF+FF-" 2 // Пример: 2 итерации
printfn "%s" lSystemResult

