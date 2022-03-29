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
    public class ChannelCategoryMapManager : IChannelCategoryMapManager
    {
        private readonly IMediator _mediator;
        public ChannelCategoryMapManager(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<CreateChannelCategoryMapCommandResponse> CreateChannelCategoryMapAsync(CreateChannelCategoryMapCommandRequest requestModel)
        {
            return await _mediator.Send(requestModel);
        }

        public async Task<EmptyResponse> DeleteChannelCategoryMapAsync(DeleteChannelCategoryMapCommandRequest requestModel)
        {
            return await _mediator.Send(requestModel);
        }

        public async Task<IEnumerable<ListChannelCategoryMapQueryResponse>> GetAllChannelCategoryMapAsync(ListChannelCategoryMapQueryRequest requestModel)
        {
            return await _mediator.Send(requestModel);
        }

        public async Task<ChannelCategoryMapQueryResponse> GetChannelCategoryMapAsync(GetChannelCategoryMapQueryRequest requestModel)
        {
            return await _mediator.Send(requestModel);
        }

        public async Task<EmptyResponse> UpdateChannelCategoryMapAsync(UpdateChannelCategoryMapCommandRequest requestModel)
        {
            return await _mediator.Send(requestModel);
        }
    }
}
