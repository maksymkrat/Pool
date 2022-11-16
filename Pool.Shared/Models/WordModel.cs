﻿namespace Pool.Shared.Models;

public class WordModel : ICloneable
{
    public int Id { get; set; }
    public string WordText { get; set; }
    public string Translation { get; set; }
    public DateTime DateTime { get; set; }
    public Guid User_id { get; set; }

    public WordModel()
    {
    }



    public WordModel(int id, string wordText, string translation, DateTime dateTime, Guid userId)
    {
        Id = id;
        WordText = wordText;
        Translation = translation;
        DateTime = dateTime;
        User_id = userId;
    }

    public object Clone()
    {
        return new WordModel(Id,WordText,Translation,DateTime, User_id);
    }
}