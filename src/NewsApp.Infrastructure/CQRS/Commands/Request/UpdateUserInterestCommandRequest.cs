using MediatR;
using NewsApp.Infrastructure.CQRS.Common;
using System.Text.Json.Serialization;

namespace NewsApp.Infrastructure.CQRS.Commands.Request
{
    public class UpdateUserInterestCommandRequest : IRequest<EmptyResponse>
    {
        public int Id { get; set; }
        public string ChannelCategoryMapId { get; set; }
        [JsonIgnore]
        public string UserId { get; set; }
    }
}
