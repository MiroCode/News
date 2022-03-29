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
    public class NewsImageQueryHandler : IRequestHandler<GetNewsImageQueryRequest, NewsImageQueryResponse>
        , IRequestHandler<ListNewsImageQueryRequest, IEnumerable<ListNewsImageQueryResponse>>
    {
        private readonly MongoDBContext _context;
        private readonly IMapper _mapper;
        private readonly IRedisCacheClient _redisCache;

        public NewsImageQueryHandler(MongoDBContext context, IMapper mapper, IRedisCacheClient redisCache)
        {
            _context = context;
            _mapper = mapper;
            _redisCache = redisCache;
        }

        public async Task<NewsImageQueryResponse> Handle(GetNewsImageQueryRequest request, CancellationToken cancellationToken)
        {
            var cacheKey = $"newsimage_{request.Id}";
            var cachedData = await _redisCache.Db0.GetAsync<NewsImageQueryResponse>(cacheKey);
            if (cachedData != null)
                return cachedData;

            var news = await _context
                .NewsImage
                .Find(x => x.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);
            var result = _mapper.Map<NewsImageQueryResponse>(news);
            await _redisCache.Db0.AddAsync(cacheKey, result, TimeSpan.FromMinutes(5));

            return result;
        }

        public async Task<IEnumerable<ListNewsImageQueryResponse>> Handle(ListNewsImageQueryRequest request, CancellationToken cancellationToken)
        {
            var isCacheable = false;
            string cacheKey = $"newsimage";
            IFindFluent<NewsImage, NewsImage>? query;

            if (string.IsNullOrEmpty(request.NewsId))
            {
                var cachedData = await _redisCache.Db0.GetAsync<IEnumerable<ListNewsImageQueryResponse>>(cacheKey);
                if (cachedData != null)
                    return cachedData;

                query = _context.NewsImage.Find(x => true);
                isCacheable = true;
            }
            else
            {
                query = _context.NewsImage.Find(x => x.NewsId != null && x.NewsId.Contains(request.NewsId));
            }

            var newsImage = await query.ToListAsync(cancellationToken);
            var result = _mapper.Map<IEnumerable<ListNewsImageQueryResponse>>(newsImage);

            if (isCacheable)
                await _redisCache.Db0.AddAsync(cacheKey, result, TimeSpan.FromMinutes(5));

            return result;
        }
    }
}
