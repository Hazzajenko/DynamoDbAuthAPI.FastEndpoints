using DynamoDbAuthAPI.Contracts.Requests;
using DynamoDbAuthAPI.Contracts.Responses;

namespace DynamoDbAuthAPI.Services;

public interface ITokenService
{
    LoginResponse ToLoginResponse(UserRequest request);
    string CreateToken(UserRequest user);
}