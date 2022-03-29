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
    public class ChannelManager : IChannelManager
    {
        private readonly IMediator _mediator;
        public ChannelManager(IMediator mediator
            )
        {
            _mediator = mediator;
        }

        public async Task<CreateChannelCommandResponse> CreateChannelAsync(CreateChannelCommandRequest requestModel)
        {
            return await _mediator.Send(requestModel);
        }

        public async Task<IEnumerable<ListChannelQueryResponse>> GetAllChannelAsync(ListChannelQueryRequest requestModel)
        {
            return await _mediator.Send(requestModel);
        }

        public async Task<ChannelQueryResponse> GetChannelAsync(GetChannelQueryRequest requestModel)
        {
            return await _mediator.Send(requestModel);
        }

        public async Task<EmptyResponse?> UpdateChannelAsync(UpdateChannelCommandRequest requestModel)
        {
            return await _mediator.Send(requestModel);
        }

        public async Task<EmptyResponse?> DeleteChannelAsync(DeleteChannelCommandRequest requestModel)
        {
            return await _mediator.Send(requestModel);
        }

    }
}
