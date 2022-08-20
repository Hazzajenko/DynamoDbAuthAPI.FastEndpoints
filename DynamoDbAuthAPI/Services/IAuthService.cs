namespace DynamoDbAuthAPI.Services;

public interface IAuthService
{
    bool VerifyPassword(byte[] passwordSalt, byte[] passwordHash, string password);
}