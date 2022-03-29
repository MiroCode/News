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
    public class UserInterestManager : IUserInterestManager
    {
        private readonly IMediator _mediator;
        public UserInterestManager(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<CreateUserInterestCommandResponse> CreateUserInterestAsync(CreateUserInterestCommandRequest requestModel)
        {
            return await _mediator.Send(requestModel);
        }

        public async Task<EmptyResponse> DeleteUserInterestAsync(DeleteUserInterestCommandRequest requestModel)
        {
            return await _mediator.Send(requestModel);
        }

        public async Task<IEnumerable<ListUserInterestQueryResponse>> GetAllUserInterestAsync(ListUserInterestQueryRequest requestModel)
        {
            return await _mediator.Send(requestModel);
        }

        public async Task<UserInterestQueryResponse> GetUserInterestAsync(GetUserInterestQueryRequest requestModel)
        {
            return await _mediator.Send(requestModel);
        }

        public async Task<EmptyResponse> UpdateUserInterestAsync(UpdateUserInterestCommandRequest requestModel)
        {
            return await _mediator.Send(requestModel);
        }
    }
}
