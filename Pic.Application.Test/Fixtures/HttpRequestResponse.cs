using System.Net;

namespace Pic.Application.Test;

public class HttpRequestResponse
{
    public string Status {get; set;}
    public Authorize Data {get; set;}
};

public class Authorize
{
    
    public bool Authorization {get; set;}

}
