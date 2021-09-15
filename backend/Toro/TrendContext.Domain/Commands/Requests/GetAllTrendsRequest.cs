using MediatR;
using System;
using System.Collections.Generic;
using TrendContext.Domain.Commands.Responses;

namespace TrendContext.Domain.Commands.Requests
{
    public class GetAllTrendsRequest : IRequest<IEnumerable<GetAllTrendsResponse>>
    {
        public void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
