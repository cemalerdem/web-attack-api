using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Notion.Api.Controllers.Base;
using Notion.Common.RequestModels;
using Notion.Services.Abstract;
using Notion.DAL.Entity.Concrete;
using Microsoft.AspNetCore.Authorization;
using System.Net.Mime;
using Microsoft.AspNetCore.Http;
using Notion.Comman.RequestModels;
using Notion.Comman.ResponseModels;

namespace Notion.Api.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _config;
        public UserController(IUserService userService, IConfiguration config)
        {
            _userService = userService;
            _config = config;
        }

        [HttpPost("register")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Register(UserRegisterRequest request)
        {
            if (await _userService.UserExist(request.Email))
                return BadRequest("Username already exist");

            var userToCreate = new User
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                
            };

            var createdUser = _userService.Register(userToCreate, request.Password);
            return StatusCode(201);
        }

        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("login")]
        public async Task<ActionResult<UserLoginResponse>> Login([FromBody]UserLoginRequest request)
        {
            var user = await _userService.Login(request.Email, request.Password);
            if (user == null)
                return Unauthorized("Email Address is not found");

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Email)
            };

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

            return Ok(new UserLoginResponse
            {
                Id = user.Id,
                Email = user.Email,
                Message = "User Logged in succesfully",
                IsSuccess = true,
                Token = tokenHandler.WriteToken(token)
            });
        }

        [HttpGet]
        public async Task<List<User>> GetUsers()
        {
            return await _userService.GetUsers();
        }
    }
}