using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Notion.Api.Controllers.Base;
using Notion.Comman.Dtos;
using Notion.Services.Abstract;

namespace Notion.Api.Controllers.Admin
{
    public class AdminController : BaseController
    {
        private readonly IAdminService _adminService;
        private readonly ILogger<AdminController> _logger;

        public AdminController(IAdminService adminService, ILogger<AdminController> logger)
        {
            _adminService = adminService;
            _logger = logger;
        }


        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet("get-users-with-roles")]
        public async Task<IActionResult> GetUsersWithRoles()
        {
            var users = await _adminService.GetUsersWithRoles();
            _logger.LogError("Failed to get user roles" + users);
            return Ok(users);
        }
        [Authorize(Policy = "RequireAdminRole")]
        [HttpPost("editRoles/{userName}")]
        public async Task<IActionResult> EditUserRoles(string userName, string newRole)
        {
            var editedUserRole = await _adminService.EditUserRoles(userName, newRole);
            return Ok(editedUserRole);
        }

        [HttpGet("request-stream")]
        public async Task<List<RequestDto>> GetRequestStream()
        {
            var result =  await _adminService.GetRequestStreams();
            return result;
        }
    }
}