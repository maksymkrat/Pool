using Newtonsoft.Json;
using Pool.API.Services;
using Pool.API.Services.IServicec;
using Pool.Shared.Models.DeserializeTranslation;

namespace Pool.APITests.Services;

[TestClass]
public class TranslatorServiceTests
{
    private Translator _expected;
    private static ITranslatorService _translatorService;

    [ClassInitialize]
    public static void ClassInitialize(TestContext testContext)
    {
        _translatorService = new TranslatorService();
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
    
        
    // [TestMethod]
    // public  void Translate_InputNull_ReturnEmptyTranslatorModel()
    // {
    //     //act and Assert
    //     Assert.ThrowsException<NullReferenceException>((() => { _translatorService.Translate(null);}));
    // }

   
    [TestMethod]
    public   void Translate_InputWord_ReturnTranslatorModel()
    {
        //arrange
        string input = "word";
        _translatorService = new TranslatorService();
        
        //act
        var actual =  _translatorService.Translate(input).Result;
        
        var expectedString = JsonConvert.SerializeObject(_expected);
        var actualString = JsonConvert.SerializeObject(actual);
        //asserts
        
        Assert.AreEqual(expectedString, actualString);
        
    }
  
    [TestMethod]
    public void M_m_m()
    {
        Assert.ThrowsException<NullReferenceException>((() => { _translatorService.M(); }));
    }
    
  


}