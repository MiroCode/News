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
    public class SearchHistoryQueryHandler :
          IRequestHandler<GetSearchHistoryQueryRequest, SearchHistoryQueryResponse>,
          IRequestHandler<ListSearchHistoryQueryRequest, IEnumerable<ListSearchHistoryQueryResponse>>
    {
        private readonly MongoDBContext _context;
        private readonly IMapper _mapper;
        private readonly IRedisCacheClient _redisCache;

        public SearchHistoryQueryHandler(MongoDBContext context, IMapper mapper, IRedisCacheClient redisCache)
        {
            _context = context;
            _mapper = mapper;
            _redisCache = redisCache;
        }

        public async Task<SearchHistoryQueryResponse> Handle(GetSearchHistoryQueryRequest request,
            CancellationToken cancellationToken)
        {
            var cacheKey = $"searchHistory_{request.Id}";
            var cachedData = await _redisCache.Db0.GetAsync<SearchHistoryQueryResponse>(cacheKey);
            if (cachedData != null)
                return cachedData;

            var SearchHistory = await _context
                .SearchHistory
                .Find(x => x.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);
            var result = _mapper.Map<SearchHistoryQueryResponse>(SearchHistory);
            await _redisCache.Db0.AddAsync(cacheKey, result, TimeSpan.FromMinutes(5));

            return result;
        }

        public async Task<IEnumerable<ListSearchHistoryQueryResponse>> Handle(ListSearchHistoryQueryRequest request,
           CancellationToken cancellationToken)
        {
            var isCacheable = false;
            string cacheKey = "searchHistory";
            IFindFluent<SearchHistory, SearchHistory>? query;

            if (string.IsNullOrEmpty(request.SearchText))
            {
                var cachedData = await _redisCache.Db0.GetAsync<IEnumerable<ListSearchHistoryQueryResponse>>(cacheKey);
                if (cachedData != null)
                    return cachedData;

                query = _context.SearchHistory.Find(x => true);
                isCacheable = true;
            }
            else
            {
                query = _context.SearchHistory.Find(x => x.SearchText != null && x.SearchText.ToLower().Contains(request.SearchText.ToLower()));
            }

            var SearchHistory = await query.ToListAsync(cancellationToken);
            var result = _mapper.Map<IEnumerable<ListSearchHistoryQueryResponse>>(SearchHistory);

            if (isCacheable)
                await _redisCache.Db0.AddAsync(cacheKey, result, TimeSpan.FromMinutes(5));

            return result;
        }
    }
}
