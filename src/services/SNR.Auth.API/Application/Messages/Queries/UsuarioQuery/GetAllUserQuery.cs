
using MediatR;
using SNR.Auth.API.Application.DTO;
using System.Collections.Generic;

namespace SNR.Auth.API.Application.Messages.Queries.UserQuery
{

    public class GetAllUserQuery : IRequest<List<UserDTO>>
    {
    }

}
