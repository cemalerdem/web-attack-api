using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Notion.DAL.Entity.Concrete;

namespace Notion.Api.Helpers
{
    public class GenerateJWTokens
    { 
    //{
    //    private readonly IConfiguration _config;

    //    public GenerateJWTokens(IConfiguration config)
    //    {
    //        _config = config;
    //    }
    //    public string GenerateJwtToken(User user)
    //    {
    //        var claims = new[]
    //        {
    //            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
    //            new Claim(ClaimTypes.Name, user.Email)
    //        };

    //        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

    //        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

    //        var tokenDescriptor = new SecurityTokenDescriptor
    //        {
    //            Subject = new ClaimsIdentity(claims),
    //            Expires = DateTime.Now.AddDays(1),
    //            SigningCredentials = creds
    //        };

    //        var tokenHandler = new JwtSecurityTokenHandler();

    //        var token = tokenHandler.CreateToken(tokenDescriptor);

    //        return tokenHandler.WriteToken(token);

    //    }
    }
}