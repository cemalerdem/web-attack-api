using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Notion.DAL.Entity.Concrete
{
    public class User : IdentityUser<Guid>
    {
        public string FirstName {get;set;}
        public string LastName {get;set;}
        public DateTime CreatedAtUTC { get; set; }
        public DateTime? UpdatedAtUTC { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}