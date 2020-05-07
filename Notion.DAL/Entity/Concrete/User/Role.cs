using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Notion.DAL.Entity.Concrete.User
{
    public class Role : IdentityRole<Guid>
    {
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}