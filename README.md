# TgBotLite
A super simple dotnet api wrapper for Telegram Bot API. 
I try to make this library as simple as possible (KISS), without any hard linked classes or weird way to handle things, which gets broken when Telegram Bot API update something.
This library need you to open https://core.telegram.org/bots/api all the time. Good way to learn the API isn't it? 😏

# Usage

Long polling example
```
  var bot = new TgBot("bot token");
  
  # Start long polling
  bot.OnUpdateReceived += Bot_OnUpdateReceived;
  bot.StartLongPolling();
  # You have to do something here to wait, while loop or anything because startlongpolling is not blocking
  
  // ...
  
  private void Bot_OnUpdateReceived(object? sender, System.Text.Json.JsonElement e)
  {
    // tg updates by getUpdates method in the api
    // use JsonElement to get properties
    // e.g
    // jsonElement.GetProperty("result").GetProperty("update_id").GetInt32();
  }
```

Send bot request
```
  // see which method you want in https://core.telegram.org/bots/api
  // example you want to sendMessage
  JsonElement response = await bot.SendAsync("sendMessage", new {
    chat_id = 12312323,
    text = "hewlo"
  });
  
  // Do anything to response object, you may want to check TgResponseHelper
```
