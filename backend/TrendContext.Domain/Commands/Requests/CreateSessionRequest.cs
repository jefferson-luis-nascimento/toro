using Flunt.Extensions.Br.Validations;
using Flunt.Notifications;
using Flunt.Validations;
using MediatR;
using Newtonsoft.Json;
using TrendContext.Domain.Commands.Responses;
using TrendContext.Shared.Commands;

namespace TrendContext.Domain.Commands.Requests
{
    public class CreateSessionRequest : Notifiable<Notification>, IRequest<CommandResponse<CreateSessionResponse>>, ICommand
    {

        [JsonProperty("cpf")]
        public string CPF { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsNotNullOrEmpty(CPF, "CPF", "CPF is required.")
                .IsCpf(CPF, "CPF", "CPF invalid.")
            );
        }
    }
}
