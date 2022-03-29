using AutoMapper;
using MediatR;
using MongoDB.Driver;
using NewsApp.Infrastructure.CQRS.Commands.Request;
using NewsApp.Infrastructure.CQRS.Commands.Response;
using NewsApp.Infrastructure.CQRS.Common;
using NewsApp.Infrastructure.Models;
using StackExchange.Redis.Extensions.Core.Abstractions;
using System;
using System.Threading;
using System.Threading.Tasks;
namespace NewsApp.Infrastructure.CQRS.Handlers.CommandHandlers
{
    public class CategoryCommandHandler :
        IRequestHandler<CreateCategoryCommandRequest, CreateCategoryCommandResponse?>,
        IRequestHandler<DeleteCategoryCommandRequest, EmptyResponse?>,
        IRequestHandler<UpdateCategoryCommandRequest, EmptyResponse?>
    {
        private readonly MongoDBContext _context;
        private readonly IMapper _mapper;
        private readonly IRedisCacheClient _redisCache;

        public CategoryCommandHandler(MongoDBContext context, IMapper mapper, IRedisCacheClient redisCache)
        {
            _context = context;
            _mapper = mapper;
            _redisCache = redisCache;
        }

        public async Task<CreateCategoryCommandResponse?> Handle(CreateCategoryCommandRequest request,
            CancellationToken cancellationToken)
        {
            var category = _mapper.Map<Category>(request);
            category.CreatedDate = DateTime.Now;
            category.UpdatedDate = DateTime.Now;

            var isCategoryExists = await _context.Category.CountDocumentsAsync(x => x.Name == request.Name,
                cancellationToken: cancellationToken) > 0;

            if (isCategoryExists)
                return null;

            await _context.Category.InsertOneAsync(category, cancellationToken: cancellationToken);
            await _redisCache.Db0.RemoveAsync("category");

            return new CreateCategoryCommandResponse
            {
                Id = category.Id
            };
        }
        public async Task<EmptyResponse?> Handle(DeleteCategoryCommandRequest request,
          CancellationToken cancellationToken)
        {
            var filter = Builders<Category>.Filter.Eq("Id", request.Id);
            var result = await _context.Category.DeleteOneAsync(filter, cancellationToken);

            await _redisCache.Db0.RemoveAllAsync(new[] { "category", $"category_{request.Id}" });
            //result.DeletedCount == 0 ? null :
            return EmptyResponse.Default;
        }

        public async Task<EmptyResponse?> Handle(UpdateCategoryCommandRequest request,
           CancellationToken cancellationToken)
        {
            var isCategoryExists = await _context.Category.CountDocumentsAsync(x => x.Id == request.Id,
                cancellationToken: cancellationToken) > 0;

            if (!isCategoryExists)
                return null;

            var filter = Builders<Category>.Filter.Eq("Id", request.Id);
            var update = Builders<Category>.Update
                .Set("Name", request.Name)
                .Set("DisplayOrder", request.DisplayOrder)
                .Set("UpdatedDate", DateTime.Now);


            var result = await _context.Category.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);
            await _redisCache.Db0.RemoveAllAsync(new[] { "category", $"category_{request.Id}" });

            return result.ModifiedCount == 0 ? null : EmptyResponse.Default;
        }
    }
}