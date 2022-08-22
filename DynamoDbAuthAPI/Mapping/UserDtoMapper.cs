using System.Security.Cryptography;
using System.Text;
using DynamoDbAuthAPI.Contracts.Data;
using DynamoDbAuthAPI.Contracts.Requests;


namespace DynamoDbAuthAPI.Mapping;

public static class UserDtoMapper
{
    public static UserDto ToUserDto(this UserRequest user)
    {
        using var hmac = new HMACSHA512();
        return new UserDto
        {
            EmailAddress = user.EmailAddress.ToLower(),
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(user.Password)),
            PasswordSalt = hmac.Key
        };
    }
}