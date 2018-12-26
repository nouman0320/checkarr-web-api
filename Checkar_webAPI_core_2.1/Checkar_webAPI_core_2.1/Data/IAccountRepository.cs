using Checkar_webAPI_core.Dtos;
using Checkar_webAPI_core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Checkar_webAPI_core.Data
{
    public interface IAccountRepository
    {
        Task<Confirmationcode> GetActivationCode(string code, int userID);
        Task<Confirmationcode> ConfirmRecoveryCode(string code, int userID);
        Task<Boolean> ChangePasswordViaReset(UserLog User, string password);
        Task<Boolean> ActivateAccount(string code, string token, string user_id);
        void StoreCode(Confirmationcode confirmationcode);
        Task<UserLog> GetUserFromEmail(string email);
        Task<UserLog> GetUserFromUserID(int id);
        Boolean isPasswordMatched(UserLog User, string password);
        Task<Boolean> StoreActivationCode(Confirmationcode confirmationcode);
        Boolean RemoveConfirmationCode(Confirmationcode confirmationcode);
        Task<Boolean> SaveUserDetails();
        Task<UserProfileDetailsDto> Userprofile_details(int userId,int current_login_Userid);
    }
}
