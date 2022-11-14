using System.Speech.Synthesis;

namespace Pool.Client.Services;

public class SpeechService
{
    private readonly SpeechSynthesizer _speechSynthesizer;
    private readonly ILogger _logger;

    public SpeechService( ILogger<SpeechService> logger)
    {
        _speechSynthesizer = new SpeechSynthesizer();
        _speechSynthesizer.SetOutputToDefaultAudioDevice();
        _logger = logger;
    }

    public async Task Speech(string word)
    {
        _logger.LogInformation($"{DateTime.UtcNow.ToLongTimeString()} method: Speech");
        _speechSynthesizer.Speak(word);
    }
}