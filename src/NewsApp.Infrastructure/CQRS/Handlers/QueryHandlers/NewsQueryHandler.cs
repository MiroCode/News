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
    public class NewsQueryHandler : IRequestHandler<GetNewsQueryRequest, NewsQueryResponse>
        , IRequestHandler<ListNewsQueryRequest, IEnumerable<ListNewsQueryResponse>>
        , IRequestHandler<ListNewsViewQueryRequest, IEnumerable<ListNewsViewQueryResponse>>
        , IRequestHandler<ListNewsCommentNPointQueryRequest, IEnumerable<ListNewsCommentNPointQueryResponse>>
    {
        private readonly MongoDBContext _context;
        private readonly IMapper _mapper;
        private readonly IRedisCacheClient _redisCache;

        public NewsQueryHandler(MongoDBContext context, IMapper mapper, IRedisCacheClient redisCache)
        {
            _context = context;
            _mapper = mapper;
            _redisCache = redisCache;
        }

        public async Task<NewsQueryResponse> Handle(GetNewsQueryRequest request,
            CancellationToken cancellationToken)
        {
            var cacheKey = $"news_{request.Id}";
            var cachedData = await _redisCache.Db0.GetAsync<NewsQueryResponse>(cacheKey);
            if (cachedData != null)
                return cachedData;

            var news = await _context
                .News
                .Find(x => x.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);
            var result = _mapper.Map<NewsQueryResponse>(news);
            await _redisCache.Db0.AddAsync(cacheKey, result, TimeSpan.FromMinutes(5));

            return result;
        }

        public async Task<IEnumerable<ListNewsQueryResponse>> Handle(ListNewsQueryRequest request, CancellationToken cancellationToken)
        {
            var isCacheable = false;
            string cacheKey = "news";
            IFindFluent<News, News>? query;
            var cachedData = await _redisCache.Db0.GetAsync<IEnumerable<News>>(cacheKey);
            if (cachedData == null)
            {
                query = _context.News.Find(x => true);
                cachedData = await query.ToListAsync(cancellationToken);

                if (!string.IsNullOrEmpty(request.Title))
                {
                    cacheKey = cacheKey + $"_TT{request.Title}";
                    cachedData = cachedData.Where(x => x.Title.Contains(request.Title)).ToList();
                    isCacheable = true;
                }

                if (!string.IsNullOrEmpty(request.ProviderNewsId))
                {
                    cacheKey = cacheKey + $"_PN{request.ProviderNewsId}";

                    cachedData = cachedData.Where(x => x.ProviderNewsId == request.ProviderNewsId).ToList();
                    isCacheable = true;
                }

                if (request.ChannelCategoryMapIds.Any())
                {
                    cacheKey = cacheKey + $"_CC{string.Join('_', request.ChannelCategoryMapIds)}";

                    cachedData = cachedData.Where(x => request.ChannelCategoryMapIds.Contains(x.ChannelCategoryMapId)).ToList();
                    isCacheable = true;
                }
            }

            var cachedDataQuery = await _redisCache.Db0.GetAsync<IEnumerable<ListNewsQueryResponse>>(cacheKey);
            if (cachedDataQuery != null)
                return cachedDataQuery;

            var result = _mapper.Map<IEnumerable<ListNewsQueryResponse>>(cachedData);

            if (isCacheable)
                await _redisCache.Db0.AddAsync(cacheKey, result, TimeSpan.FromMinutes(5));

            return result;
        }


        public async Task<IEnumerable<ListNewsViewQueryResponse>> Handle(ListNewsViewQueryRequest request,
         CancellationToken cancellationToken)
        {
            var isCacheable = false;
            string cacheKey = "newsview";
            IFindFluent<NewsView, NewsView>? query;

            if (string.IsNullOrEmpty(request.NewsId))
            {
                var cachedData = await _redisCache.Db0.GetAsync<IEnumerable<ListNewsViewQueryResponse>>(cacheKey);
                if (cachedData != null)
                    return cachedData;

                query = _context.NewsView.Find(x => true);
                isCacheable = true;
            }
            else
            {
                query = _context.NewsView.Find(x => x.NewsId != null && x.NewsId.Contains(request.NewsId));
            }

            var newsview = _context.NewsView.Aggregate().Group(
               x => x.NewsId,
               g => new ListNewsViewQueryResponse
               {
                   NewsId = g.Key,
                   Count = g.Count()
               }).ToList();

            var result = _mapper.Map<IEnumerable<ListNewsViewQueryResponse>>(newsview);

            if (isCacheable)
                await _redisCache.Db0.AddAsync(cacheKey, result, TimeSpan.FromMinutes(5));

            return result;
        }
        public async Task<IEnumerable<ListNewsCommentNPointQueryResponse>> Handle(ListNewsCommentNPointQueryRequest request,
       CancellationToken cancellationToken)
        {
            var isCacheable = false;
            string cacheKey = "newscommentnpoint";
            IFindFluent<NewsCommentNPoint, NewsCommentNPoint>? query;

            if (string.IsNullOrEmpty(request.NewsId))
            {
                var cachedData = await _redisCache.Db0.GetAsync<IEnumerable<ListNewsCommentNPointQueryResponse>>(cacheKey);
                if (cachedData != null)
                    return cachedData;

                query = _context.NewsCommentNPoint.Find(x => true);
                isCacheable = true;
            }
            else
            {
                query = _context.NewsCommentNPoint.Find(x => x.NewsId != null && x.NewsId.Contains(request.NewsId));
            }

            var newsCommentNPoint = query.ToList();

            var result = _mapper.Map<IEnumerable<ListNewsCommentNPointQueryResponse>>(newsCommentNPoint);

            if (isCacheable)
                await _redisCache.Db0.AddAsync(cacheKey, result, TimeSpan.FromMinutes(5));

            return result;
        }


    }
}
