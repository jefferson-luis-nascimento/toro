using Flunt.Notifications;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TrendContext.Domain.Commands.Requests;
using TrendContext.Domain.Commands.Responses;
using TrendContext.Domain.Data.Interfaces;
using TrendContext.Domain.Entities;
using TrendContext.Domain.Repositories.Interfaces;

namespace TrendContext.Domain.Handlers
{
    public class CreateSessionHandler : Notifiable<Notification>, IRequestHandler<CreateSessionRequest, CommandResponse<CreateSessionResponse>>
    {
        private readonly IUserRepository repository;
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<CreateSessionHandler> logger;

        public CreateSessionHandler(IUserRepository repository, 
            IUnitOfWork unitOfWork,
            ILogger<CreateSessionHandler> logger)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        public async Task<CommandResponse<CreateSessionResponse>> Handle(CreateSessionRequest request, CancellationToken cancellationToken)
        {
            try
            {
                request.Validate();

                if(!request.IsValid)
                {
                    return new CommandResponse<CreateSessionResponse>(false, 400, string.Join("<br />", request.Notifications.Select(x => x.Message)), null);
                }

                var existsUser = await repository.GetByCPFAsync(request.CPF);

                if (existsUser == null)
                {
                    return new CommandResponse<CreateSessionResponse>(false, 400, "User not found.", null);
                }

                var user = new User
                {
                    Name = existsUser.Name,
                    CPF = request.CPF,
                    CheckingAccountAmount = 100M,
                };

                unitOfWork.BeginTransaction();

                repository.Create(user);
                await unitOfWork.Commit();

                return new CommandResponse<CreateSessionResponse>(true, 201, string.Empty,
                    new CreateSessionResponse
                    {
                        Id = user.Id,
                        Name = user.Name,
                        CPF = user.CPF,
                        Token = "token JWT"
                    });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                unitOfWork.Rollback();
                return new CommandResponse<CreateSessionResponse>(false, 500, "Internal Server Error", null);
            }
        }
    }
}
