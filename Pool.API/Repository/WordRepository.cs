using System.Data;
using Microsoft.Data.SqlClient;
using Pool.API.Repository.IRepository;
using Pool.Shared.Models;

namespace Pool.API.Repository;

public class WordRepository : IWordRepository
{
    private readonly IConfiguration _configuration;
    private readonly string DefaultConnection;
    public WordRepository(IConfiguration configuration)
    {
        _configuration = configuration;
        DefaultConnection = _configuration.GetConnectionString("DefaultConnection");
    }

    public async Task<List<Word>> GetAllUsersWords() //Guid userId add in parametrs
    {
        List<Word> words = new List<Word>();
        using (var conn = new SqlConnection(DefaultConnection))
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Words", conn);
            cmd.CommandType = CommandType.Text;
            using (SqlDataReader reader =  cmd.ExecuteReader())
            {
                while (  reader.Read())
                {
                    Word word = new Word()
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

    public Task<bool> AddWord(Word word, Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteWordById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Word>> GetFourWordsForTestWords(Guid userId)
    {
        throw new NotImplementedException();
    }

    public void UpdateWord(Word word, Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task<Word> GetRandomWord(Guid userId)
    {
        throw new NotImplementedException();
    }
}