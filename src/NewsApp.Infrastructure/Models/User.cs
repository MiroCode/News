namespace NewsApp.Infrastructure.Models
{
    public class User : BaseEntity
    {
        public string Email { get; set; }
        public string AppId { get; set; }
    }
}
