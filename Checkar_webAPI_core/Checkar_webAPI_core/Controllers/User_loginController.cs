using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;

namespace Checkar_webAPI_core.Controllers
{
    [Produces("application/json")]
    [Route("api/User_login")]
    [EnableCors("AllowAnyOrigin")]
    public class User_loginController : Controller
    {
        
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        /*
        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
        */

        // POST api/values
        [HttpPost]
        public Boolean Post([FromBody]Classes.User user)
        {
            Boolean LoginCheck = false;
            try
            {
                if (user != null)
                {
                    
                    // Initializing New DBContext
                    checkarr.checkarrContext loginDBContext = new checkarr.checkarrContext();
                    checkarr.UserLog UserLogin = loginDBContext.UserLog.FirstOrDefault(i => i.Email == user.Email);
                    
                    
                    if (user.Password == UserLogin.Password)
                    {
                        // To be executed whe login is successful

                        /*
                        System.Diagnostics.Debug.Print("===========================\n");
                        System.Diagnostics.Debug.Print("Login POST successful");
                        System.Diagnostics.Debug.Print("===========================\n");
                        System.Diagnostics.Debug.Print("Password: " + user.Password + "\n");
                        System.Diagnostics.Debug.Print("Email: " + user.Email + "\n");
                        System.Diagnostics.Debug.Print("===========================\n");
                        */

                        LoginCheck = true;
                    }
                    else
                    {
                        // To be executed the login fails
                        LoginCheck = false;
                    }
                }
                else
                {

                    LoginCheck = false;
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Print(e.ToString());
            }

            return LoginCheck;
        }

        /*
        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        */
    }
}
