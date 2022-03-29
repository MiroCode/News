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
    public class TagQueryHandler : IRequestHandler<GetTagQueryRequest, TagQueryResponse>
        , IRequestHandler<ListTagQueryRequest, IEnumerable<ListTagQueryResponse>>
    {
        private readonly MongoDBContext _context;
        private readonly IMapper _mapper;
        private readonly IRedisCacheClient _redisCache;

        public TagQueryHandler(MongoDBContext context, IMapper mapper, IRedisCacheClient redisCache)
        {
            _context = context;
            _mapper = mapper;
            _redisCache = redisCache;
        }

        public async Task<TagQueryResponse> Handle(GetTagQueryRequest request, CancellationToken cancellationToken)
        {
            var cacheKey = $"newstag_{request.Id}";
            var cachedData = await _redisCache.Db0.GetAsync<TagQueryResponse>(cacheKey);
            if (cachedData != null)
                return cachedData;

            var tag = await _context
                .NewsTag
                .Find(x => x.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);

            var result = _mapper.Map<TagQueryResponse>(tag);

            await _redisCache.Db0.AddAsync(cacheKey, result, TimeSpan.FromMinutes(5));

            return result;
        }

        public async Task<IEnumerable<ListTagQueryResponse>> Handle(ListTagQueryRequest request, CancellationToken cancellationToken)
        {
            var isCacheable = false;
            string cacheKey = "newstag";
            IFindFluent<NewsTag, NewsTag>? query;

            if (string.IsNullOrEmpty(request.Name))
            {
                var cachedData = await _redisCache.Db0.GetAsync<IEnumerable<ListTagQueryResponse>>(cacheKey);
                if (cachedData != null)
                    return cachedData;

                query = _context.NewsTag.Find(x => true);
                isCacheable = true;
            }
            else
            {
                query = _context.NewsTag.Find(x => x.Name != null && x.Name.ToLower().Contains(request.Name.ToLower()));
            }

            var tags = await query.ToListAsync(cancellationToken);
            var result = _mapper.Map<IEnumerable<ListTagQueryResponse>>(tags);

            if (isCacheable)
                await _redisCache.Db0.AddAsync(cacheKey, result, TimeSpan.FromMinutes(5));

            return result;
        }
    }
}
