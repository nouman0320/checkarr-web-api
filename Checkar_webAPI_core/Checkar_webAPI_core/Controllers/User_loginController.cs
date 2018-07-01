using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

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
        public IActionResult Post([FromBody]Classes.User user)
        {
            
            
            try
            {
                if (user != null)
                {
                    
                    // Initializing New DBContext
                    checkarr.checkarrContext loginDBContext = new checkarr.checkarrContext();
                    checkarr.UserLog UserLogin = loginDBContext.UserLog.FirstOrDefault(i => i.UserEmaill == user.Email);
                    
                    if(UserLogin == null)
                    {
                        System.Diagnostics.Debug.Print("** USER NOT FOUND");
                        return Ok(new
                        {
                            OK = 3,
                            Issued = false,
                            Token = "",
                            Type = "None",
                            Generation = "NA",
                            Expiration = "NA",
                            Issuer = "http://www.checkarr.com"
                        });
                    }
                    else if (user.Password == UserLogin.UserPassword)
                    {
                        // To be executed whe login is successful

                        Classes.Token CurrentToken = new Classes.Token();
                        CurrentToken.GenerateToken(UserLogin.UserEmaill);

                        
                        //System.Diagnostics.Debug.Print("===========================\n");
                        //System.Diagnostics.Debug.Print("Login POST successful");
                        //System.Diagnostics.Debug.Print("===========================\n");
                        //System.Diagnostics.Debug.Print("Password: " + user.Password + "\n");
                        //System.Diagnostics.Debug.Print("Email: " + user.Email + "\n");
                        //System.Diagnostics.Debug.Print("===========================\n");
                        

                        return Ok(new
                        {
                            Ok = 1,
                            Issued = true,
                            Token = new JwtSecurityTokenHandler().WriteToken(CurrentToken.token),
                            Type = "Bearer",
                            Generation = DateTime.UtcNow,
                            Expiration = CurrentToken.token.ValidTo,
                            Issuer = CurrentToken.token.Issuer
                        });
                    }
                    else
                    {
                        // To be executed the login fails
                        return Ok(new
                        {
                            OK = 2,
                            Issued = false,
                            Token = "Not issued",
                            Type = "None",
                            Generation = "NA",
                            Expiration = "NA",
                            Issuer = "http://www.checkarr.com"
                        });
                    }
                }
                else
                {

                    return Unauthorized();
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.Print(e.ToString());
            }
            return Unauthorized();
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
