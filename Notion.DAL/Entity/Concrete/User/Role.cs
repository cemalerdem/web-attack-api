using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Notion.DAL.Entity.Concrete
{
    public class Role : IdentityRole<Guid>
    {
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}