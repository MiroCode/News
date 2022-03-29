using MediatR;
using NewsApp.Infrastructure.CQRS.Common;

namespace NewsApp.Infrastructure.CQRS.Commands.Request
{
    public class UpdateNewsNewsTagMapCommandRequest : IRequest<EmptyResponse>
    {
        public string Id { get; set; }
        public string NewsId { get; set; }
        public string TagId { get; set; }
    }
}
