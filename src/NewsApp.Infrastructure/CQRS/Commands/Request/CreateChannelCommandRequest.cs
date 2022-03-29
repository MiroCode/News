using MediatR;
using NewsApp.Infrastructure.CQRS.Commands.Response;

namespace NewsApp.Infrastructure.CQRS.Commands.Request
{
    public class CreateChannelCommandRequest : IRequest<CreateChannelCommandResponse>
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string ImagePath { get; set; }
        public int DisplayOrder { get; set; }

    }
}