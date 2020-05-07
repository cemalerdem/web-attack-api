using System;
using Microsoft.AspNetCore.Identity;

namespace Notion.DAL.Entity.Concrete.User
{
    public class UserRole : IdentityUserRole<Guid>
    {
        public virtual Concrete.User.User User { get; set; }
        public virtual Role Role { get; set; }
    }
}