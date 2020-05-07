using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Notion.Api.Controllers.Base;
using Notion.Common.RequestModels;
using Notion.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Notion.Comman.RequestModels;
using Notion.Comman.ResponseModels;
using Notion.DAL.Entity.Concrete.User;

namespace Notion.Api.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _config;
        private readonly ILogger<UserController> _logger;
        //private readonly GenerateJWTokens _generateJwTokens;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;
        public UserController(IUserService userService, IConfiguration config, ILogger<UserController> logger, /*GenerateJWTokens generateJwTokens,*/ UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<Role> roleManager)
        {
            _userService = userService;
            _config = config;
            _logger = logger;
            //_generateJwTokens = generateJwTokens;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        //[ProducesResponseType(200, Type = typeof(UserRegisterResponse))]
        public async Task<IActionResult> Register([FromBody]UserRegisterRequest request)
        {

            if (await _userService.UserExist(request.Email))
                return BadRequest("Username already exist");

            var userToCreate = new User
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName
            };
            var result = await _userManager.CreateAsync(userToCreate, request.Password);
           

            //await _userManager.AddToRoleAsync(userToCreate, "Visitor");

            if (result.Succeeded)
            {
                return Ok(new UserRegisterResponse
                {
                    Email = userToCreate.Email,
                    FirstName = userToCreate.FirstName,
                    LastName = userToCreate.LastName,
                    IsSuccess = true,
                    Message = "Registered Successfully"
                });
            }

            return BadRequest(result.Errors);
        }


        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody]UserLoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            if (result.Succeeded)
            {
                return Ok(new UserLoginResponse
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    IsSuccess = true,
                    Message = "Logged in Successfully",
                    Token = await GenerateJwtToken(user)
                });
            }

            return Unauthorized();
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

        private async Task<string> GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Email)
            };

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }
    }
}