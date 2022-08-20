using DynamoDbAuthAPI.Contracts.Logs;

namespace DynamoDbAuthAPI.Helpers;

public static class Logs
{
    public static SuccessLog ToSuccess(this string log)
    {
        return new SuccessLog
        {
            Success = log
        };
    }
    
    public static ErrorLog ToError(this string log)
    {
        return new ErrorLog
        {
            Error = log
        };
    }
}