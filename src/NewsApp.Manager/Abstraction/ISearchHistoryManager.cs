using NewsApp.Infrastructure.CQRS.Commands.Request;
using NewsApp.Infrastructure.CQRS.Commands.Response;
using NewsApp.Infrastructure.CQRS.Common;
using NewsApp.Infrastructure.CQRS.Queries.Request;
using NewsApp.Infrastructure.CQRS.Queries.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewsApp.Manager.Abstraction
{
    public interface ISearchHistoryManager
    {
        Task<IEnumerable<ListSearchHistoryQueryResponse>> GetAllSearchHistoryAsync(ListSearchHistoryQueryRequest requestModel);
        Task<SearchHistoryQueryResponse> GetSearchHistoryAsync(GetSearchHistoryQueryRequest requestModel);
        Task<CreateSearchHistoryCommandResponse> CreateSearchHistoryAsync(CreateSearchHistoryCommandRequest requestModel);
        Task<EmptyResponse?> UpdateSearchHistoryAsync(UpdateSearchHistoryCommandRequest requestModel);
        Task<EmptyResponse?> DeleteSearchHistoryAsync(DeleteSearchHistoryCommandRequest requestModel);
    }
}
