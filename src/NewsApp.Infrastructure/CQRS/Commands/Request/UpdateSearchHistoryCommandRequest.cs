using MediatR;
using NewsApp.Infrastructure.CQRS.Common;

namespace NewsApp.Infrastructure.CQRS.Commands.Request
{
    public class UpdateSearchHistoryCommandRequest : IRequest<EmptyResponse>
    {
        public string Id { get; set; }
        public string SearchText { get; set; }
    }
}
