using TgBotLite;

// change the bot token to your token, eg. 6287218385:AAEgvEUxEGde2-MHlBVAClAt1QMYEllQzh8
var bot = new TgBot("<put bot token here>");
bot.OnUpdateReceived += async (obj, e) =>
{
    await bot.SendAsync("sendMessage", new
    {
        chat_id = e.GetProperty("message").GetProperty("chat").GetProperty("id").GetInt64(),
        text = e.GetProperty("message").GetProperty("text").GetString()
    });
};
bot.StartLongPolling();

Console.ReadLine();