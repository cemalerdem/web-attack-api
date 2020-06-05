using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Notion.Api.Controllers.Base;
using Notion.Api.ViewModel;
using Notion.Comman.RequestModels;
using Notion.Comman.ResponseModels;
using Notion.Common.RequestModels;
using Notion.DAL.Entity.Concrete.User;
using Notion.Services.Abstract;

namespace Notion.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _config;
        private readonly IUserService _userService;

        public AuthController(UserManager<User> userManager, IUserService userService, SignInManager<User> signInManager, IConfiguration config)
        {
            _userManager = userManager;
            _userService = userService;
            _signInManager = signInManager;
            _config = config;
        }


        [AllowAnonymous]
        [HttpPost("register")]
        //[ProducesResponseType(200, Type = typeof(UserRegisterResponse))]
        public async Task<IActionResult> Register([FromBody]UserRegisterRequest request)
        {
            var userToCreate = new User
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.Email
            };
            var result = await _userManager.CreateAsync(userToCreate, request.Password);


            //await _userManager.AddToRoleAsync(userToCreate, "Visitor");

            if (result.Succeeded)
                return Ok(new RegisterResult
                {
                    Successful = true
                });
            var errors = result.Errors.Select(x => x.Description);

            return Ok(new RegisterResult { Successful = false, Errors = errors });

        }

        [AllowAnonymous]
        [ProducesResponseType(typeof(UserLoginResponse),200)]
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody]UserLoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                return BadRequest(new LoginResult { Successful = false, Error = "Username and password are invalid." });
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

            if (!result.Succeeded) return BadRequest(new LoginResult { Successful = false, Error = "Username and password are invalid." });

            var claims = new[]
            {
                new Claim("Email", request.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim("FirstName", user.FirstName),
                new Claim("LastName", user.LastName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var token = new JwtSecurityToken(
                issuer: _config["AuthSettings:Issuer"],
                audience: _config["AuthSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new UserLoginResponse {
                UserInfo = claims.ToDictionary(c => c.Type, c => c.Value),
                Token = tokenAsString,
                Successful = true,
                ExpireDate = token.ValidTo
            });

        }
    }
}