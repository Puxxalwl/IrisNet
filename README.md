# IrisNet — асинхронный API клиент для взаимодействия с Iris-API

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
![.NET 9](https://img.shields.io/badge/.NET-9-512BD4.svg?logo=dotnet&logoColor=white)
---

## Установка на Nuget
```bash
dotnet add package IrisApi
```
---

## Как получить токен?

Для начала вам прийдется создать бота в: [BotFather](https://t.me/BotFather), далее в [Iris | Black Daimond](https://t.me/iris_cm) написать: `+ирис коннект` и следовать инструкции
--- 

## Начало работы

Создание клиента:

```csharp
using Iris;

class Program
{
    public async Task Main(string[] args)
    {
        var client = IrisClient.FromEnv(); // var client = new IrisClient("BOT-ID", "IRIS-TOKEN")

        // Следущее использование
    }
}
```
В .env должны быть поля: BOT_ID, IRIS_TOKEN
---

## GetBalanceAsync
Метод для получения баланса бота

```Csharp
var b = await client.GetBalanceAsync();
Console.WriteLine($"Ирисок: {b.Sweets},ирис-голд: {b.IrisGold}, очков-доната: {b.DonateScore}");
```
---

## GiveSweetsAsync
Метод для передачи ирисок другому пользователю

```csharp
var result = await client.GiveSweetsAsync(
    UserId: 6984952764, // Ид пользователя
    Sweets: 10, // Кол-во ирисок
    WithoutDonateScore: true, // Использование очков доната
    Comment: "За такую либу" // Комментарий
);
int Id = result.ResultId; // Id транзакции (Советуется сохранять)
```
---

## GiveIrisGoldAsync
Метод для передачи ирис-голд другому пользователю

```csharp
var result = await client.GiveIrisGoldAsync(
    UserId: 6984952764, // Ид пользователя
    Gold: 10, // Кол-во ирис-голд
    WithoutDonateScore: true, // Использование очков доната
    Comment: "За такую либу" // Комментарий
);
int Id = result.ResultId; // Id транзакции (Советуется сохранять)
```
---

## BagStatusAsync
Метод для управления просмотра мешка

```csharp
var result = await client.BagStatusAsync(
    status:true // Статус: открыть или закрыть
);
if (result.Ok == true) Console.WriteLine("Успех");
```
---

## AllowAllStatusAsync
Метод для управления переводами вам от всех

```csharp
var result = await client.AllowAllStatusAsync(
    status: true // статус: разрешить или запретить
);
if (result.Ok == true) Console.WriteLine("Успех");
```
---

## AllowUserStatusAsync
Метод для управления переводами вам от указанного пользователя

```csharp
var result = await client.AllowUserStatusAsync(
    status: true, // разрешить либо запретить
    UserId: 12345679 // Id пользователя
);
if (result.Ok == true) Console.WriteLine("Успех");
```
---

## GetSweetsHistoryAsync
Метод для просмотра истории операций с ирисками (offset + 1 будет аналогом long-polling)

```csharp
var result = await client.GetSweetsHistoryAsync();

if (result.Length == 0) return;

foreach (var r in result)
{
    Console.WriteLine(
        $"ID операции: {r.Id}" +
        $"Тип операции: {(r.Type == "give" ? "получение" : "отправка")}" +
        $"Пользователь: ${r.ToUserId}" +
        $"Дата: {r.Date}" // И так далее
    )
}
```
---

## GetGoldHistoryAsync
Метод для просмотра истории операций с ирисками (offset + 1 будет аналогом long-polling)

```csharp
var result = await client.GetGoldHistoryAsync();

if (result.Length == 0) return;

foreach (var r in result)
{
    Console.WriteLine(
        $"ID операции: {r.Id}" +
        $"Тип операции: {(r.Type == "give" ? "получение" : "отправка")}" +
        $"Пользователь: ${r.ToUserId}" +
        $"Дата: {r.Date}" // И так далее
    )
}
```
---

## GetLogAsync
Метод для получения логов ирисок либо ирис голд


```csharp
var result = await client.GetGoldHistoryAsync();

if (result.Length == 0) return;

foreach (var r in result)
{
    Console.WriteLine(
        $"ID операции: {r.Id}" +
        $"Тип логов: {(r.Type == "sweets_log" ? "ириски" : "ирис-голд")}" +
        $"Дата: {r.Date}" // И так далее
    )
}
```
---

## GetAgentsAsync
Метод для получения списка агентов

```csharp
var agents = await client.GetAgentsAsync();
Console.WriteLine($"Id агентов: {string.Join(",", agents)}")
```
---

## CheckAgentAsync
Метод для проверки является ли пользователь агентов

```csharp
var result = await client.CheckAgentAsync(
    UserId: 12345678
);
Console.WriteLine($"Пользователь {(result == true ? "агент" : "не агент")}");
```
---

### Существующие типы

## GiveResult
   — ResultId (int): Id транзакции

## Result
   — Ok (bool): Успешность выполнения

## Balance
   — Sweets (double): Ириски
   — IrisGold (double): Ирис-голд
   — DonateScore (int): Очки-доната

## Transaction
   — Id (int): Id транзакции
   — Type (string): Тип операции: send — отправка, receive — получение
   — Date (long): UNIX-time операции
   — Amount (int): Кол-во едениц валюты
   — NewBalance (int): Новый баланс
   — ToUserId (long): Пользователь
   — Detalis ([TransactionDetalis](#transactiondetalis)): Детали операции
   — Comment (string?): Коментарий к переводу
   — MetaData (object?): Пока что ничего
   
## TransactionDetalis
   — Total (int): Общая сумма перевода с учётом комиссии
   — Amount (int): Сколько едениц получил пользователь
   — DonateScore (double): Кол-во переданных очков доната
   — Fee (double): Комиссия перевода

## UpdateLog
   — Id (long): Id события
   — Type (string): Тип события: sweets_log — логи ирисок, gold_log — логи ирис-голд
   — Date (long): Время события
   — Obj ([Transaction](#transaction)): Объект события

**Author: @puxalwl**


