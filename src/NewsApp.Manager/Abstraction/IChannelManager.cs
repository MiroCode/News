using NewsApp.Infrastructure.CQRS.Commands.Request;
using NewsApp.Infrastructure.CQRS.Commands.Response;
using NewsApp.Infrastructure.CQRS.Common;
using NewsApp.Infrastructure.CQRS.Queries.Request;
using NewsApp.Infrastructure.CQRS.Queries.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewsApp.Manager.Abstraction
{
    public interface IChannelManager
    {
        Task<IEnumerable<ListChannelQueryResponse>> GetAllChannelAsync(ListChannelQueryRequest requestModel);
        Task<ChannelQueryResponse> GetChannelAsync(GetChannelQueryRequest requestModel);
        Task<CreateChannelCommandResponse> CreateChannelAsync(CreateChannelCommandRequest requestModel);
        Task<EmptyResponse?> UpdateChannelAsync(UpdateChannelCommandRequest requestModel);
        Task<EmptyResponse?> DeleteChannelAsync(DeleteChannelCommandRequest requestModel);
    }
}
