open System.IO
open System.Threading.Tasks

// Функция для изменения порядка слов в строке
let reverseWords (line: string) =
    line.Split(' ')
    |> Array.rev
    |> String.concat " "

// Обработка одного файла: изменение порядка слов в каждой строке
let processFile (filePath: string) =
    let lines = File.ReadAllLines(filePath)
    let reversedLines = lines |> Array.map reverseWords
    reversedLines

// Основная функция, выполняющая параллельную обработку файлов
let processFiles (inputFiles: string list) (outputFile: string) =
    let resultLock = obj() // Для синхронизации записи в выходной файл
    Parallel.ForEach(inputFiles, fun file ->
        let reversedLines = processFile(file)
        lock resultLock (fun () ->
            File.AppendAllLines(outputFile, reversedLines)
        )
    )

// Пример использования
let inputFiles = ["file1.txt"; "file2.txt"; "file3.txt"] // Пути к входным файлам
let outputFile = "output.txt" // Путь к выходному файлу

processFiles inputFiles outputFile
