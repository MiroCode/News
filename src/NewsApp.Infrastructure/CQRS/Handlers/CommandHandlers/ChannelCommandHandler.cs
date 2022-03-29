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

    public class ChannelCommandHandler : IRequestHandler<CreateChannelCommandRequest, CreateChannelCommandResponse>,
        IRequestHandler<DeleteChannelCommandRequest, EmptyResponse?>,
        IRequestHandler<UpdateChannelCommandRequest, EmptyResponse?>
    {
        private readonly MongoDBContext _context;
        private readonly IMapper _mapper;
        private readonly IRedisCacheClient _redisCache;

        public ChannelCommandHandler(MongoDBContext context, IMapper mapper, IRedisCacheClient redisCache)
        {
            _context = context;
            _mapper = mapper;
            _redisCache = redisCache;
        }

        public async Task<CreateChannelCommandResponse> Handle(CreateChannelCommandRequest request,
            CancellationToken cancellationToken)
        {
            var channel = _mapper.Map<Channel>(request);
            channel.CreatedDate = DateTime.Now;
            channel.UpdatedDate = DateTime.Now;

            await _context.Channel.InsertOneAsync(channel, cancellationToken: cancellationToken);
            await _redisCache.Db0.RemoveAsync("channel");

            return new CreateChannelCommandResponse
            {
                Id = channel.Id
            };
        }
        public async Task<EmptyResponse?> Handle(DeleteChannelCommandRequest request,
           CancellationToken cancellationToken)
        {
            var filter = Builders<Channel>.Filter.Eq("Id", request.Id);
            var result = await _context.Channel.DeleteOneAsync(filter, cancellationToken);
            await _redisCache.Db0.RemoveAllAsync(new[] { "channel", $"channel_{request.Id}" });
            return result.DeletedCount == 0 ? null : EmptyResponse.Default;
        }
        public async Task<EmptyResponse?> Handle(UpdateChannelCommandRequest request,
            CancellationToken cancellationToken)
        {
            var filter = Builders<Channel>.Filter.Eq("Id", request.Id);
            var update = Builders<Channel>.Update
                .Set("Name", request.Name)
                .Set("Url", request.Url)
                .Set("ImagePath", request.ImagePath)
                .Set("DisplayOrder", request.DisplayOrder)
                .Set("UpdatedDate", DateTime.Now);
            var result = await _context.Channel.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);
            await _redisCache.Db0.RemoveAllAsync(new[] { "channel", $"channel_{request.Id}" });
            return result.ModifiedCount == 0 ? null : EmptyResponse.Default;
        }
    }
}