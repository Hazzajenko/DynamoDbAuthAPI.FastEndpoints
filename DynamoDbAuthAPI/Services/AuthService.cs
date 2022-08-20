using System.Security.Cryptography;
using System.Text;
using DynamoDbAuthAPI.Contracts.Responses;

namespace DynamoDbAuthAPI.Services;

public class AuthService : IAuthService
{
    public bool VerifyPassword(byte[] passwordSalt, byte[] passwordHash, string password)
    {
        using var hmac = new HMACSHA512(passwordSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

        for (int i = 0; i < computedHash.Length; i++)
        {
            if (computedHash[i] != passwordHash[i])
            {
                return false;
            };
        }

        return true;
    }
    

}