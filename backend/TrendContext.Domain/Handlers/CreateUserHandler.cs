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
    public class CreateUserHandler : Notifiable<Notification>, IRequestHandler<CreateUserRequest, CommandResponse<CreateUserResponse>>
    {
        private readonly IUserRepository repository;
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<CreateUserHandler> logger;

        public CreateUserHandler(IUserRepository repository, 
            IUnitOfWork unitOfWork,
            ILogger<CreateUserHandler> logger)
        {
            this.repository = repository;
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        public async Task<CommandResponse<CreateUserResponse>> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            try
            {
                request.Validate();

                if(!request.IsValid)
                {
                    return new CommandResponse<CreateUserResponse>(false, 400, string.Join("<br />", request.Notifications.Select(x => x.Message)), null);
                }


                if (await repository.CheckCpfAlreadyExistsAsync(request.CPF))
                {
                    return new CommandResponse<CreateUserResponse>(false, 400, "Already exists this CPF.", null);
                }

                var user = new User
                {
                    Name = request.Name,
                    CPF = request.CPF,
                    CheckingAccountAmount = 100M,
                };

                unitOfWork.BeginTransaction();

                repository.Create(user);
                await unitOfWork.Commit();

                return new CommandResponse<CreateUserResponse>(true, 201, string.Empty,
                    new CreateUserResponse
                    {
                        Id = user.Id,
                        Name = user.Name,
                        CPF = user.CPF,
                    });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                unitOfWork.Rollback();
                return new CommandResponse<CreateUserResponse>(false, 500, "Internal Server Error", null);
            }
        }
    }
}
