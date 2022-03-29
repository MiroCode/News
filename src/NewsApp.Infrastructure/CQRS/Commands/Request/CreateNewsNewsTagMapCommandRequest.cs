using MediatR;
using NewsApp.Infrastructure.CQRS.Commands.Response;

namespace NewsApp.Infrastructure.CQRS.Commands.Request
{
    public class CreateNewsNewsTagMapCommandRequest : IRequest<CreateNewsNewsTagMapCommandResponse>
    {
        public string NewsId { get; set; }
        public string TagId { get; set; }
    }
}
