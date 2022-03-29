using MediatR;
using NewsApp.Infrastructure.CQRS.Commands.Response;
using System.Text.Json.Serialization;

namespace NewsApp.Infrastructure.CQRS.Commands.Request
{
    public class CreateUserCommandRequest : IRequest<CreateUserCommandResponse>
    {
        public string Email { get; set; }
        [JsonIgnore]
        public string AppId { get; set; }
    }
}
