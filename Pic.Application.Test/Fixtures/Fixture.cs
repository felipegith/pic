using System.Net;
using System.Text;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Pic.Domain;

namespace Pic.Application.Test;

public static class Fixture
{
    public static Guid Id = Guid.NewGuid();
    public static string Name = "Felipe Costa";
    public static string Email = "felipe@mail.com";
    public static long Document = 23109876654;
    public static string Password = "12345678";
    public static decimal Balance = 1000;
    public static decimal Value = 800;
    public static long Payer = 23109876654;
    public static long Payee = 8766542310;
    public static Domain.Type Type = Domain.Type.Common;
    public static Domain.Type Shopkeeper = Domain.Type.Shopkeeper;
    public static string Success = "User created sucessfully";
    public static string EmailExists = "Email already exists";
    public static string DocumentExists = "Document already exists";

    public static User UserMoq()
    {
        
        var user = new User();
        user.Instance();
        user.SetName(Name);
        user.SetEmail(Email);
        user.SetDocument(Document);
        user.SetPassword(Password);
        user.SetType(Type);
        user.Value?.SetBalance(Balance);
        user.Value?.SetUserId(Id);
        
        return user;
    }

    public static HttpRequestResponse HttpResponseFailMoq()
    {
        return new HttpRequestResponse()
        {
            Status = "Fail",
            Data = new Authorize { Authorization = false }
        };
    }

    public static HttpRequestResponse HttpResponseSuccessMoq()
    {
        return new HttpRequestResponse()
        {
            Status = "Success",
            Data = new Authorize { Authorization = true }
        };
    }

    public static HttpMoq HandleSuccessMoq()
    {
        return new HttpMoq((request, cancellationToken) =>
        {
            var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(HttpResponseSuccessMoq()), Encoding.UTF8, "application/json")
            };
            return Task.FromResult(responseMessage);
        });
    }

    public static HttpMoq HandleFailMoq()
    {
        return new HttpMoq((request, cancellationToken) =>
        {
            var responseMessage = new HttpResponseMessage(HttpStatusCode.Forbidden)
            {
                Content = new StringContent(JsonConvert.SerializeObject(HttpResponseFailMoq()), Encoding.UTF8, "application/json")
            };
            return Task.FromResult(responseMessage);
        });
    }

    public static HttpClient HttpClientMoq(HttpMessageHandler httpMessageHandler)
    {
        return new HttpClient(httpMessageHandler)
        {
            BaseAddress = new Uri("https://util.devi.tools/api/v2/authorize")
        };
    }
}
