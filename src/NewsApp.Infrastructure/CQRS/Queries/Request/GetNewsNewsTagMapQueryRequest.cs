using MediatR;
using NewsApp.Infrastructure.CQRS.Queries.Response;

namespace NewsApp.Infrastructure.CQRS.Queries.Request
{
    public class GetNewsNewsTagMapQueryRequest : IRequest<NewsNewsTagMapQueryResponse>
    {
        public string Id { get; set; }
        public string NewsId { get; set; }
    }
}
