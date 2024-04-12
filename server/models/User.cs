using Microsoft.AspNetCore.Identity;

namespace server.models;

public class User : IdentityUser
{
    public override string Id { get; set; } = Guid.NewGuid().ToString();
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    [ProtectedPersonalData]
    public override string? Email { get; set; } = string.Empty;
    public override bool EmailConfirmed { get; set; } = false;
    [ProtectedPersonalData]
    public override string? PasswordHash { get; set; } = string.Empty;
    public DateOnly CreatedOn { get; set; } = DateOnly.FromDateTime(DateTime.Now);
}
