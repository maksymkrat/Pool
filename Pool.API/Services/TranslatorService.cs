﻿using System.Net;
using System.Text.Json;
using Pool.API.Services.IServicec;
using Microsoft.Extensions.Logging;
using Pool.Shared.Models.DeserializeTranslation;

namespace Pool.API.Services;

public class TranslatorService : ITranslatorService
{
    private readonly ILogger _logger;

    private const string URL = "https://dev-api.itranslate.com/translation/v2/";
    private const string METHOD_POST = "POST";
    private const string HEADERS = "Bearer 603160b7-cee1-4c13-bcd7-37420b55211d";
    private const string CONTENT_TYPE = "application/json";

    public TranslatorService(ILogger<TranslatorService> logger)
    {
        _logger = logger;
    }

    public async Task<Translater> Translate(string word)
    {
        try
        {
            var httpRequest = (HttpWebRequest) WebRequest.Create(URL);
            httpRequest.Method = METHOD_POST;
            httpRequest.Headers["Authorization"] = HEADERS;
            httpRequest.ContentType = CONTENT_TYPE;

            var data = $@"{{
                         ""source"": {{""dialect"": ""en"", ""text"": ""{word}""}},
                         ""target"": {{""dialect"": ""uk""}}
                            }}";

            using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
            {
                streamWriter.Write(data);
            }

            var httpResponse = (HttpWebResponse) httpRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();

                Translater translater = JsonSerializer.Deserialize<Translater>(result);

                return translater;
            }
        }
        catch (Exception e)
        {
            _logger.LogInformation($"{DateTime.UtcNow.ToLongTimeString()} method: Translate");
            return new Translater();
        }
    }
}