using System.Collections.Generic;
using System.Threading.Tasks;
using Notion.DAL.Entity.Concrete;
using Notion.Services.Abstract.Base;

namespace Notion.Services.Abstract
{
    public interface IUserService : IService
    {
        Task<User> Register(User user, string password);
        Task<User> Login(string username, string password);
        Task<bool> UserExist(string username);
        Task<List<User>> GetUsers();
    }
}