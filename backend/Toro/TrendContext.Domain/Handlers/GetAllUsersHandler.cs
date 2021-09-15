using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TrendContext.Domain.Commands.Requests;
using TrendContext.Domain.Commands.Responses;
using TrendContext.Domain.Entities;
using TrendContext.Shared.Repository;

namespace TrendContext.Domain.Handlers
{
    public class GetAllUsersHandler : IRequestHandler<GetAllUsersRequest, IEnumerable<GetAllUsersResponse>>
    {
        private readonly IRepository<User> repository;

        public GetAllUsersHandler(IRepository<User> repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<GetAllUsersResponse>> Handle(GetAllUsersRequest request, CancellationToken cancellationToken)
        {
            var result = await repository.GetAllAsync();

            return result.Select(user => new GetAllUsersResponse
            {
                Id = user.Id,
                Name = user.Name,
                CPF = user.CPF,
                CheckingAccountAmount = user.CheckingAccountAmount,
            }).AsEnumerable();
        }
    }
}
