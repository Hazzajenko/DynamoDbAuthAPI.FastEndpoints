using DynamoDbAuthAPI.Contracts.Data;
using DynamoDbAuthAPI.Contracts.Requests;

namespace DynamoDbAuthAPI.Repositories;

public interface IAuthRepository
{
  //  Task<bool> LoginAsync(UserRequest user);
    Task<bool> RegisterAsync(UserDto user);
}