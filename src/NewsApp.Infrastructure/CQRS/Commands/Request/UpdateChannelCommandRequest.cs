using MediatR;
using NewsApp.Infrastructure.CQRS.Common;
using System.ComponentModel.DataAnnotations;

namespace NewsApp.Infrastructure.CQRS.Commands.Request
{
    public class UpdateChannelCommandRequest : IRequest<EmptyResponse>
    {
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Url { get; set; }
        public string ImagePath { get; set; }
        public int DisplayOrder { get; set; }
    }
}