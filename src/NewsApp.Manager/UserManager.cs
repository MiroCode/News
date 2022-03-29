using MediatR;
using NewsApp.Infrastructure.CQRS.Commands.Request;
using NewsApp.Infrastructure.CQRS.Commands.Response;
using NewsApp.Infrastructure.CQRS.Common;
using NewsApp.Infrastructure.CQRS.Queries.Request;
using NewsApp.Infrastructure.CQRS.Queries.Response;
using NewsApp.Manager.Abstraction;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewsApp.Manager
{
    public class UserManager : IUserManager
    {
        private readonly IMediator _mediator;
        public UserManager(IMediator mediator
            )
        {
            _mediator = mediator;
        }
        public async Task<CreateUserCommandResponse> CreateUserAsync(CreateUserCommandRequest requestModel)
        {
            return await _mediator.Send(requestModel);
        }

        public async Task<EmptyResponse> DeleteUserAsync(DeleteUserCommandRequest requestModel)
        {
            return await _mediator.Send(requestModel);
        }

        public async Task<IEnumerable<ListUserQueryResponse>> GetAllUserAsync(ListUserQueryRequest requestModel)
        {
            return await _mediator.Send(requestModel);
        }

        public async Task<UserQueryResponse> GetUserAsync(GetUserQueryRequest requestModel)
        {
            return await _mediator.Send(requestModel);
        }

        public async Task<EmptyResponse> UpdateUserAsync(UpdateUserCommandRequest requestModel)
        {
            return await _mediator.Send(requestModel);
        }
    }
}
