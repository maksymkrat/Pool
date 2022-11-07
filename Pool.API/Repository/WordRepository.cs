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

    public async Task<List<Word>> GetAllUsersWords(Guid userId) //Guid userId add in parametrs
    {
        List<Word> words = new List<Word>();
        using (var conn = new SqlConnection(DefaultConnection))
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


    public async Task<bool> DeleteWordById(int id)
    {
        using (var conn = new SqlConnection(DefaultConnection))
        {
            await conn.OpenAsync();
            SqlCommand cmd = new SqlCommand("DELETE FROM Words WHERE Id=@id", conn);
            cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.Int) {Value = id});

            return await cmd.ExecuteNonQueryAsync() > 0;
        }
    }

    public async Task<List<Word>> GetFourRandomWords(Guid userId)
    {
        try
        {
            List<Word> words = new List<Word>();
            using (var conn = new SqlConnection(DefaultConnection))
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
                        Word word = new Word()
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
            return null;
        }
    }


    public async Task<Word> GetRandomWord(Guid userId)
    {
        try
        {
            using (var conn = new SqlConnection(DefaultConnection))
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
                    Word word = new Word()
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
            return null;
        }
    }
    public async Task<bool> AddWord(Word word)
    {
        using (var conn = new SqlConnection(DefaultConnection))
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

    public async Task<bool> Update(Word word)
    {
        using (var conn = new SqlConnection(DefaultConnection))
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
    
}