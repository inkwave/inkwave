﻿using Inkwave.Application.Common;
using Inkwave.Application.Interfaces.Repositories;
using Inkwave.Domain.User;
using Inkwave.Shared;
using MediatR;

namespace Inkwave.Application.Features.Users.Commands.CreateUser
{
    internal class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateUserCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            //if (await _unitOfWork.Repository<User>().Entities.AnyAsync(x => x.Email.Trim().ToLower() == command.Email.Trim().ToLower()))
            //    return await Result<Guid>.FailureAsync("User already exists");

            PasswordSecurity.CreatePassword(command.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var user = User.CreateUser(command.FirstName, command.LastName, command.Email, command.Phone, command.Gender, passwordHash, passwordSalt);
            await _unitOfWork.Repository<User>().AddAsync(user);
            await _unitOfWork.Save(cancellationToken);
            return await Result<Guid>.SuccessAsync(user.Id, "User Created.");
        }
    }
}
