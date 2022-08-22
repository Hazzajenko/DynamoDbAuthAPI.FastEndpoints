namespace DynamoDbAuthAPI.Contracts.Responses;

public class LoginResponse
{
    public string EmailAddress { get; set; } = default!;
    public string Token { get; set; } = default!;
}