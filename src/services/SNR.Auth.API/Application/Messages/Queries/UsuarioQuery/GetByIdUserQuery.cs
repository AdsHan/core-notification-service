using MediatR;
using SNR.Auth.Domain.Entities;
using System;

namespace SNR.Auth.API.Application.Messages.Queries.UserQuery
{
    public class GetByIdUserQuery : IRequest<UserModel>
    {
        public GetByIdUserQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}
