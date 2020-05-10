using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Notion.DAL.Context;
using Notion.DAL.Entity.Concrete.User;
using Notion.Services.Abstract;

namespace Notion.Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly AppDataContext _context;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;
        public UserService(AppDataContext context, IEmailService emailService, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
            _emailService = emailService;
        }
        public async Task<User> Login(string email, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);

            if (user == null)
                return null;

            //if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            //    return null;

            await _emailService.SendEmailAsync(email, "Log In", "Logged in at : " + DateTime.Now);
            return user;
        }


        public async Task<User> Register(User user, string password)
        {

            CreatePasswordHash(password, out var passwordHash, out var passwordSalt);

            //user.PasswordHash = passwordHash;
            //user.PasswordSalt = passwordSalt;
            user.CreatedAtUTC = DateTime.UtcNow;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<bool> UserExist(string email)
        {
            return await _context.Users.AnyAsync(x => x.Email == email);
        }

        public async Task<List<User>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }


        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                if (computedHash.Where((t, i) => t != passwordHash[i]).Any())
                {
                    return false;
                }
            }
            return true;
        }



    }
}