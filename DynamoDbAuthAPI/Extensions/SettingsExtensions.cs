using DynamoDbAuthAPI.Services;

namespace DynamoDbAuthAPI.Extensions;

public static class SettingsExtensions
{
    public static IServiceCollection AddSettingsExtensions(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<DatabaseSettings>(config.GetSection(DatabaseSettings.KeyName));
        services.Configure<TokenSettings>(config.GetSection(TokenSettings.KeyName));

        return services;
    }
}