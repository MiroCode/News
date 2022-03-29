using MediatR;
using NewsApp.Infrastructure.CQRS.Queries.Response;
using System.Collections.Generic;

namespace NewsApp.Infrastructure.CQRS.Queries.Request
{
    public class ListChannelCategoryMapQueryRequest : IRequest<IEnumerable<ListChannelCategoryMapQueryResponse>>
    {
        public string ChannelId { get; set; }
        public string CategoryId { get; set; }
    }
}
