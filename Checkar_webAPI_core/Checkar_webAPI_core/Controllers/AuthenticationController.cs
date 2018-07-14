using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Checkar_webAPI_core.Controllers
{
    [Produces("application/json")]
    [Route("api/Authentication")]
    [EnableCors("AllowAnyOrigin")]
    public class AuthenticationController : Controller
    {
       
        [HttpPost]
        public JObject Post([FromBody]JObject value)
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
