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
    public class UserInterestQueryHandler : IRequestHandler<GetUserInterestQueryRequest, UserInterestQueryResponse>
        , IRequestHandler<ListUserInterestQueryRequest, IEnumerable<ListUserInterestQueryResponse>>
    {
        private readonly MongoDBContext _context;
        private readonly IMapper _mapper;
        private readonly IRedisCacheClient _redisCache;
        public UserInterestQueryHandler(MongoDBContext context, IMapper mapper, IRedisCacheClient redisCache)
        {
            _context = context;
            _mapper = mapper;
            _redisCache = redisCache;
        }
        public async Task<UserInterestQueryResponse> Handle(GetUserInterestQueryRequest request, CancellationToken cancellationToken)
        {
            var cacheKey = $"userinterest_{request.Id}";
            var cachedData = await _redisCache.Db0.GetAsync<UserInterestQueryResponse>(cacheKey);
            if (cachedData != null)
                return cachedData;

            var user = await _context
                .UserInterest
                .Find(x => x.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);

            var result = _mapper.Map<UserInterestQueryResponse>(user);

            await _redisCache.Db0.AddAsync(cacheKey, result, TimeSpan.FromMinutes(5));

            return result;
        }

        public async Task<IEnumerable<ListUserInterestQueryResponse>> Handle(ListUserInterestQueryRequest request, CancellationToken cancellationToken)
        {
            var isCacheable = false;
            string cacheKey = "userinterest";
            IFindFluent<UserInterest, UserInterest>? query;

            if (string.IsNullOrEmpty(request.UserId))
            {
                var cachedData = await _redisCache.Db0.GetAsync<IEnumerable<ListUserInterestQueryResponse>>(cacheKey);
                if (cachedData != null)
                    return cachedData;

                query = _context.UserInterest.Find(x => true);
                isCacheable = true;
            }
            else
            {
                query = _context.UserInterest.Find(x => x.UserId != null && x.UserId.ToLower().Contains(request.UserId.ToLower()));
            }

            var tags = await query.ToListAsync(cancellationToken);
            var result = _mapper.Map<IEnumerable<ListUserInterestQueryResponse>>(tags);

            if (isCacheable)
                await _redisCache.Db0.AddAsync(cacheKey, result, TimeSpan.FromMinutes(5));

            return result;
        }
    }
}
