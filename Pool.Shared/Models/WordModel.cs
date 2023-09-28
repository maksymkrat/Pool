namespace Pool.Shared.Models;

public class WordModel : ICloneable
{
    public int Id { get; set; }
    public string WordText { get; set; }
    public string Translation { get; set; }
    public DateTime InsertDateTime { get; set; }
    public Guid User_Id { get; set; }

    public WordModel()
    {
    }



    public WordModel(int id, string wordText, string translation, DateTime dateTime, Guid userId)
    {
        Id = id;
        WordText = wordText;
        Translation = translation;
        InsertDateTime = dateTime;
        User_Id = userId;
    }

    public object Clone()
    {
        return new WordModel(Id,WordText,Translation,InsertDateTime, User_Id);
    }
}