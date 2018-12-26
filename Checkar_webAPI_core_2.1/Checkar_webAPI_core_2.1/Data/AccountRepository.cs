using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkar_webAPI_core.Dtos;
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

        private bool VerifyPasswordHash(string password, byte[] PasswordHash, byte[] PasswordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(PasswordSalt))
            {

                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    // Console.WriteLine(computedHash[i]);
                    // Console.WriteLine(PasswordHash[i]);
                    if (computedHash[i] != PasswordHash[i])
                        return false;
                }

            }
            return true;
        }

        public bool isPasswordMatched(UserLog user, string password)
        {
            byte[] passHash, passSalt;
            passHash = user.PasswordHash;
            passSalt = user.PasswordSalt;
            if (!VerifyPasswordHash(password, passHash, passSalt))
                return false;
            return true;
        }

        public Task<bool> ActivateAccount(string code, string token, string user_id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ChangePasswordViaReset(UserLog user, string password)
        {
            byte[] PasswordHash, PasswordSalt;
            CreatePasswordHash(password, out PasswordHash, out PasswordSalt);

            user.PasswordHash = PasswordHash;
            user.PasswordSalt = PasswordSalt;            //await _context.AddAsync(user);
            await _context.SaveChangesAsync();

            return true;
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
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

        public async Task<UserProfileDetailsDto> Userprofile_details(int userId,int current_login_Userid)
        {
            UserProfileDetailsDto temp = new UserProfileDetailsDto();
            UserLog User = await _context.UserLog.FirstOrDefaultAsync(i => i.IduserLog == userId);
            if(User.Disabled=="T")
            {
                temp.Disabled = "T";
                temp.Total_fans = 0;
                temp.UserFullname = "";
                return temp;
            }
            else
            {
                var totalFans = _context.Fan
                     .Where(f =>
                            f.UserId == userId).Count();
                temp.Total_fans = totalFans;
                temp.Disabled = User.Disabled;
                temp.UserFullname = User.UserFullname;
                temp.UserSex = User.UserSex;
               

                String temp_date = User.UserReg.ToString();
                DateTime tempTime = Convert.ToDateTime(temp_date);
                temp.UserReg = tempTime.ToString("MMMM").ToLower() + " " + tempTime.ToString("yyyy");



                Fan User_following = await _context.Fan.FirstOrDefaultAsync(i => i.UserId == userId && i.IdFan== current_login_Userid);
                if (User_following == null)
                   temp.Following=false;
                else
                {
                    temp.Following= true;

                }
                Fan User_fan = await _context.Fan.FirstOrDefaultAsync(i => i.UserId == current_login_Userid && i.IdFan ==userId);
                if (User_fan == null)
                    temp.Fan = false;
                else
                {
                    temp.Fan = true;

                }




            }

        
            return temp;
    }
}

    }
    

