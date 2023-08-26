using Pool.Shared.Models;

namespace Pool.API.Repository.IRepository;

public interface IIrregularVerbRepository
{
    Task<IrregularVerbModel> GetRandomIrregularVerb();
}