﻿using System.Data;
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

    public Task<bool> AddWord(Word word, Guid userId)
    {
        throw new NotImplementedException();
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
            throw;
        }
    }

    public void UpdateWord(Word word, Guid userId)
    {
        throw new NotImplementedException();
    }
}