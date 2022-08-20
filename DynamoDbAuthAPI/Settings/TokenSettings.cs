namespace DynamoDbAuthAPI.Services;

public class TokenSettings
{
    public const string KeyName = "Token";

    public string Key { get; set; } = default!;
}