using DynamoDbAuthAPI.Contracts.Data;
using DynamoDbAuthAPI.Contracts.Requests;


namespace DynamoDbAuthAPI.Services;

public interface IAuthService
{
    Task<UserDto?> GetAsync(string emailAddress);
    bool VerifyPassword(byte[] passwordSalt, byte[] passwordHash, string password);
    Task<bool> RegisterAsync(UserRequest user);

}