using MediatR;
using NewsApp.Infrastructure.CQRS.Common;

namespace NewsApp.Infrastructure.CQRS.Commands.Request
{

    public class DeleteChannelCommandRequest : IRequest<EmptyResponse>
    {
        public string Id { get; set; }
    }
}