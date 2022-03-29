using MediatR;
using NewsApp.Infrastructure.CQRS.Commands.Request;
using NewsApp.Infrastructure.CQRS.Commands.Response;
using NewsApp.Infrastructure.CQRS.Common;
using NewsApp.Infrastructure.CQRS.Queries.Request;
using NewsApp.Infrastructure.CQRS.Queries.Response;
using NewsApp.Manager.Abstraction;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewsApp.Manager
{
    public class SearchHistoryManager : ISearchHistoryManager
    {
        private readonly IMediator _mediator;
        public SearchHistoryManager(IMediator mediator
            )
        {
            _mediator = mediator;
        }
        public async Task<CreateSearchHistoryCommandResponse> CreateSearchHistoryAsync(CreateSearchHistoryCommandRequest requestModel)
        {
            return await _mediator.Send(requestModel);
        }

        public async Task<EmptyResponse> DeleteSearchHistoryAsync(DeleteSearchHistoryCommandRequest requestModel)
        {
            return await _mediator.Send(requestModel);
        }

        public async Task<IEnumerable<ListSearchHistoryQueryResponse>> GetAllSearchHistoryAsync(ListSearchHistoryQueryRequest requestModel)
        {
            return await _mediator.Send(requestModel);
        }

        public async Task<SearchHistoryQueryResponse> GetSearchHistoryAsync(GetSearchHistoryQueryRequest requestModel)
        {
            return await _mediator.Send(requestModel);
        }

        public async Task<EmptyResponse> UpdateSearchHistoryAsync(UpdateSearchHistoryCommandRequest requestModel)
        {
            return await _mediator.Send(requestModel);
        }
    }
}
