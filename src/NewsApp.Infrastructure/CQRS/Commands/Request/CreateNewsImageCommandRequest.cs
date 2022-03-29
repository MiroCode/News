using MediatR;
using NewsApp.Infrastructure.CQRS.Commands.Response;

namespace NewsApp.Infrastructure.CQRS.Commands.Request
{
    public class CreateNewsImageCommandRequest : IRequest<CreateNewsImageCommandResponse>
    {
        public string NewsId { get; set; }
        public string ImagePath { get; set; }
        public int DisplayOrder { get; set; }

    }
}
