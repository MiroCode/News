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
    public class NewsNewsTagMapQueryHandler :
        IRequestHandler<GetNewsNewsTagMapQueryRequest, NewsNewsTagMapQueryResponse>,
        IRequestHandler<ListNewsNewsTagMapQueryRequest, IEnumerable<ListNewsNewsTagMapQueryResponse>>
    {
        private readonly MongoDBContext _context;
        private readonly IMapper _mapper;
        private readonly IRedisCacheClient _redisCache;
        public NewsNewsTagMapQueryHandler(MongoDBContext context, IMapper mapper, IRedisCacheClient redisCache)
        {
            _context = context;
            _mapper = mapper;
            _redisCache = redisCache;
        }
        public async Task<NewsNewsTagMapQueryResponse> Handle(GetNewsNewsTagMapQueryRequest request, CancellationToken cancellationToken)
        {
            var cacheKey = $"newsnewstagmap_{request.Id}";
            var cachedData = await _redisCache.Db0.GetAsync<NewsNewsTagMapQueryResponse>(cacheKey);
            if (cachedData != null)
                return cachedData;

            var newsnewstagmap = await _context
                .NewsNewsTagMap
                .Find(x => x.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);
            var result = _mapper.Map<NewsNewsTagMapQueryResponse>(newsnewstagmap);
            await _redisCache.Db0.AddAsync(cacheKey, result, TimeSpan.FromMinutes(5));

            return result;
        }

        public async Task<IEnumerable<ListNewsNewsTagMapQueryResponse>> Handle(ListNewsNewsTagMapQueryRequest request, CancellationToken cancellationToken)
        {
            var isCacheable = false;
            string cacheKey = $"newsnewstagmap";
            IFindFluent<NewsNewsTagMap, NewsNewsTagMap>? query;

            if (!string.IsNullOrEmpty(request.NewsId))
            {
                query = _context.NewsNewsTagMap.Find(x => x.NewsId != null && x.NewsId.Contains(request.NewsId));
            }
            else
            {
                var cachedData = await _redisCache.Db0.GetAsync<IEnumerable<ListNewsNewsTagMapQueryResponse>>(cacheKey);
                if (cachedData != null)
                    return cachedData;

                query = _context.NewsNewsTagMap.Find(x => true);
                isCacheable = true;
            }

            var newsNewsTagMaps = await query.ToListAsync(cancellationToken);
            var result = _mapper.Map<IEnumerable<ListNewsNewsTagMapQueryResponse>>(newsNewsTagMaps);

            if (isCacheable)
                await _redisCache.Db0.AddAsync(cacheKey, result, TimeSpan.FromMinutes(5));

            return result;
        }
    }
}
