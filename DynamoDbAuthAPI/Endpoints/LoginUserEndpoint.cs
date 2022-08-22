using DynamoDbAuthAPI.Contracts.Requests;
using DynamoDbAuthAPI.Contracts.Responses;
using DynamoDbAuthAPI.Mapping;
using DynamoDbAuthAPI.Services;
using FastEndpoints;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;

namespace DynamoDbAuthAPI.Endpoints;


//[FastEndpoints.HttpPost("login"), AllowAnonymous]
public class LoginUserEndpoint : Endpoint<UserRequest, LoginResponse>
{
    private readonly ITokenService _tokenService;
    private readonly IAuthService _authService;
    private readonly Serilog.ILogger _log;

    public override void Configure()
    {
        Post("/Auth/login");
        Description(b => b
            .Accepts<UserRequest>("application/json")
            .Produces<LoginResponse>(200, "application/json+problem"));
        AllowAnonymous();
    }

    public LoginUserEndpoint(ITokenService tokenService, IAuthService authService, Serilog.ILogger serilog)
    {
        _tokenService = tokenService;
        _authService = authService;
        _log = serilog;

    }
    
    public override async Task HandleAsync(UserRequest request, CancellationToken cancellationToken)
    {
        var user = await _authService.GetAsync(request.EmailAddress);
        
        if (user is null)
        {
            _log.Error("POST LoginAsync Invalid Email: {User}", request.EmailAddress);
            ThrowError(r => r.EmailAddress, $"{request.EmailAddress} does not exist");
        }

        var doPasswordsMatch = _authService.VerifyPassword(user.PasswordSalt, user.PasswordHash, request.Password);
        if (!doPasswordsMatch)
        {
            _log.Error("POST LoginAsync Invalid Password: {User}", request.EmailAddress);
            ThrowError(r => r.Password, "Password is incorrect");
        }

        var loginResponse = _tokenService.ToLoginResponse(request);
        await SendOkAsync(loginResponse, cancellationToken);
    }
}