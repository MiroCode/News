using MediatR;
using NewsApp.Infrastructure.CQRS.Common;

namespace NewsApp.Infrastructure.CQRS.Commands.Request
{
    public class DeleteNewsNewsTagMapCommandRequest : IRequest<EmptyResponse>
    {
        public string Id { get; set; }

        public string NewsId { get; set; }
    }
}
