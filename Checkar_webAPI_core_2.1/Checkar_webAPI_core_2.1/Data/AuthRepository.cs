using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Checkar_webAPI_core.Model;
using Microsoft.EntityFrameworkCore;

namespace Checkar_webAPI_core.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly checkarrContext _context;
        public AuthRepository(checkarrContext context)
        {
            _context = context;
        }

        public async Task<UserLog> Login(string Email, string Password)
        {
            UserLog User = await _context.UserLog.FirstOrDefaultAsync(i => i.UserEmaill == Email);
            if (User == null)
                return null;
            if (Password != User.UserPassword)
                return null;
            return User;
        }

        public async Task<UserLog> Register(UserLog User, string Password)
        {
            // here we have to hash password
            await _context.UserLog.AddAsync(User);
            await _context.SaveChangesAsync();
            return User;
        }

        public async Task<bool> UserExists(string Email)
        {
            if (await _context.UserLog.AnyAsync(i => i.UserEmaill == Email))
                return true;
            return false;
        }
    }
}
