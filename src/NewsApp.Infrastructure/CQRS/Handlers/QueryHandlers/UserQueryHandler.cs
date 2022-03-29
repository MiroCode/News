using AutoMapper;
using MediatR;
using MongoDB.Driver;
using NewsApp.Infrastructure.CQRS.Queries.Request;
using NewsApp.Infrastructure.CQRS.Queries.Response;
using NewsApp.Infrastructure.Models;
using StackExchange.Redis.Extensions.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NewsApp.Infrastructure.CQRS.Handlers.QueryHandlers
{
    public class UserQueryHandler : IRequestHandler<GetUserQueryRequest, UserQueryResponse>
        , IRequestHandler<ListUserQueryRequest, IEnumerable<ListUserQueryResponse>>
    {
        private readonly MongoDBContext _context;
        private readonly IMapper _mapper;
        private readonly IRedisCacheClient _redisCache;
        public UserQueryHandler(MongoDBContext context, IMapper mapper, IRedisCacheClient redisCache)
        {
            _context = context;
            _mapper = mapper;
            _redisCache = redisCache;
        }
        public async Task<UserQueryResponse> Handle(GetUserQueryRequest request, CancellationToken cancellationToken)
        {
            var cacheKey = $"user_{request.Id}";
            var cachedData = await _redisCache.Db0.GetAsync<UserQueryResponse>(cacheKey);
            if (cachedData != null)
                return cachedData;

            var user = await _context
                .User
                .Find(x => x.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);

            var result = _mapper.Map<UserQueryResponse>(user);

            await _redisCache.Db0.AddAsync(cacheKey, result, TimeSpan.FromMinutes(5));

            return result;
        }

        public async Task<IEnumerable<ListUserQueryResponse>> Handle(ListUserQueryRequest request, CancellationToken cancellationToken)
        {
            var isCacheable = false;
            string cacheKey = "user";
            IFindFluent<User, User>? query;

            if (string.IsNullOrEmpty(request.Email))
            {
                var cachedData = await _redisCache.Db0.GetAsync<IEnumerable<ListUserQueryResponse>>(cacheKey);
                if (cachedData != null)
                    return cachedData;

                query = _context.User.Find(x => true);
                isCacheable = true;
            }
            else
            {
                query = _context.User.Find(x => x.Email != null && x.Email.ToLower().Contains(request.Email.ToLower()));
            }

            var users = await query.ToListAsync(cancellationToken);
            var result = _mapper.Map<IEnumerable<ListUserQueryResponse>>(users);

            if (isCacheable)
                await _redisCache.Db0.AddAsync(cacheKey, result, TimeSpan.FromMinutes(5));

            return result;
        }
    }
}
