using MediatR;
using NewsApp.Infrastructure.CQRS.Queries.Response;
using System.Collections.Generic;

namespace NewsApp.Infrastructure.CQRS.Queries.Request
{
    public class ListNewsQueryRequest : IRequest<IEnumerable<ListNewsQueryResponse>>
    {
        public ListNewsQueryRequest()
        {
            ChannelCategoryMapIds = new List<string>();
        }
        public string NewsId { get; set; }
        public string Title { get; set; }
        public string ProviderNewsId { get; set; }
        public List<string> ChannelCategoryMapIds { get; set; }
    }
}
