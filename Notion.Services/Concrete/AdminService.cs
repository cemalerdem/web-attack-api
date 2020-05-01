using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Notion.Comman.Dtos;
using Notion.DAL.Context;
using Notion.DAL.Entity.Concrete;
using Notion.Services.Abstract;

namespace Notion.Services.Concrete
{
    public class AdminService : IAdminService
    {
        private readonly AppDataContext _context;
        private readonly UserManager<User> _userManager;
        public AdminService(AppDataContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<List<UserDto>> GetUsersWithRoles()
        {
            var userList = await _context.Users
                .OrderBy(x => x.UserName)
                .Select(user => new UserDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    Roles = (from userRole in user.UserRoles
                             join role in _context.Roles on userRole.RoleId equals role.Id
                             where role.Name != "Admin"
                             select role.Name).ToList()
                }).ToListAsync();

            return userList;
        }

        public async Task<UserDto> EditUserRoles(string userName, string newRole)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return null;
            }

            var roles = await _userManager.GetRolesAsync(user);
            var userToRemoveRole = await _userManager.RemoveFromRoleAsync(user, roles.FirstOrDefault());
            if (!userToRemoveRole.Succeeded)
                throw new Exception("Cannot remove the exist roles");
            var userToAddRole = await _userManager.AddToRoleAsync(user, newRole);

            if (!userToAddRole.Succeeded)
                throw new Exception("Failed to add to role");

            var getUserNewRole = _userManager.GetRolesAsync(user).Result;

            var result = new UserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Roles = getUserNewRole.ToList()
            };

            return result;
        }
    }
}