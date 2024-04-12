using MySql.Data.MySqlClient;

namespace server;

public class DatabaseExtensions
{
    private readonly IConfiguration _configuration;

    public DatabaseExtensions(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public MySqlConnection ConnectDB()
    {
        MySqlConnection conn = new MySqlConnection();
        conn.ConnectionString = this._configuration.GetConnectionString("craigsaudevDB");
        conn.Open();
        return conn;
    }
}
