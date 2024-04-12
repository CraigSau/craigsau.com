using Microsoft.AspNetCore.Mvc;
using server.models;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Cors;

namespace server.controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<User> _logger;

    private readonly IConfiguration _configuration;

    public UserController(ILogger<User> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;

    }

    [HttpGet]
    public IEnumerable<User> GetAllUsers()
    {
        List<User> users = new List<User>();

        try
        {
            DatabaseExtensions dbExten = new DatabaseExtensions(_configuration);
            MySqlConnection conn = dbExten.ConnectDB();

            MySqlCommand command = new MySqlCommand();
            command.Connection = conn;
            command.CommandText = @"SELECT * FROM Users;";

            using MySqlDataReader reader = command.ExecuteReader();
            {
                while (reader.Read())
                {
                    User user = new User()
                    {
                        Id = reader.GetString("id"),
                        UserName = reader.GetString("user_name"),
                        FirstName = reader.GetString("first_name"),
                        LastName = reader.GetString("last_name"),
                        Email = reader.GetString("email"),
                        PasswordHash = reader.GetString("passwordhash"),
                    };

                    users.Add(user);
                }
            }
            conn.Close();
        }
        catch (MySql.Data.MySqlClient.MySqlException ex)
        {
            _logger.LogError($"{ex}");
        }

        return users;
    }

    [HttpGet]
    [EnableCors()]
    [Route("getUser")]
    public User GetUserByEmail()
    {
        return new User();
    }
}
