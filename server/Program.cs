using server.models;
using server.stores;
using server.dataaccess;
using server.services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using DotNetEnv;

public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();

        builder.Services.AddSwaggerGen();

        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
                //.AllowCredentials();
            });

            options.AddPolicy("AllowSpecificOrigins", policy =>
            {
                policy.WithOrigins("http://localhost", "http://localhost:80", "http://craigsau.dev", "https://craigsau.dev")
                .AllowAnyMethod()
                .AllowAnyHeader();
            });
        });

        builder.Services.AddIdentity<User, Role>().AddUserStore<UserStore>().AddDefaultTokenProviders();

        builder.Services.Configure<IdentityOptions>(options =>
        {
            // Password settings
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 8;
            options.Password.RequiredUniqueChars = 1;


            // Lockout settings
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;

            // User settings
            options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._";
            options.User.RequireUniqueEmail = true;

        });

        builder.Services.ConfigureApplicationCookie(options =>
        {
            options.Cookie.HttpOnly = true;
            options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

            options.LoginPath = "/Account/Login";
            options.AccessDeniedPath = "/Account/AccessDenied";
            options.SlidingExpiration = true;
        });

        builder.Services.AddTransient<IUserStore<User>, UserStore>();
        builder.Services.AddTransient<IRoleStore<Role>, RoleStore>();

        builder.Services.AddTransient<AccountDataService>();

        builder.Services.AddTransient<IEmailService, EmailService>();
        builder.Services.AddTransient<IEmailSender, EmailService>();

        Env.Load();
        Console.WriteLine($"SMTP_HOST: {Environment.GetEnvironmentVariable("SMTP_HOST")}");
        Console.WriteLine($"SMTP_HOST ATTEMPT2: {Env.GetString("SMTP_HOST")}");

        builder.Configuration.AddEnvironmentVariables();


        WebApplication app = builder.Build();

        // Configure the HTTP request pipeline.
        // if (app.Environment.IsDevelopment())
        // {
        app.UseSwagger();
        app.UseSwaggerUI();
        // }

        app.UseHttpsRedirection();

        app.UseRouting();
        //app.UseRateLimiter();

        app.UseCors("AllowSpecificOrigins");

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}


