using System;
using System.Text.Json;
using TgBotLite;
using TgBotLite.Extensions;

namespace TgBotLiteTests;

public class ExtensionsTests
{
    const string testJson = @"{
      ""update_id"": -1111,
      ""message"": {
        ""message_id"": 10,
        ""chat"": {
          ""id"": 1234,
          ""type"": ""private"",
          ""dec"": 1.23
        },
        ""isBoolean"": true
      }
    }";

    [Theory]
    [InlineData("update_id", typeof(int), -1111)]
    [InlineData("message.chat.id", typeof(long), 1234L)]
    [InlineData("message.chat.type", typeof(string), "private")]
    [InlineData("message.isBoolean", typeof(bool), true)]
    [InlineData("message.chat.dec", typeof(double), 1.23)]
    [InlineData("message", typeof(JsonElement), null)]
    public void Extensions_GetValues_Valid(string keys, Type type, object? expectedData)
    {
        var jsonElement = JsonSerializer.Deserialize<JsonElement>(testJson);
        var methodInfo = typeof(JsonElementExtensions)?.GetMethod("GetValue")?.MakeGenericMethod(type);
        var actualValue = methodInfo?.Invoke(null, new object[] { jsonElement, keys });
        if (expectedData is not null)
            Assert.Equal(expectedData, actualValue);
        else
            Assert.NotNull(actualValue);
    }
}

