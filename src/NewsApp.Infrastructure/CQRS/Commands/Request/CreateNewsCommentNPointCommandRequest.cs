using MediatR;
using NewsApp.Infrastructure.CQRS.Commands.Response;
using System.Text.Json.Serialization;

namespace NewsApp.Infrastructure.CQRS.Commands.Request
{
    public class CreateNewsCommentNPointCommandRequest : IRequest<CreateNewsCommentNPointCommandResponse>
    {
        public string NewsId { get; set; }

        [JsonIgnore]
        public string UserId { get; set; }
        public string CommandText { get; set; }
        public int Point { get; set; }
    }
}