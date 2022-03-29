using System.Collections.Generic;

namespace NewsApp.Infrastructure.CQRS.Queries.Response
{
    public class ListNewsQueryResponse
    {
        public string Id { get; set; }
        public string ProviderNewsId { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public string XmlPath { get; set; }
        public int DisplayOrder { get; set; }
        public string ChannelCategoryMapId { get; set; }
        public IEnumerable<ListNewsImageQueryResponse> Images { get; set; }
        public IEnumerable<ListNewsCommentNPointQueryResponse> NewsCommentsNPoints { get; set; }
        public IEnumerable<ListTagQueryResponse> Tags { get; set; }
        public IEnumerable<ListNewsViewQueryResponse> NewsViews { get; set; }


    }
}
