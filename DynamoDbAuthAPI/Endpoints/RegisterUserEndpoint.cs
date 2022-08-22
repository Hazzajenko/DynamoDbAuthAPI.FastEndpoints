using DynamoDbAuthAPI.Contracts.Requests;
using DynamoDbAuthAPI.Contracts.Responses;
using DynamoDbAuthAPI.Mapping;
using DynamoDbAuthAPI.Services;
using FastEndpoints;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;

namespace DynamoDbAuthAPI.Endpoints;


//[FastEndpoints.HttpPost("register"), AllowAnonymous]
public class RegisterUserEndpoint : Endpoint<UserRequest, RegisterResponse>
{
    private readonly ITokenService _tokenService;
    private readonly IAuthService _authService;
    private readonly Serilog.ILogger _log;

    public override void Configure()
    {
        Post("/Auth/register");
        Description(b => b
            .Accepts<UserRequest>("application/json")
            .Produces<RegisterResponse>(200, "application/json+problem"));
        AllowAnonymous();
    }

    public RegisterUserEndpoint(ITokenService tokenService, IAuthService authService, Serilog.ILogger serilog)
    {
        _tokenService = tokenService;
        _authService = authService;
        _log = serilog;

    }
    
    public override async Task HandleAsync(UserRequest request, CancellationToken cancellationToken)
    {
        var user = await _authService.GetAsync(request.EmailAddress);
        if (user != null)
        {
            _log.Error("POST RegisterAsync Invalid Email: {User}", request.EmailAddress);
            AddError(r => r.EmailAddress, $"{request.EmailAddress} already exists");
        };

        var register = await _authService.RegisterAsync(request);
        if (!register)
        {
            _log.Error("POST RegisterAsync Upload Fail: {User}", request.EmailAddress);
           AddError(r => r.EmailAddress, $"{request.EmailAddress} was not registered");
        }
        ThrowIfAnyErrors();
        var registerResponse = request.ToRegisterResponse();
        await SendOkAsync(registerResponse, cancellationToken);
    }
}