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

        public async Task<bool> StoreActivationCode(Confirmationcode _confirmationCode)
        {
            await _context.Confirmationcode.AddAsync(_confirmationCode);
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<bool> isPasswordMatched(UserLog user, string password)
        {
            if(user.UserPassword == password)
            {
                return true;
            }
            return false;
        }

        public Task<bool> ActivateAccount(string code, string token, string user_id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ChangePasswordViaReset(UserLog user, string password)
        {
            user.UserPassword = password;
            //await _context.AddAsync(user);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Confirmationcode> GetActivationCode(string code, int userID)
        {
            Confirmationcode confirmationCode = await _context.Confirmationcode.FirstOrDefaultAsync(i => i.UserId == userID && i.ConfirmationCode1 == code && i.ConfirmationType == "ACTIVATION_CODE");
            return confirmationCode;
        }

        public async Task<Confirmationcode> ConfirmRecoveryCode(string code, int userID)
        {
            Confirmationcode confirmationCode = await _context.Confirmationcode.FirstOrDefaultAsync(i => i.ConfirmationCode1 == code && i.ConfirmationType == "RECOVERY_CODE" && userID == i.UserId);
            return confirmationCode;
        }


        public async void StoreCode(Confirmationcode confirmationcode)
        {
            await _context.Confirmationcode.AddAsync(confirmationcode);
            await _context.SaveChangesAsync();
        }

        public async Task<UserLog> GetUserFromEmail(string email)
        {
            UserLog User = await _context.UserLog.FirstOrDefaultAsync(i => i.UserEmaill == email);
            return User;
        }

        public async Task<UserLog> GetUserFromUserID(int id)
        {
            UserLog User = await _context.UserLog.FirstOrDefaultAsync(i => i.IduserLog == id);
            return User;
        }

        public bool RemoveConfirmationCode(Confirmationcode _confirmationCode)
        {
            _context.Confirmationcode.Remove(_confirmationCode);
            return true;
        }

        public async Task<bool> SaveUserDetails() {

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
