using MediatR;
using NewsApp.Infrastructure.CQRS.Common;

namespace NewsApp.Infrastructure.CQRS.Commands.Request
{
    public class UpdateChannelCategoryMapCommandRequest : IRequest<EmptyResponse>
    {
        public string Id { get; set; }
        public string ChannelId { get; set; }
        public string CategoryId { get; set; }
        public string XmlPath { get; set; }
    }
}
