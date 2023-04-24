using System;
namespace TgBotLite.Models;

public class BotToken
{
	private string Value { get; set; }

    public BotToken(string botToken)
    {
        Value = botToken;
    }

    public override string ToString() => Value;
    public static implicit operator BotToken(string token) => new(token);
    public static implicit operator string(BotToken token) => token.Value;
}
