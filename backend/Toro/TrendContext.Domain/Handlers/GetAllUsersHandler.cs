using MediatR;
using Microsoft.Extensions.Logging;
using System;
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
    public class GetAllUsersHandler : IRequestHandler<GetAllUsersRequest, CommandResponse<IEnumerable<GetAllUsersResponse>>>
    {
        private readonly IRepository<User> repository;
        private readonly ILogger<GetAllUsersHandler> logger;

        public GetAllUsersHandler(IRepository<User> repository,
            ILogger<GetAllUsersHandler> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        public async Task<CommandResponse<IEnumerable<GetAllUsersResponse>>> Handle(GetAllUsersRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await repository.GetAllAsync();

                return new CommandResponse<IEnumerable<GetAllUsersResponse>>(true, 200, string.Empty,
                    result.Select(user => new GetAllUsersResponse
                    {
                        Id = user.Id,
                        Name = user.Name,
                        CPF = user.CPF,
                        CheckingAccountAmount = user.CheckingAccountAmount,
                    }).AsEnumerable());
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return new CommandResponse<IEnumerable<GetAllUsersResponse>>(false, 500, "Internal Server Error", null);
            }

            
        }
    }
}
