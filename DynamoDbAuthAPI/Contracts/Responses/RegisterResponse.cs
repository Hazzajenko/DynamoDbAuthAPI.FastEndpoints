namespace DynamoDbAuthAPI.Contracts.Responses;

public class RegisterResponse
{
    public bool IsSuccess = false;
    public string? Log { get; set; }
}