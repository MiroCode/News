using MediatR;
using NewsApp.Infrastructure.CQRS.Common;

namespace NewsApp.Infrastructure.CQRS.Commands.Request
{
    public class UpdateNewsImageCommandRequest : IRequest<EmptyResponse>
    {
        public string Id { get; set; }
        public string NewsId { get; set; }
        public string ImagePath { get; set; }
        public int DisplayOrder { get; set; }

    }
}
