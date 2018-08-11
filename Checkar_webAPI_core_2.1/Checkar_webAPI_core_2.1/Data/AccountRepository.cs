using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Checkar_webAPI_core.Model;
using Microsoft.EntityFrameworkCore;

namespace Checkar_webAPI_core.Data
{
    public class AccountRepository : IAccountRepository
    {
        private readonly checkarrContext _context;
        public AccountRepository(checkarrContext context)
        {
            _context = context;
        }

        public Task<bool> ActivateAccount(string code, string token, string user_id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ChangePasswordViaReset(string token, string email, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<Confirmationcode> ConfirmRecoveryCode(string code, int userID)
        {
            Confirmationcode confirmationCode = await _context.Confirmationcode.FirstOrDefaultAsync(i => i.ConfirmationCode1 == code && i.ConfirmationType == "RECOVERY_CODE" && userID == i.UserId);
            return confirmationCode;
        }


        public async void StoreCode(Confirmationcode confirmationcode)
        {
            await _context.Confirmationcode.AddAsync(confirmationcode);
            _context.SaveChanges();
        }

        public async Task<UserLog> GetUserFromEmail(string email)
        {
            UserLog User = await _context.UserLog.FirstOrDefaultAsync(i => i.UserEmaill == email);
            if (User == null)
                return null;
            return User;
        }
    }
}
