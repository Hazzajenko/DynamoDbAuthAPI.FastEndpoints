

using System.Security.Cryptography;
using System.Text;
using DynamoDbAuthAPI.Contracts.Data;
using DynamoDbAuthAPI.Contracts.Requests;
using DynamoDbAuthAPI.Contracts.Responses;

namespace DynamoDbAuthAPI.Mapping;

public static class RegisterResponseMapper
{
    public static RegisterResponse ToRegisterResponse(this UserRequest user)
    {
        return new RegisterResponse
        {
            EmailAddress = user.EmailAddress.ToLower()
        };
    }
}