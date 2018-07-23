using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Checkar_webAPI_core.Controllers
{
    [Produces("application/json")]
    [Route("api/Authentication/[action]")]
    [EnableCors("AllowAnyOrigin")]
    public class AuthenticationController : Controller
    {

        [HttpPost]
        [ActionName("refresh_access_token")]
        public JObject refresh_access_token([FromBody]JObject value)
        {
            JObject returnObject = new JObject();

            try
            {
                String REFRESH_TOKEN = value["refresh_token"].ToString();
                String EMAIL = value["email"].ToString();


                Classes.Token currentTokenObj = new Classes.Token();
                if(currentTokenObj.ValidateRefreshToken(REFRESH_TOKEN, EMAIL))
                {
                    // refresh token is valid

                    String new_refresh_token = new JwtSecurityTokenHandler().WriteToken(currentTokenObj.GenerateRefreshToken(EMAIL));

                    currentTokenObj.GenerateToken(EMAIL);
                    String new_access_token = new JwtSecurityTokenHandler().WriteToken(currentTokenObj.token);
                    String email = EMAIL;


                    

                    checkarr.checkarrContext checkarrDBContext = new checkarr.checkarrContext();
                    checkarr.UserLog user1 = checkarrDBContext.UserLog.FirstOrDefault(i => i.UserEmaill == EMAIL);

                    if (user1 != null)
                    {
                        String activation_check = user1.Activated;

                        if (activation_check == "T") returnObject.Add("account_activated", true);
                        else returnObject.Add("account_activated", false);


                        int user_id = user1.IduserLog;
                        returnObject.Add("user_id", user_id);

                        String user_email = user1.UserEmaill;
                        returnObject.Add("user_email", user_email);


                    }

                    returnObject.Add("RETURN_CODE", 1); // refresh token valid
                    returnObject.Add("NEW_REFRESH_TOKEN", new_refresh_token);
                    returnObject.Add("NEW_ACCESS_TOKEN", new_access_token);
                    returnObject.Add("EMAIL", email);

                }
                else
                {
                    // refresh token is not valid
                    returnObject.Add("RETURN_CODE", 2); // refresh token is not valid
                }
            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Exception in refresh_access_token :" + e);
                returnObject.Add("RETURN_CODE", 3); // exception has occured
            }

            return returnObject;
        }


        [HttpPost]
        [ActionName("validate_access_token")]
        public JObject validate_access_token([FromBody]JObject value)
        {
            Boolean validationCheck = false;
            JObject returnObject = new JObject();
            try
            {
                //String temp = "noumanarshad0320@gmail.com";
                //System.Diagnostics.Debug.WriteLine(value["AccessToken"]);
                Classes.Token token = new Classes.Token();
                validationCheck = token.ValidateToken(value["AccessToken"].ToString(), value["Email"].ToString());
                //System.Diagnostics.Debug.WriteLine("VALIDATION CHECK => "+ validationCheck);
                returnObject.Add("AccessValidation", validationCheck);


                if(validationCheck == true)
                {
                    checkarr.checkarrContext checkarrDBContext = new checkarr.checkarrContext();
                    checkarr.UserLog user1 = checkarrDBContext.UserLog.FirstOrDefault(i => i.UserEmaill == value["Email"].ToString());

                    if(user1 != null)
                    {
                        String activation_check = user1.Activated;

                        if (activation_check == "T") returnObject.Add("account_activated", true);
                        else returnObject.Add("account_activated", false);


                        int user_id = user1.IduserLog;
                        returnObject.Add("user_id", user_id);

                        String user_email = user1.UserEmaill;
                        returnObject.Add("user_email", user_email);


                    }
                    
                }
               





            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Exception in AuthenticationController" + e);
            }

            
            
            return returnObject;

        }
    }
}
