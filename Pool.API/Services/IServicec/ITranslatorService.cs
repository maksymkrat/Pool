using Pool.Shared.Models.DeserializeTranslation;

namespace Pool.API.Services.IServicec;

public interface ITranslatorService
{
    Task<Translator> Translate(string word);

    Task M();
}