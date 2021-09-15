using MediatR;
using System;
using System.Collections.Generic;
using TrendContext.Domain.Commands.Responses;

namespace TrendContext.Domain.Commands.Requests
{
    public class GetAllUsersRequest : IRequest<IEnumerable<GetAllUsersResponse>>
    {
        public void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
