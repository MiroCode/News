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

    public class CategoryQueryHandler :
        IRequestHandler<GetCategoryQueryRequest, CategoryQueryResponse>,
        IRequestHandler<ListCategoryQueryRequest, IEnumerable<ListCategoryQueryResponse>>
    {
        private readonly MongoDBContext _context;
        private readonly IMapper _mapper;
        private readonly IRedisCacheClient _redisCache;

        public CategoryQueryHandler(MongoDBContext context, IMapper mapper, IRedisCacheClient redisCache)
        {
            _context = context;
            _mapper = mapper;
            _redisCache = redisCache;
        }

        public async Task<CategoryQueryResponse> Handle(GetCategoryQueryRequest request,
            CancellationToken cancellationToken)
        {
            var cacheKey = $"category_{request.Id}";
            var cachedData = await _redisCache.Db0.GetAsync<CategoryQueryResponse>(cacheKey);
            if (cachedData != null)
                return cachedData;

            var category = await _context
                .Category
                .Find(x => x.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);
            var result = _mapper.Map<CategoryQueryResponse>(category);
            await _redisCache.Db0.AddAsync(cacheKey, result, TimeSpan.FromMinutes(5));

            return result;
        }

        public async Task<IEnumerable<ListCategoryQueryResponse>> Handle(ListCategoryQueryRequest request,
           CancellationToken cancellationToken)
        {
            var isCacheable = false;
            string cacheKey = "category";
            IFindFluent<Category, Category>? query;

            if (string.IsNullOrEmpty(request.Name))
            {
                var cachedData = await _redisCache.Db0.GetAsync<IEnumerable<ListCategoryQueryResponse>>(cacheKey);
                if (cachedData != null)
                    return cachedData;

                query = _context.Category.Find(x => true);
                isCacheable = true;
            }
            else
            {
                query = _context.Category.Find(x => x.Name != null && x.Name.ToLower().Contains(request.Name.ToLower()));
            }

            var category = await query.ToListAsync(cancellationToken);
            var result = _mapper.Map<IEnumerable<ListCategoryQueryResponse>>(category);

            if (isCacheable)
                await _redisCache.Db0.AddAsync(cacheKey, result, TimeSpan.FromMinutes(5));

            return result;
        }
    }
}