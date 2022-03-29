using NewsApp.Infrastructure.CQRS.Commands.Request;
using NewsApp.Infrastructure.CQRS.Commands.Response;
using NewsApp.Infrastructure.CQRS.Common;
using NewsApp.Infrastructure.CQRS.Queries.Request;
using NewsApp.Infrastructure.CQRS.Queries.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewsApp.Manager.Abstraction
{
    public interface ICategoryManager
    {
        Task<IEnumerable<ListCategoryQueryResponse>> GetAllCategoryAsync(ListCategoryQueryRequest requestModel);
        Task<CategoryQueryResponse> GetCategoryAsync(GetCategoryQueryRequest requestModel);
        Task<CreateCategoryCommandResponse> CreateCategoryAsync(CreateCategoryCommandRequest requestModel);
        Task<EmptyResponse?> UpdateCategoryAsync(UpdateCategoryCommandRequest requestModel);
        Task<EmptyResponse?> DeleteCategoryAsync(DeleteCategoryCommandRequest requestModel);
    }
}
