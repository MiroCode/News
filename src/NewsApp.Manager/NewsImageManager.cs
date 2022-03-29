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
    public class NewsImageManager : INewsImageManager
    {
        private readonly IMediator _mediator;
        public NewsImageManager(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<CreateNewsImageCommandResponse> CreateNewsImageAsync(CreateNewsImageCommandRequest requestModel)
        {
            return await _mediator.Send(requestModel);
        }

        public async Task<EmptyResponse> DeleteNewsImageAsync(DeleteNewsImageCommandRequest requestModel)
        {
            return await _mediator.Send(requestModel);
        }

        public async Task<IEnumerable<ListNewsImageQueryResponse>> GetAllNewsImageAsync(ListNewsImageQueryRequest requestModel)
        {
            return await _mediator.Send(requestModel);
        }

        public async Task<NewsImageQueryResponse> GetNewsImageAsync(GetNewsImageQueryRequest requestModel)
        {
            return await _mediator.Send(requestModel);
        }

        public async Task<EmptyResponse> UpdateNewsImageAsync(UpdateNewsImageCommandRequest requestModel)
        {
            return await _mediator.Send(requestModel);
        }
    }
}
