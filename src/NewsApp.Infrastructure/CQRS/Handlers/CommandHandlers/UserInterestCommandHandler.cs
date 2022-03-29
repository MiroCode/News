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
    public class UserInterestCommandHandler :
        IRequestHandler<CreateUserInterestCommandRequest, CreateUserInterestCommandResponse>,
        IRequestHandler<UpdateUserInterestCommandRequest, EmptyResponse>,
        IRequestHandler<DeleteUserInterestCommandRequest, EmptyResponse>
    {
        private readonly MongoDBContext _context;
        private readonly IMapper _mapper;
        private readonly IRedisCacheClient _redisCache;
        public UserInterestCommandHandler(MongoDBContext context, IMapper mapper, IRedisCacheClient redisCache)
        {
            _context = context;
            _mapper = mapper;
            _redisCache = redisCache;
        }

        public async Task<CreateUserInterestCommandResponse> Handle(CreateUserInterestCommandRequest request, CancellationToken cancellationToken)
        {
            var userinterest = _mapper.Map<UserInterest>(request);
            userinterest.CreatedDate = DateTime.Now;
            userinterest.UpdatedDate = DateTime.Now;
            await _context.UserInterest.InsertOneAsync(userinterest, cancellationToken);
            await _redisCache.Db0.RemoveAsync("userinterest");

            return new CreateUserInterestCommandResponse
            {
                Id = userinterest.Id
            };
        }

        public async Task<EmptyResponse> Handle(UpdateUserInterestCommandRequest request, CancellationToken cancellationToken)
        {
            var filter = Builders<UserInterest>.Filter.Eq("Id", request.Id);
            var update = Builders<UserInterest>.Update
                .Set("ChannelCategoryMapId", request.ChannelCategoryMapId)
                .Set("UserId", request.UserId)
                .Set("UpdatedDate", DateTime.Now);

            var result = await _context.UserInterest.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);
            await _redisCache.Db0.RemoveAllAsync(new[] { "userinterest", $"userinterest_{request.Id}" });

            return result.ModifiedCount == 0 ? null : EmptyResponse.Default;
        }

        public async Task<EmptyResponse> Handle(DeleteUserInterestCommandRequest request, CancellationToken cancellationToken)
        {
            var filter = Builders<UserInterest>.Filter.Eq("Id", request.Id);
            var result = await _context.UserInterest.DeleteOneAsync(filter, cancellationToken);

            await _redisCache.Db0.RemoveAllAsync(new[] { "userinterest", $"userinterest_{request.Id}" });

            return result.DeletedCount == 0 ? null : EmptyResponse.Default;
        }
    }
}
