//  Создать иерархию классов и интерфейсов для предметной области 
//  (класса 3 – 4, из них хотя бы один класс должен являться наследником другого класса), 
//  заданной номером варианта, и продемонстрировать её работу

// Определение интерфейса банковских операций
type IBankOperation =
    abstract member PerformOperation: unit -> unit

// Базовый класс "Человек"
type Person(name: string, age: int) =
    member this.Name = name
    member this.Age = age
    override this.ToString() = sprintf "%s, возраст: %d" name age

// Класс "Клиент", наследуемый от "Человек"
type Client(name: string, age: int, clientId: string) =
    inherit Person(name, age)
    member val ClientId = clientId with get
    override this.ToString() = sprintf "Клиент: %s, ID: %s" (base.ToString()) clientId

// Класс "Банковский счет"
type BankAccount(accountNumber: string, balance: decimal) =
    member val AccountNumber = accountNumber with get
    member val Balance = balance with get, set
    member this.Deposit(amount: decimal) =
        this.Balance <- this.Balance + amount
    member this.Withdraw(amount: decimal) =
        if this.Balance >= amount then
            this.Balance <- this.Balance - amount
        else
            printfn "Недостаточно средств на счете"
    override this.ToString() = sprintf "Счет %s, Баланс: %M" accountNumber balance

// Класс "Депозит", реализующий интерфейс IBankOperation
type Deposit(account: BankAccount, amount: decimal) =
    interface IBankOperation with
        member this.PerformOperation() =
            account.Deposit(amount)
            printfn "Депозит на сумму %M выполнен. Новый баланс: %M" amount account.Balance

// Класс "Снятие", реализующий интерфейс IBankOperation
type Withdrawal(account: BankAccount, amount: decimal) =
    interface IBankOperation with
        member this.PerformOperation() =
            account.Withdraw(amount)
            printfn "Снятие на сумму %M выполнено. Новый баланс: %M" amount account.Balance

// Класс "Менеджер", наследуемый от "Человек"
type Manager(name: string, age: int, employeeId: string) =
    inherit Person(name, age)
    member val EmployeeId = employeeId with get
    member this.PerformOperation(operation: IBankOperation) =
        operation.PerformOperation()
    override this.ToString() = sprintf "Менеджер: %s, ID: %s" (base.ToString()) employeeId

// Пример использования
let client = Client("Алексей Иванов", 34, "CL12345")
let account = BankAccount("ACC123", 1000M)
let manager = Manager("Мария Петрова", 29, "EMP987")

printfn "%s" (client.ToString())
printfn "%s" (account.ToString())
manager.PerformOperation(Deposit(account, 500M))
manager.PerformOperation(Withdrawal(account, 200M))
