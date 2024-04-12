using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

using server.models;


namespace server.controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<User> _logger;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public AccountController(
            IConfiguration configuration,
            ILogger<User> logger,
            UserManager<User> userManager,
            SignInManager<User> signInManager
    )
    {
        _configuration = configuration;
        _logger = logger;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpPost()]
    [EnableCors()]
    [Route("Register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromForm] User newUser)
    {
        if (ModelState.IsValid && newUser != null)
        {
            var result = await _userManager.CreateAsync(newUser, newUser.PasswordHash!);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(newUser, isPersistent: false);
                _logger.LogInformation(3, "User create a new account with password.");
            }
            else
            {
                return new ContentResult
                {
                    Content = $"<div>{result}</div>",
                    ContentType = "text/html",
                    StatusCode = 500,
                };
            }
        }

        return Ok($"Successfully registered user! \n Welcome {newUser.FirstName}");
    }

    [HttpPost()]
    [EnableCors()]
    [Route("Login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromForm] User user)
    {
        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(user.UserName!, user.PasswordHash!, false, false);
            if (result.Succeeded)
            {
                _logger.LogInformation($"{user.UserName} has successfully signed in!");
            }
            else
            {
                return new ContentResult
                {
                    Content = $"<div>{result} : Are you sure you have an account?</div>",
                    ContentType = "text/html",
                    StatusCode = 500,
                };
            }
        }

        // Having to do this is really dumb... has to be something I'm doing wrong to not have this data here already.
        var userFirstName = _userManager.FindByNameAsync(user.UserName).Result;
        return Ok($"Logged in as {user.UserName},\n Welcome {userFirstName.FirstName}");
    }
}
