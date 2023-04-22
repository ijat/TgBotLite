using System;
using System.Text.Json;

namespace TgBotLite.Models;

public class JsonResult
{
    public JsonElement Value { get; set; }

    public JsonResult(JsonElement jsonElement)
    {
        Value = jsonElement;
    }

    public static implicit operator JsonResult(JsonElement jsonElement) => new(jsonElement);
    public static implicit operator JsonElement(JsonResult jsonResult) => jsonResult.Value;
}

