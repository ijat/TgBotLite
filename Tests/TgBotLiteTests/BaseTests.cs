using TgBotLite;

namespace TgBotLiteTests;

public class BaseTests
{
    private static readonly string? BotToken = Environment.GetEnvironmentVariable("BOT_TOKEN");

    [Fact]
    public void TgBotInitialize()
    {
        var bot = new TgBot(BotToken ?? "randomToken");
        Assert.NotNull(bot);
    }

    [Fact]
    public void TgBotInitializeNullToken()
    {
        var re = Record.Exception(() => new TgBot(null));
        Assert.IsType<ArgumentNullException>(re);
    }
}
