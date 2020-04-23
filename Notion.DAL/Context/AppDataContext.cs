using Microsoft.EntityFrameworkCore;
using Notion.DAL.Entity.Concrete;

namespace Notion.DAL.Context
{
    public class AppDataContext : DbContext
    {
        public AppDataContext(DbContextOptions<AppDataContext> options)
        : base(options)
    {
    }

        public DbSet<User> Users { get; set; }
    }
}