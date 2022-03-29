using NewsApp.Infrastructure.CQRS.Commands.Request;
using NewsApp.Infrastructure.CQRS.Commands.Response;
using NewsApp.Infrastructure.CQRS.Common;
using NewsApp.Infrastructure.CQRS.Queries.Request;
using NewsApp.Infrastructure.CQRS.Queries.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewsApp.Manager.Abstraction
{
    public interface INewsNewsTagMapManager
    {
        Task<IEnumerable<ListNewsNewsTagMapQueryResponse>> GetAllNewsNewsTagMapAsync(ListNewsNewsTagMapQueryRequest requestModel);
        Task<NewsNewsTagMapQueryResponse> GetNewsNewsTagMapAsync(GetNewsNewsTagMapQueryRequest requestModel);
        Task<CreateNewsNewsTagMapCommandResponse> CreateNewsNewsTagMapAsync(CreateNewsNewsTagMapCommandRequest requestModel);
        Task<EmptyResponse?> UpdateNewsNewsTagMapAsync(UpdateNewsNewsTagMapCommandRequest requestModel);
        Task<EmptyResponse?> DeleteNewsNewsTagMapAsync(DeleteNewsNewsTagMapCommandRequest requestModel);
    }
}
