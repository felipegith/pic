namespace Pic.Domain;

public class User 
{
    public Guid Id {get; private set;}
    public string Name {get; private set;}
    public string Email {get; private set;}
    public long Document {get; private set;}
    public string Password {get; private set;}
    public Type Type {get; private set;}
    public Value Value {get; private set;} = new Value();

    public void SetName(string name)
    {
        Name = name;
    }

    public void SetEmail(string email)
    {
        Email = email;
    }
    public void SetDocument(long document)
    {
        Document = document;
    }

    public void SetPassword(string password)
    {
        Password = password;
    }
    
    public void SetType(Type type)
    {
        Type = type;
    }
    
    public static User Create(string name, string email, long document, string password, Type type)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = name,
            Email = email,
            Document = document,
            Password = password,
            Type = type,
        };

        return user;
    }

    public static string GetUserType(Type type)
    {
        string UserType = string.Empty;
        switch (type)
        {
            case Type.Common:
                UserType = Type.Common.ToString();
                break;
            case Type.Shopkeeper:
                UserType = Type.Shopkeeper.ToString();
                break;
        }

        return UserType;
    }
}
