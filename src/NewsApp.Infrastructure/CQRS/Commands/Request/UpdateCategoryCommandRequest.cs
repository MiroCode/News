using MediatR;
using NewsApp.Infrastructure.CQRS.Common;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NewsApp.Infrastructure.CQRS.Commands.Request
{
    public class UpdateCategoryCommandRequest : IRequest<EmptyResponse>
    {
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int DisplayOrder { get; set; }
        [JsonIgnore]
        public string UserId { get; set; }
    }
}