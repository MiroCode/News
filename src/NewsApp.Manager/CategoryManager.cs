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
    public class CategoryManager : ICategoryManager
    {
        private readonly IMediator _mediator;
        public CategoryManager(IMediator mediator
            )
        {
            _mediator = mediator;
        }

        public async Task<CreateCategoryCommandResponse> CreateCategoryAsync(CreateCategoryCommandRequest requestModel)
        {
            return await _mediator.Send(requestModel);
        }
        public async Task<IEnumerable<ListCategoryQueryResponse>> GetAllCategoryAsync(ListCategoryQueryRequest requestModel)
        {
            return await _mediator.Send(requestModel);
        }

        public async Task<CategoryQueryResponse> GetCategoryAsync(GetCategoryQueryRequest requestModel)
        {
            return await _mediator.Send(requestModel);
        }

        public async Task<EmptyResponse?> UpdateCategoryAsync(UpdateCategoryCommandRequest requestModel)
        {
            return await _mediator.Send(requestModel);
        }
        public async Task<EmptyResponse?> DeleteCategoryAsync(DeleteCategoryCommandRequest requestModel)
        {
            return await _mediator.Send(requestModel);
        }
    }
}
