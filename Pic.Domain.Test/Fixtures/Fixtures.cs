namespace Pic.Domain.Test;

public static class Fixtures
{
    public static string Name = "Felipe Costa";
    public static string Email = "felipe@mail.com";
    public static long Document = 23109876654;
    public static string Password = "12345678";
    public static Type Type = Type.Common;
    public static Guid Id = Guid.NewGuid();
    public static decimal InvalidBalance = -1;
    public static decimal Balance = 1000;
}
