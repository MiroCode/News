using NewsApp.Infrastructure.CQRS.Commands.Request;
using NewsApp.Infrastructure.CQRS.Commands.Response;
using NewsApp.Infrastructure.CQRS.Common;
using NewsApp.Infrastructure.CQRS.Queries.Request;
using NewsApp.Infrastructure.CQRS.Queries.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewsApp.Manager.Abstraction
{
    public interface ITagManager
    {
        Task<IEnumerable<ListTagQueryResponse>> GetAllTagAsync(ListTagQueryRequest requestModel);
        Task<TagQueryResponse> GetTagAsync(GetTagQueryRequest requestModel);
        Task<CreateTagCommandResponse> CreateTagAsync(CreateTagCommandRequest requestModel);
        Task<EmptyResponse?> UpdateTagAsync(UpdateTagCommandRequest requestModel);
        Task<EmptyResponse?> DeleteTagAsync(DeleteTagCommandRequest requestModel);
    }
}
