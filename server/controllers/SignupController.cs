using Microsoft.AspNetCore.Mvc;
using server.models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

namespace server.controllers;

[ApiController]
[Route("[controller]")]
public class SignupController : ControllerBase
{
    private readonly ILogger<User> _logger;

    private readonly IConfiguration _configuration;

    public SignupController(
            ILogger<User> logger,
            IConfiguration configuration
    )
    {
        _logger = logger;
        _configuration = configuration;
    }

    [HttpPost()]
    public string CreateUser(string firstName, string lastName, string email, string @alias)
    {
        int number = 23;
        try
        {
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = this._configuration.GetConnectionString("Local");
            conn.Open();

            MySqlCommand command = new MySqlCommand();
            command.Connection = conn;
            command.CommandText = 
                @"INSERT INTO Users 
                VALUES (
                    @Id,
                    @FirstName,
                    @LastName,
                    @Email,
                    @Alias
                );";

            command.Parameters.Add("@Id", MySqlDbType.VarChar).Value = Guid.NewGuid(); 
            command.Parameters.Add("@FirstName", MySqlDbType.VarChar).Value = firstName;
            command.Parameters.Add("@LastName", MySqlDbType.VarChar).Value = lastName;
            command.Parameters.Add("@Email", MySqlDbType.VarChar).Value = email;
            command.Parameters.Add("@Alias", MySqlDbType.VarChar).Value = @alias;
            //command.Parameters.AddWithValue("?FirstName", firstName); 
            //command.Parameters.AddWithValue("?LastName", lastName); 
            //command.Parameters.AddWithValue("?Email", email); 
            //command.Parameters.AddWithValue("?Alias", @alias); 

            number = command.ExecuteNonQuery();
            
            conn.Close();
        }
        catch (MySql.Data.MySqlClient.MySqlException ex)
        {
            _logger.LogError($"{ex}");
            number = 58008;
        }

        return number;
    }
}
