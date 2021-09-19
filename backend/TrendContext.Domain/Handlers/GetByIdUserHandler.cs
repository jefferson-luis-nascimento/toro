using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using TrendContext.Domain.Commands.Requests;
using TrendContext.Domain.Commands.Responses;
using TrendContext.Domain.Repositories.Interfaces;

namespace TrendContext.Domain.Handlers
{
    public class GetByIdUserHandler : IRequestHandler<GetByIdUserRequest, CommandResponse<GetByIdUserResponse>>
    {
        private readonly IUserRepository repository;
        private readonly ILogger<GetByIdUserHandler> logger;

        public GetByIdUserHandler(IUserRepository repository,
            ILogger<GetByIdUserHandler> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        public async Task<CommandResponse<GetByIdUserResponse>> Handle(GetByIdUserRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await repository.GetByIdAsync(request.Id);

                if(result == null)
                {
                    return new CommandResponse<GetByIdUserResponse>(false, 404, "User not found.", null);
                }

                return new CommandResponse<GetByIdUserResponse>(true, 200, string.Empty,
                    new GetByIdUserResponse
                    {
                        Id = result.Id,
                        Name = result.Name,
                        CPF = result.CPF,
                        CheckingAccountAmount = result.CheckingAccountAmount,
                    });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return new CommandResponse<GetByIdUserResponse>(false, 500, "Internal Server Error", null);
            }
            
        }
    }
}
