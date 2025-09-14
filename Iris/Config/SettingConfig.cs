using DotNetEnv;

namespace Iris.Config;

internal partial class IrisConfig
{
    public static ApiConfig Load()
    {
        Env.Load();

        string BotId = Environment.GetEnvironmentVariable("BOT_ID")!;
        string IrisToken = Environment.GetEnvironmentVariable("IRIS_TOKEN")!;

        if (BotId == null || IrisToken == null) throw new ArgumentException("Отсутсвуют параметры BOT_ID, IRIS_TOKEN в .env");

        return new ApiConfig
        {
            BotId = BotId,
            IrisToken = IrisToken
        };
    }
    internal class ApiConfig
    {
        public string BotId { get; set; } = string.Empty;
        public string IrisToken { get; set; } = string.Empty;
    }
}