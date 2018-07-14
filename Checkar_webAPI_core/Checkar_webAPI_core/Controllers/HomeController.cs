using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Checkar_webAPI_core.Controllers
{
   // [Authorize]
    [Produces("application/json")]
    [Route("api/Home/[action]")]
    [EnableCors("AllowAnyOrigin")]
    public class HomeController : Controller
    {
        checkarr.checkarrContext homeDBContext = new checkarr.checkarrContext();
        // GET: api/Home
        /* [HttpGet]
         public IEnumerable<string> Get()
         {
             return new string[] { "value1", "value2" };
         }*/
        [HttpGet]
        [ActionName("Get01")]
        
        public string Get01()
        {
            return "GET 1";
        }
        
        [HttpGet]
        [ActionName("Get02")]
        public string Get02()
        {
            return "Get 2";
        }

        [HttpPost]
        [ActionName("post01")]
        public void post01()
        {
        }

        [HttpPost]
        [ActionName("user_activation_status")]
        public JObject user_activation_status(JObject value)
        {
            JObject returnObj = new JObject();
            try
            {

                String USER_EMAIL = value["USER_EMAIL"].ToString();

                /*
                * 
                * MSK CHECK PROVIDE ME IF USER ACCOUNT IS ACTIVATED OR NOT USING EMAIL
                * */
                checkarr.UserLog user1 = homeDBContext.UserLog.FirstOrDefault(i=> i.UserEmaill == USER_EMAIL);
                string check = user1.Activated;



            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Exception in user_activation_status: "+e);
            }
            return returnObj;
        }

        [HttpPost]
        [ActionName("send_activation_mail")]
        public JObject send_activation_mail([FromBody]JObject value)
        {

            JObject returnObj = new JObject();

            try
            {

                

                int USER_ID = int.Parse(value["USER_ID"].ToString());
                String USER_EMAIL = value["USER_EMAIL"].ToString();

            

                Classes.CodeGenerator codeGenerator = new Classes.CodeGenerator();
                String activationCode = codeGenerator.ActivationCodeGenerator();

                // saving in confirmation code table
                checkarr.Confirmationcode confirmationCodeModel = new checkarr.Confirmationcode();
                confirmationCodeModel.ConfirmationCode = activationCode;
                confirmationCodeModel.ConfirmationType = "ACTIVATION_CODE";
                confirmationCodeModel.GeneratedOn = DateTime.UtcNow;
                confirmationCodeModel.ExpiryTime = DateTime.UtcNow.AddDays(1);
                confirmationCodeModel.Used = "F";
                confirmationCodeModel.UserId = USER_ID;

                homeDBContext.Confirmationcode.Add(confirmationCodeModel);
                homeDBContext.SaveChanges();

               
                Classes.Token tokenGenerator = new Classes.Token();
                JwtSecurityToken activationToken = tokenGenerator.GenerateActivationToken(USER_ID);


                // sending activation mail
                Classes.Mailer currentMailer = new Classes.Mailer();
                currentMailer.sendActivationMail(USER_EMAIL, new JwtSecurityTokenHandler().WriteToken(activationToken), activationCode);

                returnObj.Add("RETURN_CODE", 1); // mail sent
                returnObj.Add("ACTIVATION_TOKEN", new JwtSecurityTokenHandler().WriteToken(activationToken));


            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Exception in home controller while sending activation mail " + e);
                returnObj.Add("RETURN_CODE", 2); // exception;

            }

            return returnObj;

        }



        [HttpPost]
        [ActionName("activate_user_account")]
        public JObject activate_user_account([FromBody]JObject value)
        {
            JObject returnObj = new JObject();
            try
            {

                int USER_ID = int.Parse(value["USER_ID"].ToString());
                String ACTIVATION_CODE = value["ACTIVATION_CODE"].ToString();
                String ACTIVATION_TOKEN = value["ACTIVATION_TOKEN"].ToString();

                Classes.Token tokenObj = new Classes.Token();
                if (tokenObj.ValidateActivationToken(ACTIVATION_TOKEN, USER_ID))
                {

                    // activation token is valid

                    /*
                     * 
                     *  MSK SEE IF ACTIVATION CODE MATCH IN THE TABLE USING USER_ID
                     *  
                     *  IF ACTIVATION CODE EXIST AND VALID THEN ACTIVATE THE ACCOUNT OF THE USER USING USER_ID
                     * 
                     *  WHEN ACCOUNT IS ACTIVATED. REMOVE THE CURRENT ACTIVATION_CODE FROM THE TABLE
                     *  
                     *  
                     * */

                    
                    checkarr.Confirmationcode code1 = homeDBContext.Confirmationcode.FirstOrDefault(i => i.UserId == USER_ID && i.ConfirmationCode == ACTIVATION_CODE && i.ConfirmationType == "ACTIVATION_CODE");
                    if(code1!=null)

                    {
                        checkarr.UserLog user1 = homeDBContext.UserLog.FirstOrDefault(i => i.IduserLog == code1.UserId);
                        user1.Activated = "T";
                        homeDBContext.Confirmationcode.Remove(code1);
                        homeDBContext.SaveChanges();
                        returnObj.Add("RETURN_CODE", 1); // account is activated
                    }
                    else
                    {
                        returnObj.Add("RETURN_CODE", 4); // code not found
                    }
                    
                }
                else
                {

                    returnObj.Add("RETURN_CODE", 2); // activation token is not valid
                }

            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Exception in activate_user_account: " + e);
                returnObj.Add("RETURN_CODE", 3); // exception
            }
            return returnObj;
        }





    }
}
