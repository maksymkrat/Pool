using Pool.Shared.Models;

namespace Pool.API.Services.IServicec;

public interface IIrregularVerbService
{
    Task<IrregularVerbModel> GetRandomIrregularVerb();
}