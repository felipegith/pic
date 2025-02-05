﻿using MediatR;
using Pic.Domain;
using Pic.Infrastructure;

namespace Pic.Application;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Response>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _uow;

    public CreateUserCommandHandler(IUserRepository userRepository, IUnitOfWork uow)
    {
        _userRepository = userRepository;
        _uow = uow;
    }

    public async Task<Response> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var email = await _userRepository.IsUniqueEmail(request.Email, cancellationToken);

        if(email)
        {
            return new Response(false, "Email already exists", System.Net.HttpStatusCode.BadRequest);
        }

        var verifyDocument = await _userRepository.IsUniqueDocument(request.Document, cancellationToken);

        if(verifyDocument)
        {
            return new Response(false, "Document already exists", System.Net.HttpStatusCode.BadRequest);
        }
        var create = User.Create(request.Name, request.Email, request.Document, request.Password, request.Type);

        _userRepository.Create(create);
        await _uow.Commit();

        return new Response(true, "User created sucessfully", System.Net.HttpStatusCode.Created, create);
        
    }
}
