
using Amazon.CloudWatchLogs;
using DynamoDbAuthAPI.Contracts.Responses;
using DynamoDbAuthAPI.Extensions;
using DynamoDbAuthAPI.Validation;
using FastEndpoints;
using FastEndpoints.Swagger;
using Serilog;
using Serilog.Sinks.AwsCloudWatch;
using ILogger = Serilog.ILogger;

// Copyright (c) 2022 Harry Jenkins
// This code is licensed under MIT license

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFastEndpoints();
builder.Services.AddSwaggerDoc(settings =>
{
    settings.DocumentName = "v1.0";
    settings.Title = "DynamoDb Auth Minimal API";
    settings.Version = "v1.0";
});

ILogger serilog = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.AmazonCloudWatch(
        logGroup: $"{builder.Environment.EnvironmentName}/{builder.Environment.ApplicationName}",
        logStreamPrefix: DateTime.UtcNow.ToString("yyyyMMddHHmmssfff"),
        cloudWatchClient: new AmazonCloudWatchLogsClient()
    )
    .CreateLogger();
builder.Services.AddSingleton(serilog);
builder.Services.AddAuthenticationServices(builder.Configuration);
builder.Services.AddServiceExtensions(builder.Configuration);
builder.Services.AddSettingsExtensions(builder.Configuration);


builder.Services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);

var app = builder.Build();

app.UseMiddleware<ValidationExceptionMiddleware>();
app.UseFastEndpoints(x =>
{
    x.ErrorResponseBuilder = (failures, _) =>
    {
        return new ValidationFailureResponse
        {
            Errors = failures.Select(y => y.ErrorCode).ToList(),
            
        };
    };
});

app.UseOpenApi();
app.UseSwaggerUi3(s => s.ConfigureDefaults());

app.UseAuthentication();
app.UseAuthorization();

app.Run();