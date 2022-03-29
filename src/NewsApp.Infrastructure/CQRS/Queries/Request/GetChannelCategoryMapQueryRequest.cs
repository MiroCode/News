using MediatR;
using NewsApp.Infrastructure.CQRS.Queries.Response;

namespace NewsApp.Infrastructure.CQRS.Queries.Request
{
    public class GetChannelCategoryMapQueryRequest : IRequest<ChannelCategoryMapQueryResponse>
    {
        public string Id { get; set; }
    }
}
