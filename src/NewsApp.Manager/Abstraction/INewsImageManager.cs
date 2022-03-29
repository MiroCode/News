using NewsApp.Infrastructure.CQRS.Commands.Request;
using NewsApp.Infrastructure.CQRS.Commands.Response;
using NewsApp.Infrastructure.CQRS.Common;
using NewsApp.Infrastructure.CQRS.Queries.Request;
using NewsApp.Infrastructure.CQRS.Queries.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewsApp.Manager.Abstraction
{
    public interface INewsImageManager
    {
        Task<IEnumerable<ListNewsImageQueryResponse>> GetAllNewsImageAsync(ListNewsImageQueryRequest requestModel);
        Task<NewsImageQueryResponse> GetNewsImageAsync(GetNewsImageQueryRequest requestModel);
        Task<CreateNewsImageCommandResponse> CreateNewsImageAsync(CreateNewsImageCommandRequest requestModel);
        Task<EmptyResponse?> UpdateNewsImageAsync(UpdateNewsImageCommandRequest requestModel);
        Task<EmptyResponse?> DeleteNewsImageAsync(DeleteNewsImageCommandRequest requestModel);
    }
}
