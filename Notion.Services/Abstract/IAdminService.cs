using System.Collections.Generic;
using System.Threading.Tasks;
using Notion.Comman.Dtos;
using Notion.Services.Abstract.Base;

namespace Notion.Services.Abstract
{
    public interface IAdminService : IService
    {
        Task<List<UserDto>> GetUsersWithRoles();
        Task<UserDto> EditUserRoles(string userName, string newRole);
    }
}