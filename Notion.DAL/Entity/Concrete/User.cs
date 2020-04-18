using Notion.DAL.Entity.Abstract;

namespace Notion.DAL.Entity.Concrete
{
    public class User : BaseEntity<int>
    {
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}