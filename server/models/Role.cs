using Microsoft.AspNetCore.Identity;

namespace server.models;

public class Role : IdentityRole<Guid>
{
    public string Description { get; set; } = string.Empty;
}
