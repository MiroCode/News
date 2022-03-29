using NewsApp.Infrastructure.CQRS.Commands.Request;
using NewsApp.Infrastructure.CQRS.Commands.Response;
using NewsApp.Infrastructure.CQRS.Common;
using NewsApp.Infrastructure.CQRS.Queries.Request;
using NewsApp.Infrastructure.CQRS.Queries.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewsApp.Manager.Abstraction
{
    public interface IUserInterestManager
    {
        Task<IEnumerable<ListUserInterestQueryResponse>> GetAllUserInterestAsync(ListUserInterestQueryRequest requestModel);
        Task<UserInterestQueryResponse> GetUserInterestAsync(GetUserInterestQueryRequest requestModel);
        Task<CreateUserInterestCommandResponse> CreateUserInterestAsync(CreateUserInterestCommandRequest requestModel);
        Task<EmptyResponse?> UpdateUserInterestAsync(UpdateUserInterestCommandRequest requestModel);
        Task<EmptyResponse?> DeleteUserInterestAsync(DeleteUserInterestCommandRequest requestModel);
    }
}
