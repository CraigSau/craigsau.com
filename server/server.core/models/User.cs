using System.Collections.Generic;

namespace server.core.models;

public class User
{
    public long UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Alias { get; set; }

}