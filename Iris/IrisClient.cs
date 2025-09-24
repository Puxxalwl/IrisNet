using Iris.Config;
using Iris.Constants;
using Iris.Enums;
using Iris.Helpers;
using Iris.Types;

namespace Iris;

public class IrisClient
{
    private readonly HttpClient _httpClient = new HttpClient();
    private readonly string _baseIrisUrl;
    private readonly string BotId;
    public static readonly string IrisLink = "https://t.me/iris_black_bot";

    public IrisClient(string botId, string IrisToken)
    {
        _baseIrisUrl = $"https://iris-tg.ru/api/v{ApiConstants._versionIris}/{BotId}:{IrisToken}";
        BotId = botId;
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
            if (!string.IsNullOrEmpty(Comment) && Comment.Length > 128) throw new ArgumentException("Длина комментария не может перевышать 128 символов");

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

    public async Task<GiveResult> GiveDonateScoreAsync(
        long UserId,
        double DonateScore,
        string Comment = ""
    )
    {
        try
        {
            if (DonateScore <= 0) throw new InvalidOperationException("Число ирисок не можеть быть меньше 1");
            if (!string.IsNullOrEmpty(Comment) && Comment.Length > 128) throw new ArgumentException("Длина комментария не может перевышать 128 символов");

            string url = $"{_baseIrisUrl}/{ApiConstants.DonateScoreGive}";

            var Params = new Dictionary<string, object>
            {
                ["sweets"] = DonateScore,
                ["user_id"] = UserId,
                ["comment"] = Comment,
            };

            return await _httpClient.GetWithRetry<GiveResult>(url, Params);
        }
        catch (Exception err)
        {
            Logger.Error($"Ошибка перевода очков доната (ID: {UserId})", err);
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
    public async Task<UpdateLog[]> GetLogAsync(int Limit = 0)
    {
        try
        {
            var Params = new Dictionary<string, object>
            {
                ["limit"] = Limit
            };
            string url = $"{_baseIrisUrl}/{ApiConstants.GetUpdates}";
            return await _httpClient.GetWithRetry<UpdateLog[]>(url, Params);
        }
        catch (Exception err)
        {
            Logger.Error("Ошибка получения логов", err);
            throw;
        }
    }

    public async Task<Transaction[]> GetGoldHistoryAsync(long Offset = 0, int Limit = 0)
    {
        try
        {
            string url = $"{_baseIrisUrl}/{ApiConstants.GoldHistory}";
            var Params = new Dictionary<string, object>
            {
                ["offset"] = Offset,
                ["limit"] = Limit
            };
            return await _httpClient.GetWithRetry<Transaction[]>(url, Params);
        }
        catch (Exception err)
        {
            Logger.Error("Ошибка получения истории ирис-голд", err);
            throw;
        }
    }

    public async Task<Transaction[]> GetSweetsHistoryAsync(long Offset = 0, int Limit = 0)
    {
        try
        {
            string url = $"{_baseIrisUrl}/{ApiConstants.SweetsHistory}";
            var Params = new Dictionary<string, object>
            {
                ["offset"] = Offset,
                ["limit"] = Limit
            };
            return await _httpClient.GetWithRetry<Transaction[]>(url, Params);
        }
        catch (Exception err)
        {
            Logger.Error("Ошибка получения истории ирисок", err);
            throw;
        }
    }

    public async Task<Transaction[]> GetDonateScoreHistoryAsync(long Offset = 0, int Limit = 0)
    {
        try
        {
            string url = $"{_baseIrisUrl}/{ApiConstants.DonateScoreHistory}";
            var Params = new Dictionary<string, object>
            {
                ["offset"] = Offset,
                ["limit"] = Limit
            };
            return await _httpClient.GetWithRetry<Transaction[]>(url, Params);
        }
        catch (Exception err)
        {
            Logger.Error("Ошибка получения истории очков доната", err);
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

    public TradeClass Trade { get; } = new TradeClass();

    public class TradeClass
    {
        private static readonly string TradeDealsUrl = "https://iris-tg.ru/trade/deals";
        private static readonly string OrderBookUrl = "https://iris-tg.ru/k/trade/order_book";
        private static readonly HttpClient _httpClient = new HttpClient();
        public async Task<List<TradesDeals>> GetDealsAsync(long? Id)
        {
            var Params = Id != null ? new Dictionary<string, object>
            {
                ["id"] = Id
            } : null;
            return await _httpClient.GetWithRetry<List<TradesDeals>>(TradeDealsUrl, Params);
        }

        public async Task<Trades> GetOrderBookAsync()
        {
            return await _httpClient.GetWithRetry<Trades>(OrderBookUrl);
        }
    }

    public string GeneratePermissionsDeepLink(IEnumerable<BotPermissionOptions> options, long? botIdForLink)
    {
        var botId = botIdForLink ?? long.Parse(BotId);
        var link = $"{IrisLink}?start=request_rights_{botId}";

        foreach (var opt in options)
        {
            link += $"_{opt}";
        }
        return link;
    }


    public string GenerateDeepLink(CurrenciesOptions currency, int count, string? comment, long? botIdForLink)
    {
        if (count <= 0) { throw new InvalidOperationException("Число не может быть меньше 1"); }
        if (!string.IsNullOrEmpty(comment) && comment.Length > 128) { throw new ArgumentException("Длина комментария не может перевышать 128 символов"); }

        long botId = botIdForLink ?? long.Parse(BotId);


        string link = currency switch
        {
            CurrenciesOptions.Gold => $"{IrisLink}?start=givegold_bot{botId}_{count}",
            CurrenciesOptions.Sweets => $"{IrisLink}?start=give_bot{botId}_{count}",
            CurrenciesOptions.DonateScore => $"{IrisLink}?start=givedonate_score_bot{botId}_{count}",
            _ => throw new ArgumentOutOfRangeException(nameof(currency), currency, null)
        };
        if (string.IsNullOrEmpty(comment)) link += $"_{comment}";
        return link;
    }

    public async Task<ResultReg> GetUserRegAsync(long UserId)
    {
        try
        {
            string url = $"{_baseIrisUrl}/{ApiConstants.UserReg}";
            var Params = new Dictionary<string, object>
            {
                ["user_id"] = UserId
            };

            return await _httpClient.GetWithRetry<ResultReg>(url, Params);
        }
        catch (Exception err)
        {
            Logger.Error("Не удалось получить регистрации пользователя", err);
            throw;
        }
    }

    public async Task<ResultSpam> GetUserSpamAsync(long UserId)
    {
        try
        {
            string url = $"{_baseIrisUrl}/{ApiConstants.UserSpam}";
            var Params = new Dictionary<string, object>
            {
                ["user_id"] = UserId
            };
            return await _httpClient.GetWithRetry<ResultSpam>(url, Params);
        }
        catch (Exception err)
        {
            Logger.Error("Не удалось получить информацию о наказаниях пользователя", err);
            throw;
        }
    }

    public async Task<ResultActivity> GetUserActivityAsync(long UserId)
    {
        try
        {
            string url = $"{_baseIrisUrl}/{ApiConstants.UserActivity}";
            var Params = new Dictionary<string, object>
            {
                ["user_id"] = UserId
            };
            return await _httpClient.GetWithRetry<ResultActivity>(url, Params);
        }
        catch (Exception err)
        {
            Logger.Error("Не удалось получить информацию о активности пользователя", err);
            throw;
        }
    }

    public async Task<ResultStars> GetUserStarsAsync(long UserId)
    {
        try
        {
            string url = $"{_baseIrisUrl}/{ApiConstants.UserStars}";
            var Params = new Dictionary<string, object>
            {
                ["user_id"] = UserId
            };
            return await _httpClient.GetWithRetry<ResultStars>(url, Params);
        }
        catch (Exception err)
        {
            Logger.Error("Не удалось получить информацию о рейтинге пользователя", err);
            throw;
        }
    }

    public async Task<ResultBag> GetUserBagAsync(long UserId)
    {
        try
        {
            string url = $"{_baseIrisUrl}/{ApiConstants.UserBag}";
            var Params = new Dictionary<string, object>
            {
                ["user_id"] = UserId
            };
            return await _httpClient.GetWithRetry<ResultBag>(url, Params);
        }
        catch (Exception err)
        {
            Logger.Error("Не удалось получить информацию о мешке пользователя", err);
            throw;
        }
    }
}