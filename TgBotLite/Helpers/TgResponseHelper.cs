using System;
using System.Text.Json;
using TgBotLite.Models;

namespace TgBotLite.Helpers;

public static class TgResponseHelper
{
    public static JsonResult Result(JsonElement jsonElement)
        => jsonElement.GetProperty("result");

    public static class Update
    {
        public static long UpdateId(JsonResult jsonResult)
            => jsonResult.Value.GetProperty("update_id").GetInt64();
    }
}

