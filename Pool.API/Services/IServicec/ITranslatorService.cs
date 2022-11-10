using Pool.Shared.Models.DeserializeTranslation;

namespace Pool.API.Services.IServicec;

public interface ITranslatorService
{
    Task<Translater> Translate(string word);
}