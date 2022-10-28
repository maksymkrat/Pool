namespace Pool.Shared.Models;

public class Word
{
    public int Id { get; set; }
    public string WordText { get; set; }
    public string Translation { get; set; }
    public DateTime DateTime { get; set; }
    public Guid User_id { get; set; }
}