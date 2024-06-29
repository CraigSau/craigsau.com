using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Identity;
using server.models;

namespace server.dataaccess;

public class AccountDataService
{
    private readonly IConfiguration _configuration;

    public AccountDataService(
        IConfiguration configuration
    )
    {
        _configuration = configuration;
    }

    public async Task<IdentityResult> CreateAsync(User user)
    {
        DatabaseExtensions dbExten = new DatabaseExtensions(_configuration);
        MySqlConnection conn = dbExten.ConnectDB();

        MySqlCommand command = new MySqlCommand();
        command.Connection = conn;
        command.CommandText =
            @"INSERT INTO Users 
                VALUES (
                    @Id,
                    @UserName,
                    @FirstName,
                    @LastName,
                    @Email,
                    @EmailConfirmed,
                    @PasswordHash,
                    @CreatedOn
                );";

        command.Parameters.Add("@Id", MySqlDbType.VarChar).Value = user.Id;
        command.Parameters.Add("@UserName", MySqlDbType.VarChar).Value = user.UserName;
        command.Parameters.Add("@FirstName", MySqlDbType.VarChar).Value = user.FirstName;
        command.Parameters.Add("@LastName", MySqlDbType.VarChar).Value = user.LastName;
        command.Parameters.Add("@Email", MySqlDbType.VarChar).Value = user.Email;
        command.Parameters.Add("@EmailConfirmed", MySqlDbType.Bit).Value = user.EmailConfirmed;
        command.Parameters.Add("@PasswordHash", MySqlDbType.VarChar).Value = user.PasswordHash;
        command.Parameters.Add("@CreatedOn", MySqlDbType.Date).Value = user.CreatedOn;

        int results = command.ExecuteNonQuery();
        conn.Close();

        if (results > 0) return IdentityResult.Success;

        return IdentityResult.Failed(new IdentityError
        {
            Description = $"Could not insert user {user.Email}."
        });
    }

    public async Task<IdentityResult> DeleteAsync(User user)
    {
        DatabaseExtensions dbExten = new DatabaseExtensions(_configuration);
        MySqlConnection conn = dbExten.ConnectDB();

        MySqlCommand command = new MySqlCommand();
        command.Connection = conn;
        command.CommandText =
            @"DELETE FROM Users WHERE Id = @Id";

        command.Parameters.Add("@Id", MySqlDbType.VarChar).Value = user.Id;

        int results = command.ExecuteNonQuery();
        conn.Close();

        if (results > 0) return IdentityResult.Success;

        return IdentityResult.Failed(new IdentityError
        {
            Description = $"Could not delete user {user.Email}."
        });

    }

    public async Task<User?> FindByIdAsync(string id)
    {
        DatabaseExtensions dbExten = new DatabaseExtensions(_configuration);
        MySqlConnection conn = dbExten.ConnectDB();

        MySqlCommand command = new MySqlCommand();
        command.Connection = conn;
        command.CommandText =
            @"SELECT * FROM Users WHERE Id = @Id";

        command.Parameters.Add("@Id", MySqlDbType.VarChar).Value = id;

        using MySqlDataReader reader = command.ExecuteReader();

        User? resultUser = null;

        while (reader.Read())
        {
            resultUser = new User()
            {
                Id = reader.GetString("id"),
                UserName = reader.GetString("user_name"),
                FirstName = reader.GetString("first_name"),
                LastName = reader.GetString("last_name"),
                Email = reader.GetString("email"),
                EmailConfirmed = reader.GetBoolean("email_confirmed"),
                PasswordHash = reader.GetString("passwordhash")
            };

        }
        conn.Close();

        return resultUser;
    }

    public async Task<User?> FindByNameAsync(string userName)
    {
        DatabaseExtensions dbExten = new DatabaseExtensions(_configuration);
        MySqlConnection conn = dbExten.ConnectDB();

        MySqlCommand command = new MySqlCommand();
        command.Connection = conn;
        command.CommandText =
            @"SELECT * FROM Users WHERE user_name = @UserName";

        command.Parameters.Add("@UserName", MySqlDbType.VarChar).Value = userName;

        using MySqlDataReader reader = command.ExecuteReader();

        User? resultUser = null;

        while (reader.Read())
        {
            resultUser = new User()
            {
                Id = reader.GetString("id"),
                UserName = reader.GetString("user_name"),
                FirstName = reader.GetString("first_name"),
                LastName = reader.GetString("last_name"),
                Email = reader.GetString("email"),
                EmailConfirmed = reader.GetBoolean("email_confirmed"),
                PasswordHash = reader.GetString("passwordhash")
            };

        }
        conn.Close();

        return resultUser;
    }

    public async Task<User?> FindByEmailAsync(string email)
    {
        DatabaseExtensions dbExten = new DatabaseExtensions(_configuration);
        MySqlConnection conn = dbExten.ConnectDB();

        MySqlCommand command = new MySqlCommand();
        command.Connection = conn;
        command.CommandText =
            @"SELECT * FROM Users WHERE email = @Email";

        command.Parameters.Add("@Email", MySqlDbType.VarChar).Value = email;

        using MySqlDataReader reader = command.ExecuteReader();

        User? resultUser = null;

        while (reader.Read())
        {
            resultUser = new User()
            {
                Id = reader.GetString("id"),
                UserName = reader.GetString("user_name"),
                FirstName = reader.GetString("first_name"),
                LastName = reader.GetString("last_name"),
                Email = reader.GetString("email"),
                EmailConfirmed = reader.GetBoolean("email_confirmed"),
                PasswordHash = reader.GetString("passwordhash")
            };

        }
        conn.Close();

        return resultUser;
    }

}
