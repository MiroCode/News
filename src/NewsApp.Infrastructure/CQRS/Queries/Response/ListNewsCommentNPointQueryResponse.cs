namespace NewsApp.Infrastructure.CQRS.Queries.Response
{
    public class ListNewsCommentNPointQueryResponse
    {
        public string NewsId { get; set; }
        public string CommandText { get; set; }
        public int Point { get; set; }
    }
}
