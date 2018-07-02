using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace Checkar_webAPI_core.Controllers
{
    
    [Produces("application/json")]
    [Route("api/Register")]
    [EnableCors("AllowAnyOrigin")]
    public class RegisterController : Controller
    {
        // GET: api/Register
        [HttpGet]
        public IEnumerable<string> Get()
        {

            return new string[] { "value1", "value2" };
        }

        // GET: api/Register/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        
        // POST: api/Register
        [HttpPost]
        public Boolean Post([FromBody]Classes.User user)
        {
            if (user.Email != null && user.Password != null)
            {
                
                checkarr.checkarrContext registerDBContext = new checkarr.checkarrContext();
                checkarr.UserLog UserRegister = registerDBContext.UserLog.FirstOrDefault(i => i.UserEmaill == user.Email);
                
                if (UserRegister != null)
                {
                    // To be executed when user exists in the DB
                    System.Diagnostics.Debug.Print("> User Exists");
                    return false;
                }
                else
                {
                    // To be executed when user doesn't exist in the DB
                    UserRegister = new checkarr.UserLog();
                    UserRegister.UserFullname = user.Fullname;
                    UserRegister.UserEmaill = user.Email;
                    UserRegister.UserPassword = user.Password;
                    UserRegister.UserSex = user.Gender;
                    UserRegister.UserReg = DateTime.UtcNow;
                    UserRegister.Activated = "F";
                    UserRegister.Disabled = "F";

                    //Adding user to the register context and saving that context
                    registerDBContext.UserLog.Add(UserRegister);
                    registerDBContext.SaveChanges();

                    // Id of last user
                    UserRegister = registerDBContext.UserLog.Last();
                    int newUserID = UserRegister.IduserLog;


                    Classes.CodeGenerator codeGenerator = new Classes.CodeGenerator();
                    String activationCode = codeGenerator.ActivationCodeGenerator();
//<<<<<<< HEAD
                  //  checkarr.Confirmationcode Acode = new checkarr.Confirmationcode();
                   // Acode.ConfirmationCode = activationCode;
                    //Acode.GeneratedOn = DateTime.UtcNow;
                    //Acode.UserId = newUserID;
                    //Acode.ConfirmationType = "text";
                    //registerDBContext.Confirmationcode.Add(Acode);
                    //registerDBContext.SaveChanges();

                    // HAVE To SAVE THIS IN THE DATABASE WITH EXPIRY DATE
//=======


                    // saving in confirmation code table
                    checkarr.Confirmationcode confirmationCodeModel = new checkarr.Confirmationcode();
                    confirmationCodeModel.ConfirmationCode = activationCode;
                    confirmationCodeModel.ConfirmationType = "ACTIVATION_CODE";
                    confirmationCodeModel.GeneratedOn = DateTime.UtcNow;
                    confirmationCodeModel.ExpiryTime = DateTime.UtcNow.AddDays(1);
                    confirmationCodeModel.Used = "F";
                    confirmationCodeModel.UserId = newUserID;// have to update user id

                    registerDBContext.Confirmationcode.Add(confirmationCodeModel);
                    registerDBContext.SaveChanges();
//>>>>>>> d2bb01f3e62f915bded317d9b1cac9010234e2e6

                    /*
                     * 
                     *  I NEED USER ID OF NEW REGISTERED USER!!
                     *
                     * */

                    Classes.Token tokenGenerator = new Classes.Token();
                    JwtSecurityToken activationToken = tokenGenerator.GenerateActivationToken(newUserID);


                    // sending activation mail
                    Classes.Mailer currentMailer = new Classes.Mailer();
                    currentMailer.sendActivationMail(user.Email, activationToken, activationCode);

                    
                    //System.Diagnostics.Debug.Print("===========================\n");
                    //System.Diagnostics.Debug.Print("Register POST\n");
                    //System.Diagnostics.Debug.Print("===========================\n");
                    //System.Diagnostics.Debug.Print("Fullname: " + user.Fullname + "\n");
                    //System.Diagnostics.Debug.Print("Email: " + user.Email + "\n");
                    //System.Diagnostics.Debug.Print("Password: " + user.Password + "\n");
                    //System.Diagnostics.Debug.Print("Gender: " + user.Gender + "\n");
                    //System.Diagnostics.Debug.Print("===========================\n");
                    
                    return true;
                }
                
               // return false;
            }
            else
            {

                return false;
            }
        }

        

        // PUT: api/Register/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}