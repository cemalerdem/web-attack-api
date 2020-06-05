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
        Task SaveRequestModelToDatabase(RequestDto request);
        Task<List<RequestDto>> GetRequestStreams();
        public Task<List<UserListDto>> GetUsers();
        public Task<UserListDto> UpdateUserAsync(UserListDto user);


    }
}