using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Notion.Api.Controllers.Base;
using Notion.Services.Abstract;
using Microsoft.AspNetCore.Identity;
using Notion.DAL.Entity.Concrete.User;

namespace Notion.Api.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        //private readonly GenerateJWTokens _generateJwTokens;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        public UserController(IUserService userService, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _userService = userService;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet("get-users")]
        public async Task GetUsers()
        {
            var users = await _userService.GetUsers();

            var roles = new List<Role>
            {
                new Role{Name = "Member"},
                new Role{Name = "Admin"}
            };

            foreach (var role in roles)
            {
                 _roleManager.CreateAsync(role).Wait();
            }

            foreach (var user in users)
            {
               await _userManager.AddToRoleAsync(user, "Member");
            }

            var adminUser = new User
            {
                UserName = "Admin",
                Email = "superadmin@admin.com"
            };

            var result = _userManager.CreateAsync(adminUser, "Test1234+").Result;

            if (result.Succeeded)
            {
                var admin = _userManager.FindByNameAsync("Admin").Result;
                await _userManager.AddToRoleAsync(admin, "Admin");
            }
        }

        
    }
}