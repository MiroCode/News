using MediatR;
using NewsApp.Infrastructure.CQRS.Commands.Response;
using System.Text.Json.Serialization;

namespace NewsApp.Infrastructure.CQRS.Commands.Request
{
    public class CreateNewsViewCommandRequest : IRequest<CreateNewsViewCommandResponse>
    {
        public string NewsId { get; set; }

        [JsonIgnore]
        public string UserId { get; set; }
    }
}
