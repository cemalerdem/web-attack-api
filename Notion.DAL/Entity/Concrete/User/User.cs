using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Notion.DAL.Entity.Concrete.User
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