using System.Net.Http.Json;

namespace Pic.Application.Test;

 public class HttpRequestFixture 
{
    private readonly HttpClient _httpClient;
    private readonly string URL = "https://util.devi.tools/api/v2/authorize";
    public HttpRequestFixture(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));;
    }
    public HttpRequestFixture()
    {
        
    }

    public async Task<HttpRequestResponse> GetAuthorizeTransfer()
    {
        try
        {
            using HttpResponseMessage response = await _httpClient.GetAsync(URL);
            
            var result = await response.Content.ReadFromJsonAsync<HttpRequestResponse>();
            
            return result;
        }
        catch (Exception exception)
        {
            Console.Write(exception.Message);
            throw;
        }
        
    }
}
