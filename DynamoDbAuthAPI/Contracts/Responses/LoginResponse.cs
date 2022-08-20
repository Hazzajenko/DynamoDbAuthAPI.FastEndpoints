namespace DynamoDbAuthAPI.Models;

public class LoginResponseModel
{
    public string? EmailAddress { get; set; }
    public string? Token { get; set; }
    public bool IsSuccess = false;
    public string? Log { get; set; }
}