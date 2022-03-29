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
    public class TagManager : ITagManager
    {
        private readonly IMediator _mediator;
        public TagManager(IMediator mediator
            )
        {
            _mediator = mediator;
        }
        public async Task<CreateTagCommandResponse> CreateTagAsync(CreateTagCommandRequest requestModel)
        {
            return await _mediator.Send(requestModel);
        }

        public async Task<EmptyResponse> DeleteTagAsync(DeleteTagCommandRequest requestModel)
        {
            return await _mediator.Send(requestModel);
        }

        public async Task<IEnumerable<ListTagQueryResponse>> GetAllTagAsync(ListTagQueryRequest requestModel)
        {
            return await _mediator.Send(requestModel);
        }

        public async Task<TagQueryResponse> GetTagAsync(GetTagQueryRequest requestModel)
        {
            return await _mediator.Send(requestModel);
        }

        public async Task<EmptyResponse> UpdateTagAsync(UpdateTagCommandRequest requestModel)
        {
            return await _mediator.Send(requestModel);
        }
    }
}
