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

    public class ChannelQueryHandler : IRequestHandler<GetChannelQueryRequest, ChannelQueryResponse>,
        IRequestHandler<ListChannelQueryRequest, IEnumerable<ListChannelQueryResponse>>
    {
        private readonly MongoDBContext _context;
        private readonly IMapper _mapper;
        private readonly IRedisCacheClient _redisCache;

        public ChannelQueryHandler(MongoDBContext context, IMapper mapper, IRedisCacheClient redisCache)
        {
            _context = context;
            _mapper = mapper;
            _redisCache = redisCache;
        }

        public async Task<ChannelQueryResponse> Handle(GetChannelQueryRequest request,
            CancellationToken cancellationToken)
        {
            var cacheKey = $"channel_{request.Id}";
            var cachedData = await _redisCache.Db0.GetAsync<ChannelQueryResponse>(cacheKey);
            if (cachedData != null)
                return cachedData;

            var Channel = await _context
                .Channel
                .Find(x => x.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);

            var result = _mapper.Map<ChannelQueryResponse>(Channel);

            await _redisCache.Db0.AddAsync(cacheKey, result, TimeSpan.FromMinutes(5));

            return result;
        }
        public async Task<IEnumerable<ListChannelQueryResponse>> Handle(ListChannelQueryRequest request, CancellationToken cancellationToken)
        {
            var isCacheable = false;
            string cacheKey = "channel";
            IFindFluent<Channel, Channel>? query;

            if (string.IsNullOrEmpty(request.Name))
            {
                var cachedData = await _redisCache.Db0.GetAsync<IEnumerable<ListChannelQueryResponse>>(cacheKey);
                if (cachedData != null)
                    return cachedData;

                query = _context.Channel.Find(x => true);
                isCacheable = true;
            }
            else
            {
                query = _context.Channel.Find(x => x.Name != null && x.Name.ToLower().Contains(request.Name.ToLower()));
            }

            var channel = await query.ToListAsync(cancellationToken);
            var result = _mapper.Map<IEnumerable<ListChannelQueryResponse>>(channel);

            if (isCacheable)
                await _redisCache.Db0.AddAsync(cacheKey, result, TimeSpan.FromMinutes(5));

            return result;
        }
    }
}
