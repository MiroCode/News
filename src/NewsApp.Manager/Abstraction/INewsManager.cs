using NewsApp.Infrastructure.CQRS.Commands.Request;
using NewsApp.Infrastructure.CQRS.Commands.Response;
using NewsApp.Infrastructure.CQRS.Common;
using NewsApp.Infrastructure.CQRS.Queries.Request;
using NewsApp.Infrastructure.CQRS.Queries.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewsApp.Manager.Abstraction
{
    public interface INewsManager
    {
        Task<IEnumerable<ListNewsQueryResponse>> GetAllNewsAsync(ListNewsQueryRequest requestModel);
        Task<IEnumerable<ListNewsViewQueryResponse>> GetAllNewsViewAsync(ListNewsViewQueryRequest requestModel);
        Task<NewsQueryResponse> GetNewsAsync(GetNewsQueryRequest requestModel);
        Task<CreateNewsCommandResponse> CreateNewsAsync(CreateNewsCommandRequest requestModel);
        Task<CreateNewsViewCommandResponse> CreateNewsViewAsync(CreateNewsViewCommandRequest requestModel);
        Task<EmptyResponse?> UpdateNewsAsync(UpdateNewsCommandRequest requestModel);
        Task<EmptyResponse?> DeleteNewsAsync(DeleteNewsCommandRequest requestModel);
        Task<CreateNewsCommentNPointCommandResponse> CreateNewsCommentNPointAsync(CreateNewsCommentNPointCommandRequest requestModel);
        Task<IEnumerable<ListNewsCommentNPointQueryResponse>> GetAllNewsCommentNPointAsync(ListNewsCommentNPointQueryRequest requestModel);
    }
}
