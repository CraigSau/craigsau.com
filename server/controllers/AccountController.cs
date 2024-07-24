using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Identity.UI.Services;

using server.models;
using server.services;


namespace server.controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly IConfiguration configuration;
    private readonly ILogger<User> logger;
    private readonly UserManager<User> userManager;
    private readonly SignInManager<User> signInManager;
    private readonly IEmailService emailService;

    public AccountController(
            IConfiguration Configuration,
            ILogger<User> Logger,
            UserManager<User> UserManager,
            SignInManager<User> SignInManager,
            IEmailService EmailService
    )
    {
        configuration = Configuration;
        logger = Logger;
        userManager = UserManager;
        signInManager = SignInManager;
        emailService = EmailService;
    }

    [HttpPost()]
    [Route("Register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromForm] User newUser)
    {
        logger.LogInformation("Attempting to register new user!");
        if (ModelState.IsValid && newUser != null)
        {
            IdentityResult result = await userManager.CreateAsync(newUser, newUser.PasswordHash!);
            if (result.Succeeded)
            {
                await signInManager.SignInAsync(newUser, isPersistent: false);
                logger.LogInformation(3, "User create a new account with password.");
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

        return Ok($"<pre>Successfully egistered user! \n Welcome {newUser!.UserName}</pre>");
    }

    [HttpPost()]
    [Route("Login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromForm] User user)
    {
        if (ModelState.IsValid)
        {
            var result = await signInManager.PasswordSignInAsync(user.UserName!, user.PasswordHash!, false, false);
            if (result.Succeeded)
            {
                logger.LogInformation($"{user.UserName} has successfully signed in!");
            }
            else
            {
                return new ContentResult
                {
                    Content = $"<div>Username or password is incorrect</div>",
                    ContentType = "text/html",
                    StatusCode = 500,
                };
            }
        }

        Console.WriteLine("Attempting to send email from EmailService!");
        await emailService.SendEmailAsync("kaleethieme@gmail.com", "We are so back", "Got emails working from my website, as you can see :)");

        // Having to do this is really dumb... has to be something I'm doing wrong to not have this data here already.
        var userFirstName = userManager.FindByNameAsync(user.UserName!).Result;
        return Ok($"<pre>Successfully logged in.\n Welcome {user.UserName}!</pre>");
    }
}
