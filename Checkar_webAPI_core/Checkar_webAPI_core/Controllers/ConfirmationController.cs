using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
namespace Checkar_webAPI_core.Controllers
{
    [Produces("application/json")]
    [Route("api/Confirmation/[action]")]
    [EnableCors("AllowAnyOrigin")]
    public class ConfirmationController : Controller
    {
        // GET: api/Confirmation
        /* [HttpGet]
         public IEnumerable<string> Get()
         {
             return new string[] { "value1", "value2" };
         }

         // GET: api/Confirmation/5
         [HttpGet("{id}", Name = "Get")]
         public string Get(int id)
         {
             return "value";
         }
         */
        // POST: api/Confirmation
        [HttpPost]
        [ActionName("Recovery_Confirmation")]
        public JObject Recovery_Confirmation([FromBody]JObject value)
        {
            JObject returnObject = new JObject();
            
            try
            {
                // query through database and check if codes matches in the confirmation codes table && code is not expired
                checkarr.checkarrContext registerDBContext = new checkarr.checkarrContext();
                string str_code = value["Activation_Code"].ToString();
                int usr_id = value["User_ID"].Value<int>("User_ID");
                string rec_token = value["RECOVERY_TOKEN"].ToString();
                checkarr.Confirmationcode UserCode = registerDBContext.Confirmationcode.FirstOrDefault(i => i.ConfirmationCode == str_code);
                checkarr.UserLog Userr = registerDBContext.UserLog.FirstOrDefault(i => i.IduserLog == UserCode.UserId);
                if (UserCode!=null && UserCode.UserId == usr_id && UserCode.ExpiryTime > DateTime.Now) // check here 
                {
                    if (new Classes.Token().ValidateRecoveryToken(rec_token, Userr.UserEmaill)) // pass second argument recovery eamil fetch from database
                {
                        JwtSecurityToken objecttmp = new JwtSecurityToken();
                        objecttmp = new Classes.Token().GenerateResetToken(Userr.UserEmaill); // pass argument recovery email to that function

                }

                }
            }
            catch(Exception ex)
            {
                returnObject.Add("RETURN_CODE", 3, "REST_TOKEN", null);
            }
            
            return returnObject;
        }

        [ActionName("Account_recovery")]
        [HttpPost]
        public JObject Account_recovery([FromBody]JObject value)
        {
            string recovery_email_temp = "";
            checkarr.checkarrContext registerDBContext = new checkarr.checkarrContext();
            checkarr.UserLog Userr = registerDBContext.UserLog.FirstOrDefault(i => i.UserEmaill == value["RECOVERY_EMAIL"].ToString());
            // query through database and store email in recovery_email_temp
            JObject returnObject = new JObject();

            try
            {
                if (value["RECOVERY_EMAIL"].ToString() == recovery_email_temp)
                {
                    // string gen_recoveryToken_tmp=
                    JwtSecurityToken objecttmp = new JwtSecurityToken();

                    objecttmp = new Classes.Token().GenerateRecoveryToken(value["RECOVERY_EMAIL"].ToString());
                    Classes.CodeGenerator recovery_code_temp = new Classes.CodeGenerator();
                    string recoverycode_stringtemp = recovery_code_temp.RecoveryCodeGenerator();

                    //SAVE recoverycode_stringtemp  TO TABLE CONFIRMATION CODES WITH TYPE "RECOVERY_CODE"
                    checkarr.Confirmationcode newcode = new checkarr.Confirmationcode();
                    newcode.ConfirmationCode = recoverycode_stringtemp;
                    newcode.ConfirmationType = "RECOVERY_CODE";
                    newcode.GeneratedOn = DateTime.UtcNow;
                    newcode.ExpiryTime = DateTime.UtcNow.AddDays(1);
                    newcode.Used = "F";
                    newcode.UserId = Userr.IduserLog;

                    registerDBContext.Confirmationcode.Add(newcode);
                    registerDBContext.SaveChanges();
                    



                    Classes.Mailer mail_temp_obj = new Classes.Mailer();
                    mail_temp_obj.sendRecoveryMail(value["RECOVERY_EMAIL"].ToString(), new JwtSecurityTokenHandler().WriteToken(objecttmp), recoverycode_stringtemp);
                }
            }
            catch(Exception ex)
            {
                returnObject.Add("RETURN_CODE",3, "RECOVERY_TOKEN",null);
            }






            
            return returnObject;
        }

        [HttpPost]
        [ActionName("Account_activation")]
        public JObject Account_activation([FromBody]JObject value)
        {
            JObject returnObject = new JObject();
            
            try
            {
                string activation_code = "";
                int user_id;
                checkarr.checkarrContext registerDBContext = new checkarr.checkarrContext();
                string rec_code = value["RECOVERY_CODE"].ToString();
                string rec_token = value["RECOVERY_TOKEN"].ToString();
                checkarr.Confirmationcode RecCode = registerDBContext.Confirmationcode.FirstOrDefault(i => i.ConfirmationCode == rec_code);
                // query through database and store in user_id and activation_code and check it
                user_id = RecCode.UserId.Value;
                activation_code = RecCode.ConfirmationCode;
                if (value["Activation_Code"].ToString() == activation_code)// && value["User_ID"].ToString() == user_id)
                {
                    // if condition true then perform this action in database updated user account activation status in table "true"

                    checkarr.UserLog Userr = registerDBContext.UserLog.FirstOrDefault(i => i.IduserLog == RecCode.UserId);
                    Userr.Activated = "F";
                    registerDBContext.SaveChanges();

                    string activation_token_tempstr = value["ACTIVATION_TOKEN"].ToString();
                    Classes.Token object1 = new Classes.Token();
                    if (object1.ValidateActivationToken(activation_token_tempstr, user_id))
                    {

                        Boolean check_code_expirytime = false;

                        // use query and check code is not expire and change variable check_code_expirytime true

                        if (RecCode.ExpiryTime > DateTime.Now)
                            check_code_expirytime = true;
                        if (check_code_expirytime)
                        {

                            returnObject.Add("RETURN_CODE", 1);
                        }
                        else
                        {

                            returnObject.Add("RETURN_CODE", 3);
                        }

                    }
                }
                else
                {
                    returnObject.Add("RETURN_CODE", 2);
                }
            }
            catch(Exception exce)
            {

                returnObject.Add("RETURN_CODE", 4);
            }


          
            
            return returnObject;
        }
        
        // PUT: api/Confirmation/5
      /*  [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }*/
    }
}
