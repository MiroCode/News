using NewsApp.Infrastructure.CQRS.Commands.Request;
using NewsApp.Infrastructure.CQRS.Commands.Response;
using NewsApp.Infrastructure.CQRS.Common;
using NewsApp.Infrastructure.CQRS.Queries.Request;
using NewsApp.Infrastructure.CQRS.Queries.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewsApp.Manager.Abstraction
{
    public interface IUserManager
    {
        Task<IEnumerable<ListUserQueryResponse>> GetAllUserAsync(ListUserQueryRequest requestModel);
        Task<UserQueryResponse> GetUserAsync(GetUserQueryRequest requestModel);
        Task<CreateUserCommandResponse> CreateUserAsync(CreateUserCommandRequest requestModel);
        Task<EmptyResponse?> UpdateUserAsync(UpdateUserCommandRequest requestModel);
        Task<EmptyResponse?> DeleteUserAsync(DeleteUserCommandRequest requestModel);
    }
}
