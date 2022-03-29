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
    public class NewsNewsTagMapCommandHandler :
        IRequestHandler<CreateNewsNewsTagMapCommandRequest, CreateNewsNewsTagMapCommandResponse>,
        IRequestHandler<UpdateNewsNewsTagMapCommandRequest, EmptyResponse>,
        IRequestHandler<DeleteNewsNewsTagMapCommandRequest, EmptyResponse>
    {
        private readonly MongoDBContext _context;
        private readonly IMapper _mapper;
        private readonly IRedisCacheClient _redisCache;
        public NewsNewsTagMapCommandHandler(MongoDBContext context, IMapper mapper, IRedisCacheClient redisCache)
        {
            _context = context;
            _mapper = mapper;
            _redisCache = redisCache;
        }
        public async Task<CreateNewsNewsTagMapCommandResponse> Handle(CreateNewsNewsTagMapCommandRequest request, CancellationToken cancellationToken)
        {
            var newsnewstagmap = _mapper.Map<NewsNewsTagMap>(request);
            newsnewstagmap.CreatedDate = DateTime.Now;
            newsnewstagmap.UpdatedDate = DateTime.Now;

            await _context.NewsNewsTagMap.InsertOneAsync(newsnewstagmap, cancellationToken: cancellationToken);
            await _redisCache.Db0.RemoveAsync("newsnewstagmap");

            return new CreateNewsNewsTagMapCommandResponse
            {
                Id = newsnewstagmap.Id
            };
        }

        public async Task<EmptyResponse> Handle(UpdateNewsNewsTagMapCommandRequest request, CancellationToken cancellationToken)
        {
            var filter = Builders<NewsNewsTagMap>.Filter.Eq("Id", request.Id);
            var update = Builders<NewsNewsTagMap>.Update
                .Set("NewsId", request.NewsId)
                .Set("TagId", request.TagId)
                .Set("UpdatedDate", DateTime.Now);

            var result = await _context.NewsNewsTagMap.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);
            await _redisCache.Db0.RemoveAllAsync(new[] { "newsnewstagmap", $"newsnewstagmap_{request.Id}" });

            return result.ModifiedCount == 0 ? null : EmptyResponse.Default;
        }

        public async Task<EmptyResponse> Handle(DeleteNewsNewsTagMapCommandRequest request, CancellationToken cancellationToken)
        {
            var filter = Builders<NewsNewsTagMap>.Filter.Eq("Id", request.Id);
            if (!string.IsNullOrEmpty(request.Id))
                filter = Builders<NewsNewsTagMap>.Filter.Eq("Id", request.Id);
            if (!string.IsNullOrEmpty(request.NewsId))
                filter = Builders<NewsNewsTagMap>.Filter.Eq("NewsId", request.NewsId);
            var result = await _context.NewsNewsTagMap.DeleteOneAsync(filter, cancellationToken);

            await _redisCache.Db0.RemoveAllAsync(new[] { "newsnewstagmap", $"newsnewstagmap_{request.Id}" });

            return result.DeletedCount == 0 ? null : EmptyResponse.Default;
        }
    }
}
