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
            Boolean check_var = false;
            try
            {
                if (user != null)
                {
                    /*
                    checkarrEntities1 ch = new checkarrEntities1();
                    user_log u1 = ch.user_log.First(i => i.email == user.Email);
                    */

                    //=== DELETE FOLLOWING !!!!
                    var u1 = new { password = "" };
                    // JUST SO CAN IF WORK!!!
                    //==================== DELETE ABOVE LINE!!!

                    
                    if (user.Password == u1.password)
                    {


                        System.Diagnostics.Debug.Print("===========================\n");
                        System.Diagnostics.Debug.Print("Login POST successful");
                        System.Diagnostics.Debug.Print("===========================\n");
                        System.Diagnostics.Debug.Print("Password: " + user.Password + "\n");
                        System.Diagnostics.Debug.Print("Email: " + user.Email + "\n");
                        System.Diagnostics.Debug.Print("===========================\n");

                        check_var = true;
                    }
                    else
                    {
                        check_var = false;
                        System.Diagnostics.Debug.Print("USER does not exists with the following credentials, please signup!\n");
                    }
                }
                else
                {

                    check_var = false;
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Print(e.ToString());
            }

            return check_var;
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
