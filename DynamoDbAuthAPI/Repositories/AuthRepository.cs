using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using DynamoDbAuthAPI.Contracts.Data;
using DynamoDbAuthAPI.Contracts.Requests;
using DynamoDbAuthAPI.Contracts.Responses;
using DynamoDbAuthAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DynamoDbAuthAPI.Repositories;

public class AuthRepository : IAuthRepository
{
   // public IDynamoDBContext DynamoDbContext { get; }
  //  private readonly IAmazonDynamoDB _dynamoDb;
  private readonly IDynamoDBContext _dynamoDbContext;

  private readonly IOptions<DatabaseSettings> _databaseSettings;

  private readonly ITokenService _tokenService;
  //    private readonly DynamoDBContext _dynamoDbContext;

    public AuthRepository(IDynamoDBContext dynamoDbContext,
        IOptions<DatabaseSettings> databaseSettings,
        ITokenService tokenService)
    {
        //DynamoDbContext = dynamoDbContext;
       // _dynamoDbContext = new DynamoDBContext(new AmazonDynamoDBClient());
       // _dynamoDb = dynamoDb;
       _dynamoDbContext = dynamoDbContext;
       _databaseSettings = databaseSettings;
       _tokenService = tokenService;
    }
    
    /*public async Task<bool> LoginAsync(UserRequest user)
    {
        
    }*/
    
    public async Task<bool> RegisterAsync(UserRequest user)
    {
        using var hmac = new HMACSHA512();

        var createUserDto = new UserDto
        {
            EmailAddress = user.EmailAddress!.ToLower(),
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(user.Password!)),
            PasswordSalt = hmac.Key
        };
        
        try
        {
            await _dynamoDbContext.SaveAsync(createUserDto);
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}