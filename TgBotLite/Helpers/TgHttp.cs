using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TgBotLite.Models;

namespace TgBotLite.Helpers;

public static class TgHttp
{
    private static HttpClient _httpClient = new()
    {
        Timeout = TimeSpan.FromSeconds(60),
    };

    private static JsonSerializerOptions _jsonOptions = new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
    };

    public static async Task<JsonElement> Get(BotToken token, string method)
    {
        var response = await _httpClient.GetAsync($"{Constants.TelegramBotApi.Bot(token)}/{method}");
        var jsonString = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<JsonElement>(jsonString);
    }

    public static async Task<JsonElement> Post(BotToken token, string method, object body)
    {
        var jsonBody = JsonSerializer.Serialize(body, _jsonOptions);
        var response = await _httpClient.PostAsync($"{Constants.TelegramBotApi.Bot(token)}/{method}", new StringContent(jsonBody, Encoding.UTF8, "application/json"));
        var jsonString = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<JsonElement>(jsonString);
    }
}

