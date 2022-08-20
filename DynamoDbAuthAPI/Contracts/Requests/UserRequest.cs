namespace DynamoDbAuthAPI.Contracts.Requests;

public class UserRequest
{
    public string? EmailAddress { get; set; }

    public string? Password { get; set; }
}