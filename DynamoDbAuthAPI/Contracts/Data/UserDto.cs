using Amazon.DynamoDBv2.DataModel;

namespace DynamoDbAuthAPI.Models;

[DynamoDBTable("users")]
public class UserDtoModel
{
    [DynamoDBProperty("emailAddress")]
    public string? EmailAddress { get; set; }
    [DynamoDBProperty("passwordHash")]
    public byte[]? PasswordHash { get; set; }
    [DynamoDBProperty("passwordSalt")]
    public byte[]? PasswordSalt { get; set; }
}