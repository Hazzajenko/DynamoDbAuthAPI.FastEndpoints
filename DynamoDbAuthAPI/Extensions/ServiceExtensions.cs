using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using DynamoDbAuthAPI.Repositories;
using DynamoDbAuthAPI.Services;

namespace DynamoDbAuthAPI.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddServiceExtensions(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IAuthRepository, AuthRepository>();
        services.AddSingleton<IDynamoDBContext>(_ => new DynamoDBContext(new AmazonDynamoDBClient()));

        return services;
    }
}