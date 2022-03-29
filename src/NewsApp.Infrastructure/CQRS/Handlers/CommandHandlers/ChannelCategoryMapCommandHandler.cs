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
    public class ChannelCategoryMapCommandHandler :
      IRequestHandler<CreateChannelCategoryMapCommandRequest, CreateChannelCategoryMapCommandResponse?>,
      IRequestHandler<DeleteChannelCategoryMapCommandRequest, EmptyResponse?>,
      IRequestHandler<UpdateChannelCategoryMapCommandRequest, EmptyResponse?>
    {
        private readonly MongoDBContext _context;
        private readonly IMapper _mapper;
        private readonly IRedisCacheClient _redisCache;
        public ChannelCategoryMapCommandHandler(MongoDBContext context, IMapper mapper, IRedisCacheClient redisCache)
        {
            _context = context;
            _mapper = mapper;
            _redisCache = redisCache;
        }
        public async Task<CreateChannelCategoryMapCommandResponse> Handle(CreateChannelCategoryMapCommandRequest request, CancellationToken cancellationToken)
        {
            var channelcategorymap = _mapper.Map<ChannelCategoryMap>(request);
            channelcategorymap.CreatedDate = System.DateTime.Now;
            channelcategorymap.UpdatedDate = System.DateTime.Now;
            await _context.ChannelCategoryMap.InsertOneAsync(channelcategorymap, cancellationToken: cancellationToken);
            await _redisCache.Db0.RemoveAsync("channelcategorymap");

            return new CreateChannelCategoryMapCommandResponse
            {
                Id = channelcategorymap.Id
            };
        }

        public async Task<EmptyResponse> Handle(DeleteChannelCategoryMapCommandRequest request, CancellationToken cancellationToken)
        {
            var filter = Builders<ChannelCategoryMap>.Filter.Eq("Id", request.Id);
            var result = await _context.ChannelCategoryMap.DeleteOneAsync(filter, cancellationToken);
            await _redisCache.Db0.RemoveAllAsync(new[] { "channelcategorymap", $"channelcategorymap_{request.Id}" });
            return result.DeletedCount == 0 ? null : EmptyResponse.Default;
        }

        public async Task<EmptyResponse> Handle(UpdateChannelCategoryMapCommandRequest request, CancellationToken cancellationToken)
        {
            var filter = Builders<ChannelCategoryMap>.Filter.Eq("Id", request.Id);
            var update = Builders<ChannelCategoryMap>.Update
                .Set("CategoryId", request.CategoryId)
                .Set("ChannelId", request.ChannelId)
                .Set("XmlPath", request.XmlPath)
               .Set("UpdatedDate", DateTime.Now);
            var result = await _context.ChannelCategoryMap.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);
            await _redisCache.Db0.RemoveAllAsync(new[] { "channelcategorymap", $"channelcategorymap_{request.Id}" });

            return result.ModifiedCount == 0 ? null : EmptyResponse.Default;
        }
    }
}
