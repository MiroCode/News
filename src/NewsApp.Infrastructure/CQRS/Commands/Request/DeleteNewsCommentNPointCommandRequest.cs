using MediatR;
using NewsApp.Infrastructure.CQRS.Common;

namespace NewsApp.Infrastructure.CQRS.Commands.Request
{
    public class DeleteNewsCommentNPointCommandRequest : IRequest<EmptyResponse>
    {
        public string NewsId { get; set; }
    }
}
