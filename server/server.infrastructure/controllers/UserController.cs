using Microsoft.AspNetCore.Mvc;
using server.core.models;

namespace server.infrastructure.controllers;

[Route("[UserController]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly ILogger<User> _logger;

    public UserController(ILogger<User> logger)
    {
        _logger = logger;
    }

    [Route("[TestUser]")]
    [HttpGet(Name = "TestUser")]
    public IEnumerable<User> TestUser()
    {
        return Enumerable.Range(1, 5).Select(index => new User
        {
            UserId = index,
            FirstName = "Craig",
            LastName = "Sauers",

        })
        .ToArray();
    }
}