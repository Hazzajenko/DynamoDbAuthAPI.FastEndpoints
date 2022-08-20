namespace DynamoDbAuthAPI.Contracts.Responses;

public class LoginResponse
{
    public string? EmailAddress { get; set; }
    public string? Token { get; set; }
}