namespace Iris.Enums;

public enum BotPermissionOptions
{
    Reg,
    Activity,
    Stars,
    Spam,
    Bag
}

internal static class BotPermissionExtensions
{
    public static string GetValue(this BotPermissionOptions options)
    {
        return options switch
        {
            BotPermissionOptions.Spam => "spam",
            BotPermissionOptions.Reg => "reg",
            BotPermissionOptions.Activity => "activity",
            BotPermissionOptions.Stars => "stars",
            BotPermissionOptions.Bag => "pocket",
            _ => throw new ArgumentOutOfRangeException(nameof(options), options, null)
        };
        
    }
}