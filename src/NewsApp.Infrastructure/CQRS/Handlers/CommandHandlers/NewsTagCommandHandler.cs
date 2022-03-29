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
    public class NewsTagCommandHandler : IRequestHandler<CreateTagCommandRequest, CreateTagCommandResponse>, IRequestHandler<DeleteTagCommandRequest, EmptyResponse>, IRequestHandler<UpdateTagCommandRequest, EmptyResponse>
    {
        private readonly MongoDBContext _context;
        private readonly IMapper _mapper;
        private readonly IRedisCacheClient _redisCache;
        public NewsTagCommandHandler(MongoDBContext context, IMapper mapper, IRedisCacheClient redisCache)
        {
            _context = context;
            _mapper = mapper;
            _redisCache = redisCache;
        }
        public async Task<CreateTagCommandResponse> Handle(CreateTagCommandRequest request, CancellationToken cancellationToken)
        {
            var tag = _mapper.Map<NewsTag>(request);
            tag.CreatedDate = DateTime.Now;
            tag.UpdatedDate = DateTime.Now;

            await _context.NewsTag.InsertOneAsync(tag, cancellationToken: cancellationToken);
            await _redisCache.Db0.RemoveAsync("newstag");

            return new CreateTagCommandResponse
            {
                Id = tag.Id
            };
        }
        public async Task<EmptyResponse> Handle(DeleteTagCommandRequest request, CancellationToken cancellationToken)
        {
            var filter = Builders<NewsTag>.Filter.Eq("Id", request.Id);
            var result = await _context.NewsTag.DeleteOneAsync(filter, cancellationToken);

            await _redisCache.Db0.RemoveAllAsync(new[] { "newstag", $"newstag_{request.Id}" });

            return result.DeletedCount == 0 ? null : EmptyResponse.Default;
        }
        public async Task<EmptyResponse> Handle(UpdateTagCommandRequest request, CancellationToken cancellationToken)
        {
            var filter = Builders<NewsTag>.Filter.Eq("Id", request.Id);
            var update = Builders<NewsTag>.Update
                .Set("Name", request.Name)
                .Set("DisplayOrder", request.DisplayOrder)
                .Set("UpdatedDate", DateTime.Now);

            var result = await _context.NewsTag.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);
            await _redisCache.Db0.RemoveAllAsync(new[] { "newstag", $"newstag_{request.Id}" });

            return result.ModifiedCount == 0 ? null : EmptyResponse.Default;
        }
    }
}
