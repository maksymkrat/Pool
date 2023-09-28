using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Pool.API.Repository.IRepository;
using Pool.Shared.Models;

namespace Pool.API.Repository;

public class WordRepository : IWordRepository
{
    private readonly IConfiguration _configuration;
    private readonly string _defaultConnection;
    private readonly ILogger _logger;

    public WordRepository(IConfiguration configuration, ILogger<WordRepository> logger)
    {
        _configuration = configuration;
        _logger = logger;
        _defaultConnection = _configuration.GetConnectionString("DefaultConnection");
    }

    public async Task<IEnumerable<WordModel>> GetAllUsersWords(Guid userId)
    {
        _logger.LogInformation($"{DateTime.UtcNow.ToLongTimeString()} method: GetAllUsersWords");
        
        using (IDbConnection db = new SqlConnection(_defaultConnection))
        {
            var words = await db.QueryAsync<WordModel>("SELECT Id, Word as WordText, Translation, Date as UpdateDateTime, User_Id FROM Words WHERE User_Id = @userId ORDER BY Id DESC", new { userId });
            return words.ToList();
        }
        
    }

    public async Task<IEnumerable<WordModel>> SearchWords( Guid userId, string searchWord)
    {
        try
        {
        _logger.LogInformation($"{DateTime.UtcNow.ToLongTimeString()} method: SearchWords");
        using (IDbConnection db = new SqlConnection(_defaultConnection))
        {
            var words = await db.QueryAsync<WordModel>("SELECT Id, Word as WordText, Translation, Date as UpdateDateTime, User_Id FROM Words  WHERE [User_Id] = @userId AND [Word] LIKE '%' + @searchWord + '%'", new { userId,searchWord});
            return words.ToList();
        }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new List<WordModel>();
        }
    }
    
      public async Task<IEnumerable<WordModel>> GetFourRandomWords(Guid userId)
    {
        _logger.LogInformation($"{DateTime.UtcNow.ToLongTimeString()} method: GetFourRandomWords");

        try
        {
            using (IDbConnection db = new SqlConnection(_defaultConnection))
            {
                var words = await db.QueryAsync<WordModel>(
                    "SELECT TOP 4 Id, Word as WordText, Translation, Date as UpdateDateTime, User_Id " +
                    "FROM Words WHERE User_Id = @userId ORDER BY NEWID()", new { userId});
                return words.ToList();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }


    public async Task<WordModel> GetRandomWord(Guid userId)
    {
        _logger.LogInformation($"{DateTime.UtcNow.ToLongTimeString()} method: GetRandomWord");
        try
        {
            using (IDbConnection db = new SqlConnection(_defaultConnection))
            {
                var words = await db.QueryAsync<WordModel>(
                    "SELECT TOP 1 Id, Word as WordText, Translation, Date as UpdateDateTime, User_Id " +
                    "FROM Words WHERE User_Id = @userId ORDER BY NEWID()", new { userId});
                return words.FirstOrDefault();
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }


    public async Task<bool> DeleteById(int id)
    {
        try
        {
            
            using (IDbConnection db = new SqlConnection(_defaultConnection))
            {
                var sqlQuery = $"DELETE FROM Words WHERE Id=@id";
                var reult = db.Execute(sqlQuery,new {id});
                return reult > 0;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> AddWord(WordModel word)
    {
        _logger.LogInformation($"{DateTime.UtcNow.ToLongTimeString()} method: AddWord");
        try
        {
            using (IDbConnection  db = new SqlConnection(_defaultConnection))
            {
                var sqlQuery = $"INSERT INTO Words (Word, Translation,User_Id) VALUES (@WordText ,@Translation,@User_Id )";
                var reult = db.Execute(sqlQuery, word);
                return reult > 0;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }


  


    public async Task<bool> Update(WordModel word)
    {
        _logger.LogInformation($"{DateTime.UtcNow.ToLongTimeString()} method: Update");
        try
        {
            using (IDbConnection  db = new SqlConnection(_defaultConnection))
            {
                var sqlQuery = $"UPDATE Words SET Word = @WordText, Translation = @Translation WHERE Id = @Id";
                var reult = db.Execute(sqlQuery, word);
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