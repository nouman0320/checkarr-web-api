using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
                /*
                checkarrEntities1 ch = new checkarrEntities1();
                user_log u1 = ch.user_log.First(i => i.email == user.Email);
                
                if (u1.email != null)
                {
                    System.Diagnostics.Debug.Print("User already exists with this email!\n");
                    return false;
                }
                else
                {
                    u1.full_name = user.Fullname;
                    u1.email = user.Email;
                    u1.password = user.Password;
                    u1.gender = user.Gender;
                    ch.user_log.Add(u1);
                    ch.SaveChanges();

                    System.Diagnostics.Debug.Print("===========================\n");
                    System.Diagnostics.Debug.Print("Register POST\n");
                    System.Diagnostics.Debug.Print("===========================\n");
                    System.Diagnostics.Debug.Print("Fullname: " + user.Fullname + "\n");
                    System.Diagnostics.Debug.Print("Email: " + user.Email + "\n");
                    System.Diagnostics.Debug.Print("Password: " + user.Password + "\n");
                    System.Diagnostics.Debug.Print("Gender: " + user.Gender + "\n");
                    System.Diagnostics.Debug.Print("===========================\n");
                    return true;
                }
                */
                return false;
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