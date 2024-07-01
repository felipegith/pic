namespace Pic.Infrastructure;


public interface IConsultService
{
    Task<HttpRequestResponse> GetAuthorizeTransfer();
}
