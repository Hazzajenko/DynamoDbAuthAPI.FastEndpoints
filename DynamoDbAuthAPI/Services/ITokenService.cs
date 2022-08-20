using DynamoDbAuthAPI.Models;

namespace DynamoDbAuthAPI.Services;

public interface ITokenService
{
    string CreateToken(UserModel user);
}