using System.Data;
using Microsoft.Data.SqlClient;
using Pool.API.Models;
using Pool.API.Repository.IRepository;

namespace Pool.API.Repository;

public class UserRepository : IUserRepository
{
    private readonly IConfiguration _configuration;
    private readonly string _defaultConnection;
    private readonly ILogger _logger;

    public UserRepository(IConfiguration configuration, ILogger<UserRepository> logger)
    {
        _configuration = configuration;
        _logger = logger;
        _defaultConnection = _configuration.GetConnectionString("DefaultConnection");
    }

    public async Task<UserAccount> GetUserAccountByEmail(string email)
    {
        _logger.LogInformation($"{DateTime.UtcNow.ToLongTimeString()} method: GetUserAccountByEmail");
        try
        {
            using (var conn = new SqlConnection(_defaultConnection))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT  [U].[Id]," +
                                                "[U].[First_name]," +
                                                "[U].[Last_name]," +
                                                "[U].[Email]," +
                                                "[U].[Phone_number]," +
                                                "[U].[PasswordHash]," +
                                                "R.[Name] AS [Role]" +
                                                "FROM [Users] U" +
                                                " LEFT JOIN Users_Roles AS UR  ON U.Id = UR.User_id" +
                                                " LEFT JOIN Roles AS R ON UR.Role_id = R.Id" +
                                                " WHERE U.Email = @email ", conn);
                cmd.Parameters.Add(new SqlParameter("@email", email.ToLower())); // TODO When create user email ot lower
                cmd.CommandType = CommandType.Text;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    UserAccount userAccount = new UserAccount()
                    {
                        Id = new Guid(reader["Id"].ToString()),
                        Email = reader["Email"].ToString(),
                        FirstName = reader["First_name"].ToString(),
                        LastName = reader["Last_name"].ToString(),
                        PhoneNumber = reader["Phone_number"].ToString(),
                        PasswordH = reader["PasswordHash"].ToString(),
                        Role = reader["Role"].ToString().Trim(),
                    };
                    return userAccount;
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}