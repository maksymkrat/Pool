using System.Data;
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

    public async Task<List<WordModel>> GetAllUsersWords(Guid userId) //Guid userId add in parametrs
    {
        _logger.LogInformation($"{DateTime.UtcNow.ToLongTimeString()} method: GetAllUsersWords");

        List<WordModel> words = new List<WordModel>();
        using (var conn = new SqlConnection(_defaultConnection))
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Words" +
                                            " WHERE User_Id = @userId " +
                                            " ORDER BY Id DESC", conn);
            cmd.Parameters.Add(new SqlParameter("@userId", userId));
            cmd.CommandType = CommandType.Text;
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    WordModel word = new WordModel()
                    {
                        Id = Int32.Parse(reader["Id"].ToString()),
                        WordText = reader["Word"].ToString(),
                        Translation = reader["Translation"].ToString(),
                        User_id = new Guid(reader["User_Id"].ToString())
                    };
                    words.Add(word);
                }
            }

            return words;
        }
    }


    public async Task<bool> DeleteById(int id)
    {
        _logger.LogInformation($"{DateTime.UtcNow.ToLongTimeString()} method: DeleteWordById");
        try
        {
            using (var conn = new SqlConnection(_defaultConnection))
            {
                await conn.OpenAsync();
                SqlCommand cmd = new SqlCommand("DELETE FROM Words WHERE Id=@id", conn);
                cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int) {Value = id});

                return await cmd.ExecuteNonQueryAsync() > 0;
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
            using (var conn = new SqlConnection(_defaultConnection))
            {
                await conn.OpenAsync();
                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO Words (Word, Translation,User_Id) VALUES (@textWord ,@translation,@userId )", conn);
                cmd.Parameters.Add(new SqlParameter("@textWord", word.WordText.ToLower()));
                cmd.Parameters.Add(new SqlParameter("@translation", word.Translation.ToLower()));
                cmd.Parameters.Add(new SqlParameter("@userId", new Guid(word.User_id.ToString())));

                return await cmd.ExecuteNonQueryAsync() > 0;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }


    public async Task<List<WordModel>> GetFourRandomWords(Guid userId)
    {
        _logger.LogInformation($"{DateTime.UtcNow.ToLongTimeString()} method: GetFourRandomWords");

        try
        {
            List<WordModel> words = new List<WordModel>();
            using (var conn = new SqlConnection(_defaultConnection))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT TOP 4 Id,Word,Translation, User_Id " +
                                                " FROM Words" +
                                                " WHERE User_Id = @userId " +
                                                " ORDER BY NEWID()", conn);
                cmd.Parameters.Add(new SqlParameter("@userId", userId));
                cmd.CommandType = CommandType.Text;
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        WordModel word = new WordModel()
                        {
                            Id = Int32.Parse(reader["Id"].ToString()),
                            WordText = reader["Word"].ToString(),
                            Translation = reader["Translation"].ToString(),
                            User_id = new Guid(reader["User_Id"].ToString()),
                        };
                        words.Add(word);
                    }
                }

                return words;
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
            using (var conn = new SqlConnection(_defaultConnection))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT TOP 1 Id,Word,Translation,User_Id" +
                                                " FROM Words" +
                                                " WHERE User_Id = @userId " +
                                                " ORDER BY NEWID()", conn);
                cmd.Parameters.Add(new SqlParameter("@userId", userId));
                cmd.CommandType = CommandType.Text;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    reader.Read();
                    WordModel word = new WordModel()
                    {
                        Id = Int32.Parse(reader["Id"].ToString()),
                        WordText = reader["Word"].ToString(),
                        Translation = reader["Translation"].ToString(),
                        User_id = new Guid(reader["User_Id"].ToString()),
                    };

                    return word;
                }
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
            using (var conn = new SqlConnection(_defaultConnection))
            {
                await conn.OpenAsync();
                SqlCommand cmd = new SqlCommand(
                    "UPDATE Words SET Word = @textWord, Translation = @translation WHERE Id = @Id", conn);
                cmd.Parameters.Add(new SqlParameter("@textWord", word.WordText.ToLower()));
                cmd.Parameters.Add(new SqlParameter("@translation", word.Translation.ToLower()));
                cmd.Parameters.Add(new SqlParameter("@Id", word.Id));

                return await cmd.ExecuteNonQueryAsync() > 0;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}