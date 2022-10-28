using Microsoft.Data.SqlClient;
using Pool.API.Models;
using Pool.API.Repository.IRepository;

namespace Pool.API.Repository;

public class UserRepository : IUserRepository
{
    private readonly IConfiguration _configuration;
    private readonly string _defaultConnection;
    public UserRepository( IConfiguration configuration)
    {
        _configuration = configuration;
        _defaultConnection = _configuration.GetConnectionString("DefaultConnection");
    }

    public UserAccount GetUserAccountByEmail(string email)
    {
        using (var conn = new SqlConnection(_defaultConnection))
        {
            
        }

        return null;
    }
}