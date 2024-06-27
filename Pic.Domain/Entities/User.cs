﻿namespace Pic.Domain;

public class User 
{
    public Guid Id {get; private set;}
    public string Name {get; private set;}
    public string Email {get; private set;}
    public string Document {get; private set;}
    public string Password {get; private set;}
    public Type Type {get; private set;}

    public void SetName(string name)
    {
        Name = name;
    }

    public void SetEmail(string email)
    {
        Email = email;
    }
    public void SetDocument(string document)
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
    
    public static User Create(string name, string email, string document, string password, Type type)
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
}
