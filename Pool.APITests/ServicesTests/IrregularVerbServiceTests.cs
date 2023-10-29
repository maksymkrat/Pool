using Microsoft.Extensions.Logging;
using Moq;
using Pool.API.Repository;
using Pool.API.Repository.IRepository;
using Pool.API.Services;
using Pool.API.Services.IServicec;
using Pool.Shared.Models;

namespace Pool.APITests.Services;

[TestClass]
public class IrregularVerbServiceTests
{
   

    [TestMethod]
    public void GetRandomIrregularVerb_GetIrregularVerbModel()
    {
        Mock<IIrregularVerbRepository> mockVerbRepo = new Mock<IIrregularVerbRepository>();
        IIrregularVerbService service = new IrregularVerbService(mockVerbRepo.Object);

        mockVerbRepo.Setup(x => x.GetRandomIrregularVerb())
            .ReturnsAsync((() => null));

        Assert.ThrowsExceptionAsync<NullReferenceException>((() => service.GetRandomIrregularVerb()));


    }

  
}