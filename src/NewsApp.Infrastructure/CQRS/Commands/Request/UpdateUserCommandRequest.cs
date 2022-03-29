using MediatR;
using NewsApp.Infrastructure.CQRS.Common;
using System.ComponentModel.DataAnnotations;

namespace NewsApp.Infrastructure.CQRS.Commands.Request
{
    public class UpdateUserCommandRequest : IRequest<EmptyResponse>
    {
        public string Id { get; set; }
        [Required]
        public string Email { get; set; }
        public string AppId { get; set; }
    }
}
