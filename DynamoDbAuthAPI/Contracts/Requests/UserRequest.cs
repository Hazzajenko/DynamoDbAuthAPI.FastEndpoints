using System.ComponentModel;

namespace DynamoDbAuthAPI.Contracts.Requests;

public class UserRequest
{
    [DefaultValue("string@mail.com")]
    public string EmailAddress { get; set; } = default!;

    public string Password { get; set; } = default!;
}