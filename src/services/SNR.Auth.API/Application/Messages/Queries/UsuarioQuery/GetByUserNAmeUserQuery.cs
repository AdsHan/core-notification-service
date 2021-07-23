using MediatR;
using SNR.Auth.Domain.Entities;
using System;

namespace SNR.Auth.API.Application.Messages.Queries.UserQuery
{
    public class GetByUserNAmeUserQuery : IRequest<UserModel>
    {
        public GetByUserNAmeUserQuery(string userName)
        {
            UserName = userName;
        }

        public String UserName { get; private set; }
    }
}
