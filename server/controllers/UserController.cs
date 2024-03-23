using Microsoft.AspNetCore.Mvc;
using server.models;
using MySql.Data.MySqlClient;
using server;

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
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = this._configuration.GetConnectionString("Local");
            conn.Open();

            MySqlCommand command = new MySqlCommand();
            command.Connection = conn;
            command.CommandText = @"SELECT * FROM Users;";
            //command.Parameters.AddWithValue("@clientId", clientId); --Example

            using MySqlDataReader reader = command.ExecuteReader();
            {
                while (reader.Read())
                {
                    User user = new User()
                    {
                        UserId = reader.GetGuid("id"),
                        FirstName = reader.GetString("first_name"),
                        LastName = reader.GetString("last_name"),
                        Email = reader.GetString("email"),
                        Alias = reader.GetString("alias")
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
}
