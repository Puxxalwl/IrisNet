using Iris.Config;
using Iris.Constants;
using Iris.Helpers;
using Iris.Types;

namespace Iris;

public class IrisClient
{
    private readonly HttpClient _httpClient = new HttpClient();
    private readonly string _baseIrisUrl;

    public IrisClient(string BotId, string IrisToken)
    {
        _baseIrisUrl = $"https://iris-tg.ru/api/v{ApiConstants._versionIris}/{BotId}:{IrisToken}";
    }

    public static IrisClient FromEnv()
    {
        var config = IrisConfig.Load();
        return new IrisClient(config.BotId, config.IrisToken);
    }

    public async Task<Balance> GetBalanceAsync()
    {
        try
        {
            string url = $"{_baseIrisUrl}/{ApiConstants.Balance}";
            return await _httpClient.GetWithRetry<Balance>(url);
        }
        catch (Exception err)
        {
            Logger.Error("Не удалось получить баланс", err);
            throw;
        }
    }

    public async Task<GiveResult> GiveSweetsAsync(
        long UserId,
        double Sweets,
        bool WithoutDonateScore = true,
        string Comment = ""
    )
    {
        try
        {
            if (Sweets <= 0) throw new InvalidOperationException("Число ирисок не можеть быть меньше 1");
            if (Comment != "" && Comment.Length > 128) throw new ArgumentException("Длина комментария не может перевышать 128 символов");

            string url = $"{_baseIrisUrl}/{ApiConstants.GiveSweets}";

            var Params = new Dictionary<string, object>
            {
                ["sweets"] = Sweets,
                ["user_id"] = UserId,
                ["comment"] = Comment,
                ["without_donate_score"] = WithoutDonateScore
            };

            return await _httpClient.GetWithRetry<GiveResult>(url, Params);
        }
        catch (Exception err)
        {
            Logger.Error($"Ошибка перевода ирисок (ID: {UserId})", err);
            throw;
        }
    }

    public async Task<GiveResult> GiveIrisGoldAsync(
        long UserId,
        double IrisGold,
        bool WithoutDonateScore = true,
        string Comment = ""
    )
    {
        try
        {
            if (IrisGold <= 0) throw new InvalidOperationException("Число ирис-голд не можеть быть меньше 1");
            if (Comment != "" && Comment.Length > 128) throw new ArgumentException("Длина комментария не может перевышать 128 символов");
           
            string url = $"{_baseIrisUrl}/{ApiConstants.GiveGold}";

            var Params = new Dictionary<string, object>
            {
                ["gold"] = IrisGold,
                ["user_id"] = UserId,
                ["comment"] = Comment,
                ["without_donate_score"] = WithoutDonateScore
            };

            return await _httpClient.GetWithRetry<GiveResult>(url, Params);
        }
        catch (Exception err)
        {
            Logger.Error($"Ошибка перевода ирис-голд (ID: {UserId})", err);
            throw;
        }
    }
    public async Task<Result> BagStatusAsync(bool status)
    {
        try
        {
            string url = $"{_baseIrisUrl}/{(status == true ? ApiConstants.Enable : ApiConstants.Disable)}";
            return await _httpClient.GetWithRetry<Result>(url);
        }
        catch (Exception err)
        {
            Logger.Error("Ошибка изменения статуса мешка", err);
            throw;
        }
    }

    public async Task<Result> AllowAllStatusAsync(bool status)
    {
        try
        {
            string url = $"{_baseIrisUrl}/{(status == true ? ApiConstants.AllAllow : ApiConstants.AllDeny)}";
            return await _httpClient.GetWithRetry<Result>(url);
        }
        catch (Exception err)
        {
            Logger.Error("Ошибка изменения статуса переводов", err);
            throw;
        }
    }

    public async Task<Result> AllowUserStatusAsync(bool status, long UserId)
    {
        try
        {
            string url = $"{_baseIrisUrl}/{(status == true ? ApiConstants.AllowUser : ApiConstants.DenyUser)}";
            var Params = new Dictionary<string, object>
            {
                ["user_id"] = UserId
            };
            return await _httpClient.GetWithRetry<Result>(url, Params);
        }
        catch (Exception err)
        {
            Logger.Error($"Ошибка изменения статуса переводов для {UserId}", err);
            throw;
        }
    }
    public async Task<UpdateLog[]> GetLogAsync()
    {
        try
        {
            string url = $"{_baseIrisUrl}/{ApiConstants.GetUpdates}";
            return await _httpClient.GetWithRetry<UpdateLog[]>(url);
        }
        catch (Exception err)
        {
            Logger.Error("Ошибка получения логов", err);
            throw;
        }
    }

    public async Task<Transaction[]> GetGoldHistoryAsync(long Offset = 0)
    {
        try
        {
            string url = $"{_baseIrisUrl}/{ApiConstants.GoldHistory}";
            var Params = new Dictionary<string, object>
            {
                ["offset"] = Offset
            };
            return await _httpClient.GetWithRetry<Transaction[]>(url, Params);
        }
        catch (Exception err)
        {
            Logger.Error("Ошибка получения истории ирис-голд", err);
            throw;
        }
    }

    public async Task<Transaction[]> GetSweetsHistoryAsync(long Offset = 0)
    {
        try
        {
            string url = $"{_baseIrisUrl}/{ApiConstants.SweetsHistory}";
            var Params = new Dictionary<string, object>
            {
                ["offset"] = Offset
            };
            return await _httpClient.GetWithRetry<Transaction[]>(url, Params);
        }
        catch (Exception err)
        {
            Logger.Error("Ошибка получения истории ирис-голд", err);
            throw;
        }
    }
    public async Task<long[]> GetAgentsAsync()
    {
        try
        {
            string url = $"https://iris-tg.ru/api/v{ApiConstants._versionIris}/{ApiConstants.IrisAgents}";
            return await _httpClient.GetWithRetry<long[]>(url);
        }
        catch (Exception err)
        {
            Logger.Error("Ошибка получения списка агентов", err);
            throw;
        }
    }

    public async Task<bool> CheckAgentAsync(long UserId)
    {
        long[] Agents = await GetAgentsAsync();
        return Agents.Contains(UserId);
    }

}