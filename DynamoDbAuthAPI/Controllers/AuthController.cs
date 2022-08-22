/*using System.Security.Cryptography;
using System.Text;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using DynamoDbAuthAPI.Contracts.Data;
using DynamoDbAuthAPI.Contracts.Logs;
using DynamoDbAuthAPI.Contracts.Requests;
using DynamoDbAuthAPI.Contracts.Responses;
using DynamoDbAuthAPI.Helpers;
using DynamoDbAuthAPI.Repositories;
using DynamoDbAuthAPI.Services;
using Microsoft.AspNetCore.Mvc;
using ILogger = Serilog.ILogger;

namespace DynamoDbAuthAPI.Controllers;
[ApiController]
[Route("[controller]")]
public class AuthController : Controller
{
    private readonly ITokenService _tokenService;
    private readonly IAuthRepository _authRepository;
    private readonly IAuthService _authService;
    private readonly ILogger _logger;
    private readonly DynamoDBContext _dynamoDbContext;
    public AuthController(ITokenService tokenService, IAuthRepository authRepository, IAuthService authService, ILogger logger)
    {
        _tokenService = tokenService;
        _authRepository = authRepository;
        _authService = authService;
        _logger = logger;
        _dynamoDbContext = new DynamoDBContext(new AmazonDynamoDBClient());
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> LoginAsync(UserRequest request)
    {
        _logger.Information("POST LoginAsync User: {User}", request.EmailAddress);
        var user = await _dynamoDbContext.LoadAsync<UserDto>(request.EmailAddress);
        if (user == null)
        {
            return NotFound($"{request.EmailAddress} does not exist".ToError());
        };
        
        var doPasswordsMatch = _authService.VerifyPassword(user.PasswordSalt!, user.PasswordHash!, request.Password!);
        if (!doPasswordsMatch)
        {
            return BadRequest("Password is incorrect".ToError());
        }

      //  return Ok(_tokenService.ToLoginResponse(request));
        return Ok();
    }
    
    [HttpPost("register")]
    public async Task<ActionResult<RegisterResponse>> RegisterAsync(UserRequest request)
    {
        var user = await _dynamoDbContext.LoadAsync<UserDto>(request.EmailAddress);
        if (user != null)
        {
            return BadRequest($"{request.EmailAddress} is taken".ToError());
        };

        var register = await _authRepository.RegisterAsync(request);
        if (register)
        {
            return Ok($"{request.EmailAddress} is registered".ToSuccess());
        }

        return BadRequest("Unknown error".ToError());
    }
}*/