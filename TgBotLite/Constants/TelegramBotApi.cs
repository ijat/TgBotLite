using System;
using TgBotLite.Models;

namespace TgBotLite.Constants;

public static class TelegramBotApi
{
    public const string TgBotEndpoint = "https://api.telegram.org/bot";
    public static string Bot(BotToken token) => $"{TgBotEndpoint}{token}";
}

