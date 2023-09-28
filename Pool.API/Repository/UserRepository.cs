using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Pool.API.Models;
using Pool.API.Repository.IRepository;
using Pool.Shared.Models;

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
        Console.WriteLine(_defaultConnection);
    }

    public async Task<UserAccount> GetUserAccountByEmail(string email)
    {
        _logger.LogInformation($"{DateTime.UtcNow.ToLongTimeString()} method: GetUserAccountByEmail");
        try
        {
            using (IDbConnection db = new SqlConnection(_defaultConnection))
            {
                var users = await db.QueryAsync<UserAccount>(
                    "SELECT  [U].[Id]," +
                    "[U].[First_name] as FirstName," +
                    "[U].[Last_name] as LastName," +
                    "[U].[Email]," +
                    "[U].[Username]," +
                    "[U].[Phone_number] as PhoneNumber," +
                    "[U].[PasswordHash]," +
                    "R.[Name] AS [Role]" +
                    "FROM [Users] U" +
                    " LEFT JOIN Users_Roles AS UR  ON U.Id = UR.User_id" +
                    " LEFT JOIN Roles AS R ON UR.Role_id = R.Id" +
                    " WHERE U.Email = @email ", new {email});

                return users.FirstOrDefault();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> CreateUser(UserAccount user)
    {
        _logger.LogInformation($"{DateTime.UtcNow.ToLongTimeString()} method: CreateUser");
        try
        {
            using (IDbConnection db = new SqlConnection(_defaultConnection))
            {
                var sqlQuery =
                    $"INSERT INTO Users (Id, First_name, Last_name,Username, Email, Phone_number, PasswordHash)" +
                    "VALUES (@Id, @FirstName, @LastName, @Username, @Email, @PhoneNumber, @PasswordHash)" +
                    "INSERT INTO Users_Roles(User_id, Role_id)" +
                    "VALUES (@Id, (SELECT r.Id FROM Roles r WHERE r.[Name] = N'USER'))";
                var reult = db.Execute(sqlQuery, user);
                return reult > 0;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}