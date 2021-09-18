using Flunt.Extensions.Br.Validations;
using Flunt.Notifications;
using Flunt.Validations;
using MediatR;
using Newtonsoft.Json;
using TrendContext.Domain.Commands.Responses;
using TrendContext.Shared.Commands;

namespace TrendContext.Domain.Commands.Requests
{
    public class CreateUserRequest : Notifiable<Notification>, IRequest<CommandResponse<CreateUserResponse>>, ICommand
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("cpf")]
        public string CPF { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsNotNullOrEmpty(Name, "Name", "Name is required.")
                .IsNotNullOrEmpty(CPF, "CPF", "CPF is required.")
                .IsCpf(CPF, "CPF", "CPF invalid.")
            );
        }
    }
}
