using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using Pool.API.Services;
using Pool.API.Services.IServicec;
using Pool.Shared.Models.DeserializeTranslation;

namespace Pool.APITests.Services;

[TestClass]
public class TranslatorServiceTests
{
    private Translator _expected;
    static Mock<ILogger<TranslatorService>> _loggerMock = new Mock<ILogger<TranslatorService>>();
    private static ITranslatorService _translatorService = new TranslatorService(_loggerMock.Object);


    [ClassInitialize]
    public static void ClassInitialize(TestContext testContext)
    {
   
    }

    [TestInitialize]
    public void TestInitialize()
    {
        _expected = new Translator();
        _expected.Source = new Source();
        _expected.Target = new Target();

        _expected.Source.Dialect = "en";
        _expected.Source.Text = "word";
        _expected.Target.Dialect = "uk";
        _expected.Target.Text = "слово";
    }

    [TestCleanup]
    public void TestCleanUp()
    {
    }


   

    [TestMethod]
    public async Task Translate_WithNullWord_ThrowsException()
    {
        // Arrange
        var loggerMock = new Mock<ILogger<TranslatorService>>();
        var translatorService = new TranslatorService(loggerMock.Object);
        
        // Act and Assert
        await Assert.ThrowsExceptionAsync<NullReferenceException>(() => translatorService.Translate(null));
    }


    [TestMethod]
    public void Translate_InputWord_ReturnTranslatorModel()
    {
        //arrange
        string input = "word";

        //act
        var actual = _translatorService.Translate(input).Result;

        var expectedString = JsonConvert.SerializeObject(_expected);
        var actualString = JsonConvert.SerializeObject(actual);
        //asserts

        Assert.AreEqual(expectedString, actualString);
    }

    [TestMethod]
    public void M_m_m()
    {
      
    }
}