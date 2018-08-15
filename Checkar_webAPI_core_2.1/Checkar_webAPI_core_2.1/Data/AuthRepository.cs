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
            if (!VerifyPasswordHash(Password, User.password_hash, User.password_salt))
                return null;
            if (Password != User.UserPassword)
                return null;
            return User;
        }

        private bool VerifyPasswordHash(string password, out byte[] PasswordHash, out byte[] PasswordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(PasswordSalt))
            {
                
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for(int i=0; i< computedHash.Length;i++)
                {
                    if (computedHash[i] != PasswordHash[i])
                        return false;
                }
                
        }
            return true;
        }
        public async Task<UserLog> Register(UserLog User, string Password)
        {
            // here we have to hash password
            byte[] PasswordHash, PasswordSalt;
            CreatePasswordHash(Password, out PasswordHash, out PasswordSalt);

            User.Activated = "F";
            User.Disabled = "F";
            User.UserReg = DateTime.UtcNow;
            User.password_hash = PasswordHash;
            User.password_salt = PasswordSalt;
            User.UserSex = User.UserSex.ToLower();
            if (User.UserSex == "male") User.UserSex = "M";
            else if (User.UserSex == "female") User.UserSex = "F";
            else User.UserSex = "M";

            // we have to hash and salt the password using method
            User.UserPassword = Password;
            //

            await _context.UserLog.AddAsync(User);
            await _context.SaveChangesAsync();
            return User;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        public async Task<bool> UserExists(string Email)
        {
            if (await _context.UserLog.AnyAsync(i => i.UserEmaill == Email))
                return true;
            return false;
        }
    }
}
