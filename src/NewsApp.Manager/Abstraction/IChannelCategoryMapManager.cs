using NewsApp.Infrastructure.CQRS.Commands.Request;
using NewsApp.Infrastructure.CQRS.Commands.Response;
using NewsApp.Infrastructure.CQRS.Common;
using NewsApp.Infrastructure.CQRS.Queries.Request;
using NewsApp.Infrastructure.CQRS.Queries.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewsApp.Manager.Abstraction
{
    public interface IChannelCategoryMapManager
    {
        Task<IEnumerable<ListChannelCategoryMapQueryResponse>> GetAllChannelCategoryMapAsync(ListChannelCategoryMapQueryRequest requestModel);
        Task<ChannelCategoryMapQueryResponse> GetChannelCategoryMapAsync(GetChannelCategoryMapQueryRequest requestModel);
        Task<CreateChannelCategoryMapCommandResponse> CreateChannelCategoryMapAsync(CreateChannelCategoryMapCommandRequest requestModel);
        Task<EmptyResponse?> UpdateChannelCategoryMapAsync(UpdateChannelCategoryMapCommandRequest requestModel);
        Task<EmptyResponse?> DeleteChannelCategoryMapAsync(DeleteChannelCategoryMapCommandRequest requestModel);
    }
}
