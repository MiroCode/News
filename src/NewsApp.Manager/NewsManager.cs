using MediatR;
using NewsApp.Infrastructure.CQRS.Commands.Request;
using NewsApp.Infrastructure.CQRS.Commands.Response;
using NewsApp.Infrastructure.CQRS.Common;
using NewsApp.Infrastructure.CQRS.Queries.Request;
using NewsApp.Infrastructure.CQRS.Queries.Response;
using NewsApp.Manager.Abstraction;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewsApp.Manager
{
    public class NewsManager : INewsManager
    {
        private readonly IMediator _mediator;
        public NewsManager(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<CreateNewsCommandResponse> CreateNewsAsync(CreateNewsCommandRequest requestModel)
        {
            var model = await _mediator.Send(requestModel);
            foreach (var imagePath in requestModel.ImagePaths)
            {

                var request = new CreateNewsImageCommandRequest
                {
                    NewsId = model.Id,
                    ImagePath = imagePath
                };
                await _mediator.Send(request);
            }

            return model;
        }

        public async Task<IEnumerable<ListNewsQueryResponse>> GetAllNewsAsync(ListNewsQueryRequest requestModel)
        {
            if (!string.IsNullOrEmpty(requestModel.Title))
            {
                //return await _mediator.Send(new searchtable);
            }
            var response = new List<ListNewsQueryResponse>();
            var news = await _mediator.Send(requestModel);
            foreach (var item in news)
            {
                var newsImageQueryRequest = new ListNewsImageQueryRequest { NewsId = item.Id };
                var Imageresponse = await _mediator.Send(newsImageQueryRequest);
                ListNewsQueryResponse newsresult = item;
                newsresult.Images = Imageresponse;

                var newsCommentnPointQueryRequest = new ListNewsCommentNPointQueryRequest { NewsId = requestModel.NewsId };
                var newsCommentnPoint = await _mediator.Send(newsCommentnPointQueryRequest);
                newsresult.NewsCommentsNPoints = newsCommentnPoint;

                var tagQueryRequest = new ListTagQueryRequest { NewsId = requestModel.NewsId };
                var tag = await _mediator.Send(tagQueryRequest);
                newsresult.Tags = tag;

                var newsViewQueryRequest = new ListNewsViewQueryRequest { NewsId = requestModel.NewsId };
                var newsView = await _mediator.Send(newsViewQueryRequest);
                newsresult.NewsViews = newsView;

                //todo : newscommentnpoint 
                //todo : newsview
                //todo : newsnewstagmap
                response.Add(newsresult);
            }

            return response;
        }

        public async Task<NewsQueryResponse> GetNewsAsync(GetNewsQueryRequest requestModel)
        {
            var news = await _mediator.Send(requestModel);

            var newsImageQueryRequest = new ListNewsImageQueryRequest { NewsId = requestModel.Id };
            var Imageresponse = await _mediator.Send(newsImageQueryRequest);
            news.Images = Imageresponse;

            var newsCommentnPointQueryRequest = new ListNewsCommentNPointQueryRequest { NewsId = requestModel.Id };
            var newsCommentnPoint = await _mediator.Send(newsCommentnPointQueryRequest);
            news.NewsCommentsNPoints = newsCommentnPoint;

            var tagQueryRequest = new ListTagQueryRequest { NewsId = requestModel.Id };
            var tag = await _mediator.Send(tagQueryRequest);
            news.Tags = tag;

            var newsViewQueryRequest = new ListNewsViewQueryRequest { NewsId = requestModel.Id };
            var newsView = await _mediator.Send(newsViewQueryRequest);
            news.NewsViews = newsView;
            //todo : newscommentnpoint 
            //todo : newsview
            //todo : newsnewstagmap

            return news;
        }

        public async Task<EmptyResponse?> UpdateNewsAsync(UpdateNewsCommandRequest requestModel)
        {
            var request = new DeleteNewsImageCommandRequest
            {
                NewsId = requestModel.Id
            };

            await _mediator.Send(request);

            foreach (var imagePath in requestModel.ImagePaths)
            {

                var requestImage = new CreateNewsImageCommandRequest
                {
                    NewsId = requestModel.Id,
                    ImagePath = imagePath
                };
                await _mediator.Send(requestImage);
            }

            return await _mediator.Send(requestModel);
        }
        public async Task<EmptyResponse?> DeleteNewsAsync(DeleteNewsCommandRequest requestModel)
        {
            var requestNewsImage = new DeleteNewsImageCommandRequest
            {
                NewsId = requestModel.Id
            };

            await _mediator.Send(requestNewsImage);

            var requestNewsTagMap = new DeleteNewsNewsTagMapCommandRequest
            {
                NewsId = requestModel.Id
            };

            await _mediator.Send(requestNewsTagMap);

            var requestNewsCommentNPoint = new DeleteNewsCommentNPointCommandRequest
            {
                NewsId = requestModel.Id
            };

            await _mediator.Send(requestNewsCommentNPoint);

            //todo : view ve commentleride  tagleride silmeli
            return await _mediator.Send(requestModel);
        }

        public async Task<IEnumerable<ListNewsViewQueryResponse>> GetAllNewsViewAsync(ListNewsViewQueryRequest requestModel)
        {
            return await _mediator.Send(requestModel);
        }
        public async Task<CreateNewsViewCommandResponse> CreateNewsViewAsync(CreateNewsViewCommandRequest requestModel)
        {
            return await _mediator.Send(requestModel);
        }

        public async Task<CreateNewsCommentNPointCommandResponse> CreateNewsCommentNPointAsync(CreateNewsCommentNPointCommandRequest requestModel)
        {
            return await _mediator.Send(requestModel);
        }
        public async Task<IEnumerable<ListNewsCommentNPointQueryResponse>> GetAllNewsCommentNPointAsync(ListNewsCommentNPointQueryRequest requestModel)
        {
            return await _mediator.Send(requestModel);
        }
    }
}
