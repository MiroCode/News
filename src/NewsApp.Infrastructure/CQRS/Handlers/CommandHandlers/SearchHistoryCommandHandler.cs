using AutoMapper;
using MediatR;
using MongoDB.Driver;
using NewsApp.Infrastructure.CQRS.Commands.Request;
using NewsApp.Infrastructure.CQRS.Commands.Response;
using NewsApp.Infrastructure.CQRS.Common;
using NewsApp.Infrastructure.Models;
using StackExchange.Redis.Extensions.Core.Abstractions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NewsApp.Infrastructure.CQRS.Handlers.CommandHandlers
{
    public class SearchHistoryCommandHandler :
         IRequestHandler<CreateSearchHistoryCommandRequest, CreateSearchHistoryCommandResponse?>,
         IRequestHandler<DeleteSearchHistoryCommandRequest, EmptyResponse?>,
         IRequestHandler<UpdateSearchHistoryCommandRequest, EmptyResponse?>
    {
        private readonly MongoDBContext _context;
        private readonly IMapper _mapper;
        private readonly IRedisCacheClient _redisCache;

        public SearchHistoryCommandHandler(MongoDBContext context, IMapper mapper, IRedisCacheClient redisCache)
        {
            _context = context;
            _mapper = mapper;
            _redisCache = redisCache;
        }

        public async Task<CreateSearchHistoryCommandResponse?> Handle(CreateSearchHistoryCommandRequest request,
            CancellationToken cancellationToken)
        {
            var searchHistory = _mapper.Map<SearchHistory>(request);
            searchHistory.CreatedDate = DateTime.Now;
            searchHistory.UpdatedDate = DateTime.Now;

            var isSearchHistoryExists = await _context.SearchHistory.CountDocumentsAsync(x => x.SearchText == request.SearchText,
                cancellationToken: cancellationToken) > 0;

            if (isSearchHistoryExists)
                return null;

            await _context.SearchHistory.InsertOneAsync(searchHistory, cancellationToken: cancellationToken);
            await _redisCache.Db0.RemoveAsync("searchhistory");

            return new CreateSearchHistoryCommandResponse
            {
                Id = searchHistory.Id
            };
        }
        public async Task<EmptyResponse?> Handle(DeleteSearchHistoryCommandRequest request,
          CancellationToken cancellationToken)
        {
            var filter = Builders<SearchHistory>.Filter.Eq("Id", request.Id);
            var result = await _context.SearchHistory.DeleteOneAsync(filter, cancellationToken);

            await _redisCache.Db0.RemoveAllAsync(new[] { "searchhistory", $"searchhistory_{request.Id}" });

            return result.DeletedCount == 0 ? null : EmptyResponse.Default;
        }

        public async Task<EmptyResponse?> Handle(UpdateSearchHistoryCommandRequest request,
           CancellationToken cancellationToken)
        {
            var isSearchHistoryExists = await _context.SearchHistory.CountDocumentsAsync(x => x.Id == request.Id,
                cancellationToken: cancellationToken) > 0;

            if (!isSearchHistoryExists)
                return null;

            var filter = Builders<SearchHistory>.Filter.Eq("Id", request.Id);
            var update = Builders<SearchHistory>.Update
                .Set("SearchText", request.SearchText)
                .Set("UpdatedDate", DateTime.Now);


            var result = await _context.SearchHistory.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);
            await _redisCache.Db0.RemoveAllAsync(new[] { "searchhistory", $"searchhistory_{request.Id}" });

            return result.ModifiedCount == 0 ? null : EmptyResponse.Default;
        }
    }
}
