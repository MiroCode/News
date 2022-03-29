using MediatR;
using NewsApp.Infrastructure.CQRS.Commands.Response;

namespace NewsApp.Infrastructure.CQRS.Commands.Request
{
    public class CreateSearchHistoryCommandRequest : IRequest<CreateSearchHistoryCommandResponse>
    {
        public string SearchText { get; set; }
    }
}
