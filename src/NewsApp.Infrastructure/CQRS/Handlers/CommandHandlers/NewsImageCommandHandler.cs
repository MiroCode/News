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
    public class NewsImageCommandHandler : IRequestHandler<CreateNewsImageCommandRequest, CreateNewsImageCommandResponse?>,
        IRequestHandler<DeleteNewsImageCommandRequest, EmptyResponse>,
        IRequestHandler<UpdateNewsImageCommandRequest, EmptyResponse?>
    {
        private readonly MongoDBContext _context;
        private readonly IMapper _mapper;
        private readonly IRedisCacheClient _redisCache;
        public NewsImageCommandHandler(MongoDBContext context, IMapper mapper, IRedisCacheClient redisCache)
        {
            _context = context;
            _mapper = mapper;
            _redisCache = redisCache;
        }
        public async Task<CreateNewsImageCommandResponse?> Handle(CreateNewsImageCommandRequest request,
           CancellationToken cancellationToken)
        {
            var newsImage = _mapper.Map<NewsImage>(request);
            newsImage.CreatedDate = DateTime.Now;
            newsImage.UpdatedDate = DateTime.Now;

            await _context.NewsImage.InsertOneAsync(newsImage, cancellationToken: cancellationToken);
            await _redisCache.Db0.RemoveAsync("newsimage");

            return new CreateNewsImageCommandResponse
            {
                Id = newsImage.Id
            };
        }
        public async Task<EmptyResponse?> Handle(DeleteNewsImageCommandRequest request,
          CancellationToken cancellationToken)
        {
            var filter = Builders<NewsImage>.Filter.Where(x => x.Id != null);
            if (!string.IsNullOrEmpty(request.Id))
                filter = Builders<NewsImage>.Filter.Eq("Id", request.Id);
            if (!string.IsNullOrEmpty(request.NewsId))
                filter = Builders<NewsImage>.Filter.Eq("NewsId", request.NewsId);

            var result = await _context.NewsImage.DeleteManyAsync(filter, cancellationToken);

            await _redisCache.Db0.RemoveAllAsync(new[] { "newsimage", $"newsimage_{request.Id}" });

            return result.DeletedCount == 0 ? null : EmptyResponse.Default;
        }
        public async Task<EmptyResponse> Handle(UpdateNewsImageCommandRequest request, CancellationToken cancellationToken)
        {
            var filter = Builders<NewsImage>.Filter.Eq("Id", request.Id);
            var update = Builders<NewsImage>.Update
                .Set("NewsId", request.NewsId)
                .Set("ImagePath", request.ImagePath)
                .Set("DisplayOrder", request.DisplayOrder)
                .Set("UpdatedDate", DateTime.Now);

            var result = await _context.NewsImage.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);
            await _redisCache.Db0.RemoveAllAsync(new[] { "newsimage", $"newsimage_{request.Id}" });

            return result.ModifiedCount == 0 ? null : EmptyResponse.Default;
        }
    }
}
