using System.Text.Json.Serialization;

namespace Pool.Shared.Models.DeserializeTranslation;

public class Source
{
    [JsonPropertyName("dialect")]
    public string Dialect { get; set; }
    [JsonPropertyName("text")]
    public string Text { get; set; }
}