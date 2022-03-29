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
    public class NewsNewsTagMapManager : INewsNewsTagMapManager
    {
        private readonly IMediator _mediator;
        public NewsNewsTagMapManager(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<CreateNewsNewsTagMapCommandResponse> CreateNewsNewsTagMapAsync(CreateNewsNewsTagMapCommandRequest requestModel)
        {
            return await _mediator.Send(requestModel);
        }

        public async Task<EmptyResponse> DeleteNewsNewsTagMapAsync(DeleteNewsNewsTagMapCommandRequest requestModel)
        {
            return await _mediator.Send(requestModel);
        }

        public async Task<IEnumerable<ListNewsNewsTagMapQueryResponse>> GetAllNewsNewsTagMapAsync(ListNewsNewsTagMapQueryRequest requestModel)
        {
            return await _mediator.Send(requestModel);
        }

        public async Task<NewsNewsTagMapQueryResponse> GetNewsNewsTagMapAsync(GetNewsNewsTagMapQueryRequest requestModel)
        {
            return await _mediator.Send(requestModel);
        }

        public async Task<EmptyResponse> UpdateNewsNewsTagMapAsync(UpdateNewsNewsTagMapCommandRequest requestModel)
        {
            return await _mediator.Send(requestModel);
        }
    }
}
