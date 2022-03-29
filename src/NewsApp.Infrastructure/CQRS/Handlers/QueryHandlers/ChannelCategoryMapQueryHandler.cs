using AutoMapper;
using MediatR;
using MongoDB.Driver;
using NewsApp.Infrastructure.CQRS.Queries.Request;
using NewsApp.Infrastructure.CQRS.Queries.Response;
using NewsApp.Infrastructure.Models;
using StackExchange.Redis.Extensions.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NewsApp.Infrastructure.CQRS.Handlers.QueryHandlers
{
    public class ChannelCategoryMapQueryHandler : IRequestHandler<GetChannelCategoryMapQueryRequest, ChannelCategoryMapQueryResponse>,
      IRequestHandler<ListChannelCategoryMapQueryRequest, IEnumerable<ListChannelCategoryMapQueryResponse>>
    {
        private readonly MongoDBContext _context;
        private readonly IMapper _mapper;
        private readonly IRedisCacheClient _redisCache;
        public ChannelCategoryMapQueryHandler(MongoDBContext context, IMapper mapper, IRedisCacheClient redisCache)
        {
            _context = context;
            _mapper = mapper;
            _redisCache = redisCache;
        }
        public async Task<ChannelCategoryMapQueryResponse> Handle(GetChannelCategoryMapQueryRequest request, CancellationToken cancellationToken)
        {
            var cacheKey = $"channelcategorymap_{request.Id}";
            var cachedData = await _redisCache.Db0.GetAsync<ChannelCategoryMapQueryResponse>(cacheKey);
            if (cachedData != null)
            {
                return cachedData;
            }
            var channelcategorymap = await _context
                .ChannelCategoryMap
                .Find(x => x.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);
            var result = _mapper.Map<ChannelCategoryMapQueryResponse>(channelcategorymap);
            await _redisCache.Db0.AddAsync(cacheKey, result, TimeSpan.FromMinutes(5));

            return result;
        }

        public async Task<IEnumerable<ListChannelCategoryMapQueryResponse>> Handle(ListChannelCategoryMapQueryRequest request, CancellationToken cancellationToken)
        {
            var isCacheable = false;
            const string cacheKey = "channelcategorymap";
            IFindFluent<ChannelCategoryMap, ChannelCategoryMap>? query;

            var cachedData = await _redisCache.Db0.GetAsync<IEnumerable<ListChannelCategoryMapQueryResponse>>(cacheKey);
            if (cachedData != null)
                return cachedData;
            else
            {
                query = _context.ChannelCategoryMap.Find(x => true);

                var channelCategoryMaps = await query.ToListAsync(cancellationToken);

                if (!string.IsNullOrEmpty(request.ChannelId))
                {
                    if (!string.IsNullOrEmpty(request.CategoryId))
                    {
                        channelCategoryMaps = channelCategoryMaps.Where(x => x.ChannelId == request.ChannelId && x.CategoryId == request.CategoryId).ToList();
                    }
                    else
                    {
                        channelCategoryMaps = channelCategoryMaps.Where(x => x.ChannelId == request.ChannelId).ToList();
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(request.CategoryId))
                    {
                        channelCategoryMaps = channelCategoryMaps.Where(x => x.CategoryId == request.CategoryId).ToList();
                    }
                }

                var result = _mapper.Map<IEnumerable<ListChannelCategoryMapQueryResponse>>(channelCategoryMaps);

                if (isCacheable)
                    await _redisCache.Db0.AddAsync(cacheKey, result, TimeSpan.FromMinutes(5));

                return result;
            }
        }
    }
}
