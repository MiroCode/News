using MediatR;
using NewsApp.Infrastructure.CQRS.Commands.Response;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NewsApp.Infrastructure.CQRS.Commands.Request
{
    public class CreateCategoryCommandRequest : IRequest<CreateCategoryCommandResponse>
    {
        [Required]
        public string Name { get; set; }
        public int DisplayOrder { get; set; }
        [JsonIgnore]
        public string UserId { get; set; }
    }
}