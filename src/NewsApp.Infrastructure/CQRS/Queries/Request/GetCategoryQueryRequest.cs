using MediatR;
using NewsApp.Infrastructure.CQRS.Queries.Response;

namespace NewsApp.Infrastructure.CQRS.Queries.Request
{
    public class GetCategoryQueryRequest : IRequest<CategoryQueryResponse>
    {
        public string Id { get; set; }
    }
}