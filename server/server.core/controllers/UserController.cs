using Microsoft.AspNetCore.Mvc;
using server.infrastructure.models;

namespace server.core.controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<User> _logger;

    public UserController(ILogger<User> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IEnumerable<User> TestUser()
    {
        return Enumerable.Range(1, 5).Select(i => new User
        {
            UserId = i,
            FirstName = "Craig",
            LastName = "Sauers",
        }).ToArray();
    }

}