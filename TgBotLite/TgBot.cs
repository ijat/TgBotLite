using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using TgBotLite.Helpers;
using TgBotLite.Models;

namespace TgBotLite;

public class TgBot
{
    private const long LongPollingTimeout = 300;
    private const int LongPollingLimit = 100;

    private readonly BotToken _botToken;

    public event EventHandler<JsonElement>? OnUpdateReceived;

    public TgBot(BotToken botToken)
    {
        _botToken = botToken;
    }

    public void StartLongPolling(CancellationToken? cancellationToken = null)
    {
        Task.Run(() => LongPolling(cancellationToken ?? new()));
    }

    private void LongPollingNewUpdate(JsonElement e)
        => OnUpdateReceived?.Invoke(this, e);
    
    private async Task LongPolling(CancellationToken cancellationToken)
    {
        var longOffset = 0L;
        while (true)
        {
            if (cancellationToken.IsCancellationRequested)
                break;

            var response = await TgHttp.Post(_botToken, "getUpdates", new
            {
                offset = longOffset,
                limit = LongPollingLimit,
                timeout = LongPollingTimeout
            });

            var result = TgResponseHelper.Result(response);

            foreach (var e in result.Value.EnumerateArray())
            {
                var er = TgResponseHelper.Update.UpdateId(e);
                if (er > longOffset) longOffset = er;
                LongPollingNewUpdate(e);
            }

            ++longOffset;
        }
    }
}