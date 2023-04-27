using System;
using System.Text.Json;

namespace TgBotLite.Extensions;

public static class JsonElementExtensions
{
    /// <summary>
    /// Get the value directly as <typeparamref name="T"/> using <paramref name="keys"/> (dot as seperator - e.g. message.chat.id) without repeatedly use <see cref="JsonElement.GetProperty(string)"/>. 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="jsonElement"><see cref="JsonElement"/> object</param>
    /// <param name="keys">Properties joined by dot - e.g message.chat.id</param>
    /// <returns></returns>
    public static T? GetValue<T>(this JsonElement jsonElement, string keys)
    {
        JsonElement temp = jsonElement;
        var split = keys.Split(".").AsSpan();

        foreach (var key in split)
            temp = temp.GetProperty(key);

        return typeof(T) switch
        {
            Type t when t == typeof(string) => (T?)(object?)temp.GetString(),
            Type t when t == typeof(short) => (T)(object)temp.GetInt16(),
            Type t when t == typeof(int) => (T)(object)temp.GetInt32(),
            Type t when t == typeof(long) => (T)(object)temp.GetInt64(),
            Type t when t == typeof(bool) => (T)(object)temp.GetBoolean(),
            Type t when t == typeof(double) => (T)(object)temp.GetDouble(),
            Type t when t == typeof(JsonElement) => (T)(object)temp,
            _ => default
        };
    }
}
