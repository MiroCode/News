using MediatR;
using NewsApp.Infrastructure.CQRS.Commands.Response;

namespace NewsApp.Infrastructure.CQRS.Commands.Request
{
    public class CreateTagCommandRequest : IRequest<CreateTagCommandResponse>
    {
        public string Name { get; set; }
        public int DisplayOrder { get; set; }

    }
}
