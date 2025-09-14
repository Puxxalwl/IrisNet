using System.Text.Json.Serialization;

namespace Iris.Types;

public partial class Transaction
{
    // Id транзакции
    [JsonPropertyName("id")]
    public long Id { get; set; }

    // Тип операции: send — отправка, receive — получение
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    // UNIX-time операции
    [JsonPropertyName("date")]
    public long Date { get; set; }

    // кол-во едениц валюты
    [JsonPropertyName("amount")]
    public int Amount { get; set; }

    // Новый баланс
    [JsonPropertyName("balance")]
    public int NewBalance { get; set; }

    // Пользователь
    [JsonPropertyName("peer_id")]
    public long ToUserId { get; set; }


    [JsonPropertyName("to_user_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    private long ToUser { get; set; }

    // Детали перевода
    [JsonPropertyName("detalis")]
    public TransactionDetalis Detalis { get; set; } = null!;

    // Коментарий к переводу
    [JsonPropertyName("comment")]
    public string? Comment { get; set; }

    // Что-то.
    [JsonPropertyName("metadata")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object? MetaData { get; set; }
}

public class TransactionDetalis
{
    // общая сумма перевода с учётом комиссии
    [JsonPropertyName("total")]
    public int Total { get; set; }

    // Сколько едениц получил пользователь
    [JsonPropertyName("amount")]
    public int Amount { get; set; }

    // Кол-во переданных очков доната
    [JsonPropertyName("dotane_score")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public double DonateScore { get; set; }

    // комиссия перевода
    [JsonPropertyName("fee")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public double Fee { get; set; }

}