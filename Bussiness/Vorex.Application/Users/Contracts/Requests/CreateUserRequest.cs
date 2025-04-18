namespace Vorex.Application.Users.Contracts.Requests;

public class CreateUserRequest
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public string? ProfileImage { get; set; }
}
