using System.Text.Json.Serialization;

namespace Pool.Shared.Models.DeserializeTranslation;

public class Translator
{
    [JsonPropertyName("source")]
    public Source Source { get; set; }
        
    [JsonPropertyName("target")]
    public Target Target { get; set; }
    
}