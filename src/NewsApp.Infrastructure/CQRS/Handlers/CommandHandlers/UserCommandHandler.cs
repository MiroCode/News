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
    public class UserCommandHandler :
        IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>,
        IRequestHandler<UpdateUserCommandRequest, EmptyResponse>,
        IRequestHandler<DeleteUserCommandRequest, EmptyResponse>
    {
        private readonly MongoDBContext _context;
        private readonly IMapper _mapper;
        private readonly IRedisCacheClient _redisCache;

        public UserCommandHandler(MongoDBContext context, IMapper mapper, IRedisCacheClient redisCache)
        {
            _context = context;
            _mapper = mapper;
            _redisCache = redisCache;
        }
        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request,
            CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(request);
            user.CreatedDate = DateTime.Now;
            user.UpdatedDate = DateTime.Now;
            await _context.User.InsertOneAsync(user, cancellationToken);
            await _redisCache.Db0.RemoveAsync("user");

            return new CreateUserCommandResponse
            {
                Id = user.Id
            };
        }
        public async Task<EmptyResponse> Handle(UpdateUserCommandRequest request, CancellationToken cancellationToken)
        {
            var filter = Builders<User>.Filter.Eq("Id", request.Id);
            var update = Builders<User>.Update
                .Set("Email", request.Email)
                .Set("AppId", request.AppId)
                .Set("UpdatedDate", DateTime.Now);

            var result = await _context.User.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);
            await _redisCache.Db0.RemoveAllAsync(new[] { "user", $"user_{request.Id}" });

            return result.ModifiedCount == 0 ? null : EmptyResponse.Default;
        }
        public async Task<EmptyResponse> Handle(DeleteUserCommandRequest request,
          CancellationToken cancellationToken)
        {
            var filter = Builders<User>.Filter.Eq("Id", request.Id);
            var result = await _context.User.DeleteOneAsync(filter, cancellationToken);

            await _redisCache.Db0.RemoveAllAsync(new[] { "user", $"user_{request.Id}" });

            return result.DeletedCount == 0 ? null : EmptyResponse.Default;
        }
    }
}
