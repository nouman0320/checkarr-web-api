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
        checkarr.checkarrContext registerDBContext = new checkarr.checkarrContext();
        // POST: api/Confirmation
        [HttpPost]
        [ActionName("Account_activation")]
        public JObject Account_activation([FromBody]JObject value)
        {
            JObject returnObject = new JObject();
            
            try
            {
                String activationCode = value["ACTIVATION_CODE"].ToString();
                int userId = Int32.Parse(value["USER_ID"].ToString());
                String activationToken = value["ACTIVATION_TOKEN"].ToString();

                Classes.Token tokenClassObj = new Classes.Token();

                Boolean isActivationTokenValid = tokenClassObj.ValidateActivationToken(activationToken, userId);

                if (isActivationTokenValid)
                {
                    // activation token is valid

                   
                    checkarr.Confirmationcode ccode = registerDBContext.Confirmationcode.FirstOrDefault(i => i.ConfirmationCode == activationCode && i.ConfirmationType== "ACTIVATION_CODE" && i.UserId == userId);
                    if(ccode != null && ccode.ConfirmationCode.Equals(activationCode))
                    {
                        // code is valid
                        if(ccode.ExpiryTime >= DateTime.UtcNow)
                        {
                            checkarr.UserLog User1 = registerDBContext.UserLog.FirstOrDefault(i => i.IduserLog == userId);
                            if(User1!=null )
                            {
                                User1.Activated = "T";
                                registerDBContext.SaveChanges();
                                returnObject.Add("RETURN_CODE", 1);

                            }
                            else returnObject.Add("RETURN_CODE", 5); // exception

                        }
                        else
                        {
                            // code is expired
                            returnObject.Add("RETURN_CODE", 3);
                        }
                    }
                    else
                    {
                        // code ins invalid
                        returnObject.Add("RETURN_CODE", 2);
                    }

                    
                }
                else
                {
                    // activation token is invalid
                    returnObject.Add("RETURN_CODE", 4);
                }

            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("EXCEPTION IN RECOVERY CONFIRMATION = "+ex);
                returnObject.Add("RETURN_CODE", 5);
            }


            /*
             * 
             * 
             * 
             * . RETURN_CODE: 1  = Account is activated
                . RETURN_CODE: 2  = CODE IS INVALID
                . RETURN_CODE: 3  = CODE IS EXPIRED
                . RETURN_CODE: 4  = TOKEN IS INVALID
                . RETURN_CODE: 5  = EXCEPTION 
             * */
            return returnObject;
        }

        [ActionName("Account_recovery")]
        [HttpPost]
        public JObject Account_recovery([FromBody]JObject value)
        {
            JObject returnObject = new JObject();
            try
            {
                
                checkarr.checkarrContext registerDBContext = new checkarr.checkarrContext();
                checkarr.UserLog Userr = registerDBContext.UserLog.FirstOrDefault(i => i.UserEmaill == value["RECOVERY_EMAIL"].ToString());
                // query through database and store email in recovery_email_temp

                if (Userr == null)
                {
                    returnObject.Add("RETURN_CODE", 2);
                    returnObject.Add("RECOVERY_TOKEN", null);
                }

                else if (value["RECOVERY_EMAIL"].ToString() == Userr.UserEmaill)
                {
                    // string gen_recoveryToken_tmp=
                    JwtSecurityToken recoveryToken = new JwtSecurityToken();

                    recoveryToken = new Classes.Token().GenerateRecoveryToken(value["RECOVERY_EMAIL"].ToString());
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
                    mail_temp_obj.sendRecoveryMail(value["RECOVERY_EMAIL"].ToString(), new JwtSecurityTokenHandler().WriteToken(recoveryToken), recoverycode_stringtemp);


                    returnObject.Add("RETURN_CODE", 1);
                    returnObject.Add("RECOVERY_TOKEN", new JwtSecurityTokenHandler().WriteToken(recoveryToken));

                }
                else
                {
                    returnObject.Add("RETURN_CODE", 2);
                    returnObject.Add("RECOVERY_TOKEN", null);
                }
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("EXCEPTION IN ACCOUNT RECOVERY = " + ex);
                returnObject.Add("RETURN_CODE", 3);
                returnObject.Add("RECOVERY_TOKEN",null);
            }


            /*
             * . RETURN_CODE: 1 = RECOVERY MAIL IS SENT
                . RETURN_CODE: 2 = RECOVERY MAIL DOES NOT EXIST
                . RETURN_CODE: 3 = EXCEPTION
             * */
            return returnObject;
        }

        [HttpPost]
        [ActionName("Recovery_confirmation")]
        public JObject Recovery_confirmation([FromBody]JObject value)
        {
            JObject returnObject = new JObject();
            
            try
            {
                String recoveryCode = value["RECOVERY_CODE"].ToString();
                String recoveryToken = value["RECOVERY_TOKEN"].ToString();
                String recoveryEmail = value["RECOVERY_EMAIL"].ToString();

                Classes.Token tokenClassObj = new Classes.Token();

                Boolean isRecoveryTokenValid = tokenClassObj.ValidateRecoveryToken(recoveryToken, recoveryEmail);
                if (isRecoveryTokenValid)
                {
                    // recovery token is valid

                    // MSK => GIVE ME HERE A OBJECT FOR RECOVERY CODE FROM CONFIRMATION CODES TABLE 
                    // MATCH USING PROVIDED EMAIL and CODE and type "RECOVERY CODE"
                    checkarr.UserLog Userr = registerDBContext.UserLog.FirstOrDefault(i => i.UserEmaill == recoveryEmail);

                    if(Userr != null)
                    {
                        
                        checkarr.Confirmationcode ccode1 = registerDBContext.Confirmationcode.FirstOrDefault(i => i.ConfirmationCode == recoveryCode && i.ConfirmationType == "RECOVERY_CODE" && Userr.IduserLog == i.UserId);
                        if (ccode1 != null && ccode1.ExpiryTime >= DateTime.UtcNow)
                        {
                            JwtSecurityToken resetToken = tokenClassObj.GenerateResetToken(recoveryEmail);
                            returnObject.Add("RESET_TOKEN", new JwtSecurityTokenHandler().WriteToken(resetToken));
                            returnObject.Add("RETURN_CODE", 1);
                        }
                        else
                        {
                            System.Diagnostics.Debug.WriteLine("CCODE IF ====>" + ccode1);
                            // recovery code is invalid
                            returnObject.Add("RESET_TOKEN", null);
                            returnObject.Add("RETURN_CODE", 2);
                        }
                    }
                    else
                    {
                        returnObject.Add("RESET_TOKEN", null);
                        returnObject.Add("RETURN_CODE", 5);
                    }
                }
                else
                {
                    // recovery token is invalid
                    returnObject.Add("RESET_TOKEN", null);
                    returnObject.Add("RETURN_CODE", 3);
                }

            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Exception in RECOVERY_CONFIRMATION " + e);
                returnObject.Add("RESET_TOKEN", null);
                returnObject.Add("RETURN_CODE", 4);
            }
            /*
             * RESET_TOKEN +  
             * 
             * . RETURN_CODE: 1 = RECOVERY CODE IS CONFIRMED
                . RETURN_CODE: 2 = RECOVERY CODE IS INVALID
                . RETURN_CODE: 3 = RECOVERY TOKEN IS INVALID
                . RETURN_CODE: 4 = EXCEPTION
                . RETURN_CODE: 5 = SOMETHING WENT WRONG
             * */



            return returnObject;
        }




        [HttpPost]
        [ActionName("Verify_reset_token")]
        public JObject Verify_reset_token([FromBody]JObject value)
        {
            JObject returnObject = new JObject();
            try
            {
                String reset_token = value["RESET_TOKEN"].ToString();
                String reset_email = value["RESET_EMAIL"].ToString();


                Classes.Token TokenObj = new Classes.Token();

                Boolean isResetTokenValid = TokenObj.ValidateResetToken(reset_token, reset_email);
                returnObject.Add("RESET_TOKEN_STATUS", isResetTokenValid);


            }
            catch (Exception e)
            {
                returnObject.Add("RESET_TOKEN_STATUS", false);
                System.Diagnostics.Debug.WriteLine("Exception in Verify reset token: " + e);
            }

            return returnObject;

        }

        [HttpPost]
        [ActionName("Reset_change_password")]
        public JObject Reset_change_password([FromBody]JObject value)
        {
            JObject returnObj = new JObject();

            try
            {
                String RESET_TOKEN = value["RESET_TOKEN"].ToString();
                String RESET_EMAIL = value["RESET_EMAIL"].ToString();
                String NEW_PASSWORD = value["NEW_PASSWORD"].ToString();

                Classes.Token TokenObj = new Classes.Token();

                if(TokenObj.ValidateResetToken(RESET_TOKEN, RESET_EMAIL))
                {

                    /*
                     * MSK UPDATE THE PASSWORD OF THE USER WITH NEW PASSWORD USING RESET EMAIL
                     * */

                    returnObj.Add("RETURN_CODE", 1); // password changed

                }
                else
                {
                    returnObj.Add("RETURN_CODE", 3); // reset token is not valid

                }
               
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Reset change password: " + e);
                returnObj.Add("RETURN_CODE", 2); //exception
            }

            return returnObj;

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
