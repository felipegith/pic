using Microsoft.VisualBasic;
using Pic.Domain;

namespace Pic.Application.Test;

public static class Fixture
{
    public static string Name = "Felipe Costa";
    public static string Email = "felipe@mail.com";
    public static long Document = 23109876654;
    public static string Password = "12345678";
    public static decimal Value = 1000;
    public static long Payer = 23109876654;
    public static decimal Payee = 15;
    public static Domain.Type Type = Domain.Type.Common;
    public static Domain.Type Shopkeeper = Domain.Type.Shopkeeper;
    public static string Success = "User created sucessfully";
    public static string EmailExists = "Email already exists";
    public static string DocumentExists = "Document already exists";

    public static User UserMoq()
    {
        var user = new User();
        user.SetName(Name);
        user.SetEmail(Email);
        user.SetDocument(Document);
        user.SetPassword(Password);
        user.SetType(Type);

        return user;
    }
}
