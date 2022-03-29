using MediatR;
using NewsApp.Infrastructure.CQRS.Commands.Response;

namespace NewsApp.Infrastructure.CQRS.Commands.Request
{
    public class CreateChannelCategoryMapCommandRequest : IRequest<CreateChannelCategoryMapCommandResponse>
    {
        public string ChannelId { get; set; }
        public string CategoryId { get; set; }
        public string XmlPath { get; set; }
    }
}
