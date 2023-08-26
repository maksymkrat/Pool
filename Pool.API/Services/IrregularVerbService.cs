using Pool.API.Repository.IRepository;
using Pool.API.Services.IServicec;
using Pool.Shared.Models;

namespace Pool.API.Services;

public class IrregularVerbService : IIrregularVerbService
{

    private readonly IIrregularVerbRepository _irregularVerbRepository;

    public IrregularVerbService(IIrregularVerbRepository irregularVerbRepository)
    {
        _irregularVerbRepository = irregularVerbRepository;
    }

    public async Task<IrregularVerbModel> GetRandomIrregularVerb()
    {
        return await _irregularVerbRepository.GetRandomIrregularVerb();
    }
}