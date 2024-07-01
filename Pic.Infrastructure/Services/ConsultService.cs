
using System.Net.Http.Json;

namespace Pic.Infrastructure;

public class ConsultService : IConsultService
{
    private readonly HttpClient _httpClient;
    private readonly string URL = "https://util.devi.tools/api/v2/authorize";
    public ConsultService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<HttpRequestResponse> GetAuthorizeTransfer()
    {
        try
        {
            using HttpResponseMessage response = await _httpClient.GetAsync(URL);
            
            var result = await response.Content.ReadFromJsonAsync<HttpRequestResponse>();
            
            if(result is null)
            {
                return new HttpRequestResponse();
            }
            return result;
        }
        catch (Exception exception)
        {
            Console.Write(exception.Message);
            throw;
        }
    }
}
