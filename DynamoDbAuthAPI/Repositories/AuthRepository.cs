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
    private readonly IDynamoDBContext _dynamoDbContext;
  private readonly IOptions<DatabaseSettings> _databaseSettings;
  private readonly ITokenService _tokenService;

  public AuthRepository(IDynamoDBContext dynamoDbContext,
        IOptions<DatabaseSettings> databaseSettings,
        ITokenService tokenService)
    {
        _dynamoDbContext = dynamoDbContext;
       _databaseSettings = databaseSettings;
       _tokenService = tokenService;
    }
    
    
    public async Task<bool> RegisterAsync(UserDto user)
    {
        await _dynamoDbContext.SaveAsync(user);
        return true;
    }
}