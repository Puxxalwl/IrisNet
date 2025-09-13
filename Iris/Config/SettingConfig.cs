using DotNetEnv;

namespace Iris.Config;

public partial class IrisConfig
{
    public static ApiConfig Load()
    {
        Env.Load();

        return new ApiConfig
        {
            BotId = Environment.GetEnvironmentVariable("BOT_ID")!,
            IrisToken = Environment.GetEnvironmentVariable("IRIS_TOKEN")!
        };
    }
    public class ApiConfig
    {
        public string BotId { get; set; } = string.Empty;
        public string IrisToken { get; set; } = string.Empty;
    }
}