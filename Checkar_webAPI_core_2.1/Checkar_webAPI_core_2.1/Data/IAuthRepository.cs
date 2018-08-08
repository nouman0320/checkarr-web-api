using Checkar_webAPI_core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Checkar_webAPI_core.Data
{
    public interface IAuthRepository
    {
        Task<UserLog> Register(UserLog User, string Password);
        Task<UserLog> Login(string Email, string Password);
        Task<Boolean> UserExists(string Email);
    }
}
