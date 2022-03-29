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
    public class NewsCommandHandler :
        IRequestHandler<CreateNewsCommandRequest, CreateNewsCommandResponse?>,
        IRequestHandler<DeleteNewsCommandRequest, EmptyResponse?>,
        IRequestHandler<DeleteNewsCommentNPointCommandRequest, EmptyResponse?>,
        IRequestHandler<UpdateNewsCommandRequest, EmptyResponse?>,
        IRequestHandler<CreateNewsViewCommandRequest, CreateNewsViewCommandResponse?>,
        IRequestHandler<CreateNewsCommentNPointCommandRequest, CreateNewsCommentNPointCommandResponse?>
    {
        private readonly MongoDBContext _context;
        private readonly IMapper _mapper;
        private readonly IRedisCacheClient _redisCache;

        public NewsCommandHandler(MongoDBContext context, IMapper mapper, IRedisCacheClient redisCache)
        {
            _context = context;
            _mapper = mapper;
            _redisCache = redisCache;
        }

        public async Task<CreateNewsCommandResponse?> Handle(CreateNewsCommandRequest request,
            CancellationToken cancellationToken)
        {
            var news = _mapper.Map<News>(request);
            news.CreatedDate = DateTime.Now;
            news.UpdatedDate = DateTime.Now;

            //var isNewsExists = await _context.News.CountDocumentsAsync(x => x.Id == request.Title,
            //    cancellationToken: cancellationToken) > 0;

            //if (!isNewsExists)
            //    return null;

            await _context.News.InsertOneAsync(news, cancellationToken: cancellationToken);
            await _redisCache.Db0.RemoveAsync("news");

            return new CreateNewsCommandResponse
            {
                Id = news.Id
            };
        }


        public async Task<CreateNewsViewCommandResponse?> Handle(CreateNewsViewCommandRequest request,
          CancellationToken cancellationToken)
        {
            var newsView = _mapper.Map<NewsView>(request);
            newsView.CreatedDate = DateTime.Now;
            newsView.UpdatedDate = DateTime.Now;

            //var isNewsExists = await _context.News.CountDocumentsAsync(x => x.Id == request.Title,
            //    cancellationToken: cancellationToken) > 0;

            //if (!isNewsExists)
            //    return null;

            await _context.NewsView.InsertOneAsync(newsView, cancellationToken: cancellationToken);
            await _redisCache.Db0.RemoveAsync("newsView");

            return new CreateNewsViewCommandResponse
            {
                Id = newsView.Id
            };
        }
        public async Task<CreateNewsCommentNPointCommandResponse?> Handle(CreateNewsCommentNPointCommandRequest request,
        CancellationToken cancellationToken)
        {
            var newsCommentNPoint = _mapper.Map<NewsCommentNPoint>(request);
            newsCommentNPoint.CreatedDate = DateTime.Now;
            newsCommentNPoint.UpdatedDate = DateTime.Now;

            //var isNewsExists = await _context.News.CountDocumentsAsync(x => x.Id == request.Title,
            //    cancellationToken: cancellationToken) > 0;

            //if (!isNewsExists)
            //    return null;

            await _context.NewsCommentNPoint.InsertOneAsync(newsCommentNPoint, cancellationToken: cancellationToken);
            await _redisCache.Db0.RemoveAsync("newscommentnpoint");

            return new CreateNewsCommentNPointCommandResponse
            {
                Id = newsCommentNPoint.Id
            };
        }

        public async Task<EmptyResponse?> Handle(UpdateNewsCommandRequest request,
           CancellationToken cancellationToken)
        {
            var filter = Builders<News>.Filter.Eq("Id", request.Id);
            var update = Builders<News>.Update
                .Set("Title", request.Title)
                .Set("Link", request.Link)
                .Set("Description", request.Description)
                .Set("Language", request.Language)
                .Set("ChannelCategoryMapId", request.ChannelCategoryMapId)
                .Set("ProviderNewsId", request.ProviderNewsId)
                .Set("DisplayOrder", request.DisplayOrder)
                .Set("UpdatedDate", DateTime.Now);

            var result = await _context.News.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);
            await _redisCache.Db0.RemoveAllAsync(new[] { "news", $"news_{request.Id}" });

            return result.ModifiedCount == 0 ? null : EmptyResponse.Default;
        }
        public async Task<EmptyResponse?> Handle(DeleteNewsCommandRequest request,
           CancellationToken cancellationToken)
        {
            var filter = Builders<News>.Filter.Eq("Id", request.Id);
            var result = await _context.News.DeleteOneAsync(filter, cancellationToken);

            await _redisCache.Db0.RemoveAllAsync(new[] { "news", $"news_{request.Id}" });

            return result.DeletedCount == 0 ? null : EmptyResponse.Default;
        }

        public async Task<EmptyResponse?> Handle(DeleteNewsCommentNPointCommandRequest request,
           CancellationToken cancellationToken)
        {
            var filter = Builders<NewsCommentNPoint>.Filter.Eq("NewsId", request.NewsId);
            var result = await _context.NewsCommentNPoint.DeleteOneAsync(filter, cancellationToken);

            await _redisCache.Db0.RemoveAllAsync(new[] { "newscommentnpoint" });

            return result.DeletedCount == 0 ? null : EmptyResponse.Default;
        }
    }
}
