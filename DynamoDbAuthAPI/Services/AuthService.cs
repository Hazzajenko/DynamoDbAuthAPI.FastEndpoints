using System.Security.Cryptography;
using System.Text;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using DynamoDbAuthAPI.Contracts.Data;
using DynamoDbAuthAPI.Contracts.Requests;
using DynamoDbAuthAPI.Mapping;
using DynamoDbAuthAPI.Repositories;
using FluentValidation;
using FluentValidation.Results;

namespace DynamoDbAuthAPI.Services;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _authRepository;

    private readonly DynamoDBContext _dynamoDbContext;
    public AuthService(IAuthRepository authRepository)
    {
        _authRepository = authRepository;

        _dynamoDbContext = new DynamoDBContext(new AmazonDynamoDBClient());
    }
    
    public async Task<UserDto?> GetAsync(string emailAddress)
    {
        var userDto = await _dynamoDbContext.LoadAsync<UserDto>(emailAddress);
        return userDto;
    }
    
    public async Task<bool> RegisterAsync(UserRequest user)
    {
        var userDto = user.ToUserDto();
        return await _authRepository.RegisterAsync(userDto);
    }
    
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