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

    /// <summary>
    /// Initialize TgBot with bot token
    /// </summary>
    /// <param name="botToken"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public TgBot(BotToken? botToken)
    {
        if (botToken is null)
            throw new ArgumentNullException(nameof(botToken));

        _botToken = botToken;
    }

    /// <summary>
    /// Start long polling process at the background
    /// </summary>
    /// <param name="cancellationToken"></param>
    public void StartLongPolling(CancellationToken? cancellationToken = null)
        => Task.Run(() => LongPolling(cancellationToken ?? new()));

    /// <summary>
    /// Send get method to Tg
    /// </summary>
    /// <param name="method"></param>
    /// <returns></returns>
    public async Task<JsonElement> SendAsync(string method)
        => await TgHttp.GetAsync(_botToken, method);

    /// <summary>
    /// Send post method to Tg
    /// </summary>
    /// <param name="method"></param>
    /// <param name="body"></param>
    /// <returns></returns>
    public async Task<JsonElement> SendAsync(string method, object body)
        => await TgHttp.PostAsync(_botToken, method, body);

    private void LongPollingNewUpdate(JsonElement e)
        => OnUpdateReceived?.Invoke(this, e);
    
    private async Task LongPolling(CancellationToken cancellationToken)
    {
        var longOffset = 0L;
        while (true)
        {
            if (cancellationToken.IsCancellationRequested)
                break;

            var response = await TgHttp.PostAsync(_botToken, "getUpdates", new
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