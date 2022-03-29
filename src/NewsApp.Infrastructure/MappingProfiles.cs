using AutoMapper;
using NewsApp.Infrastructure.CQRS.Commands.Request;
using NewsApp.Infrastructure.CQRS.Queries.Response;
using NewsApp.Infrastructure.Models;

namespace NewsApp.Infrastructure
{

    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<CreateCategoryCommandRequest, Category>();
            CreateMap<Category, ListCategoryQueryResponse>();
            CreateMap<Category, CategoryQueryResponse>();

            CreateMap<CreateChannelCommandRequest, Channel>();
            CreateMap<Channel, ListChannelQueryResponse>();
            CreateMap<Channel, ChannelQueryResponse>();

            CreateMap<CreateUserCommandRequest, User>();
            CreateMap<User, ListUserQueryResponse>();
            CreateMap<User, UserQueryResponse>();

            CreateMap<CreateTagCommandRequest, NewsTag>();
            CreateMap<NewsTag, ListTagQueryResponse>();
            CreateMap<NewsTag, TagQueryResponse>();

            CreateMap<CreateNewsCommandRequest, News>();
            CreateMap<News, ListNewsQueryResponse>();
            CreateMap<News, NewsQueryResponse>();

            CreateMap<NewsView, ListNewsViewQueryResponse>();
            CreateMap<CreateNewsViewCommandRequest, NewsView>();

            CreateMap<CreateNewsCommentNPointCommandRequest, NewsCommentNPoint>();
            CreateMap<NewsCommentNPoint, ListNewsCommentNPointQueryResponse>();

            CreateMap<CreateNewsImageCommandRequest, NewsImage>();
            CreateMap<NewsImage, ListNewsImageQueryResponse>();
            CreateMap<NewsImage, NewsImageQueryResponse>();

            CreateMap<CreateChannelCategoryMapCommandRequest, ChannelCategoryMap>();
            CreateMap<ChannelCategoryMap, ListChannelCategoryMapQueryResponse>();
            CreateMap<ChannelCategoryMap, ChannelCategoryMapQueryResponse>();

            CreateMap<CreateNewsNewsTagMapCommandRequest, NewsNewsTagMap>();
            CreateMap<NewsNewsTagMap, ListNewsNewsTagMapQueryResponse>();
            CreateMap<NewsNewsTagMap, NewsNewsTagMapQueryResponse>();

            CreateMap<CreateUserInterestCommandRequest, UserInterest>();
            CreateMap<UserInterest, ListUserInterestQueryResponse>();
            CreateMap<UserInterest, UserInterestQueryResponse>();

            CreateMap<CreateSearchHistoryCommandRequest, SearchHistory>();
            CreateMap<SearchHistory, ListSearchHistoryQueryResponse>();
            CreateMap<SearchHistory, SearchHistoryQueryResponse>();
        }
    }
}
