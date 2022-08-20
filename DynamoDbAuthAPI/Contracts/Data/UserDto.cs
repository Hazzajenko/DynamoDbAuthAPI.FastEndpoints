using Amazon.DynamoDBv2.DataModel;

namespace DynamoDbAuthAPI.Contracts.Data;

[DynamoDBTable("users")]
public class UserDto
{
    [DynamoDBProperty("emailAddress")]
    public string? EmailAddress { get; set; }
    [DynamoDBProperty("passwordHash")]
    public byte[]? PasswordHash { get; set; }
    [DynamoDBProperty("passwordSalt")]
    public byte[]? PasswordSalt { get; set; }
}