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
                    checkarr.UserLog UserLogin = loginDBContext.UserLog.FirstOrDefault(i => i.Email == user.Email);
                    
                    
                    if (user.Password == UserLogin.Password)
                    {
                        // To be executed whe login is successful

                        var claims = new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Sub, UserLogin.Email),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                        };

                        string privateSecretKey = "OfED+KgbZxtu4e4+JSQWdtSgTnuNixKy1nMVAEww8QL3IN3idcNgbNDSSaV4491Fo3sq2aGSCtYvekzs7JwXJnNAyvDSJjfK/7M8MpxSMnm1vMscBXyiYFXhGC4wqWlYBE828/5DNyw3QZW5EjD7hvDrY5OlYd4smCTa53helNnJz5NT9HQaDbE2sMwIDAQABAoIBAEs63TvT94njrPDP3A/sfCEXg1F2y0D/PjzUhM1aJGcRiOUXnGlYdViGhLnnJoNZTZm9qI1LT0NWcDA5NmBN6gcrk2EApyTt1D1i4AQ66rYoTF9iEC4Wye28v245BYESA6IIelgIxXGsVyllERsbTkaphzibbYfHmvwMxkn135Zfzd/NOXl/O32vYIomzrNEP+tN2WXhhG8c8+iZ8PErBV3CqrYogYy97d2CeQbXcpd5unPiU4TK0nnzeBAXdgeYuJHFC45YHl9UvShRoe6CHR47ceIGp6WMc5BTyyTkZpctuYJTwaChdj/QuRSkTYmn6jFL+MRfYQJ8VVwSVo5DbkECgYEA4/YIMKcwObYcSuHzgkMwH645CRDoy9M98eptAoNLdJBHYz23U5IbGL1+qHDDCPXxKs9ZG7EEqyWezq42eoFoebLA5O6/xrYXoaeIb094dbCF4D932hAkgAaAZkZVsSiWDCjYSV+JoWX4NVBcIL9yyHRhaaPVULTRbPsZQWq9+hMCgYEA48j4RGO7CaVpgUVobYasJnkGSdhkSCd1VwgvHH3vtuk7/JGUBRaZc0WZGcXkAJXnLh7QnDHOzWASdaxVgnuviaDi4CIkmTCfRqPesgDR2Iu35iQsH7P2/o1pzhpXQS/Ct6J7/GwJTqcXCvp4tfZDbFxS8oewzp4RstILj+pDyWECgYByQAbOy5xB8GGxrhjrOl1OI3V2c8EZFqA/NKy5y6/vlbgRpwbQnbNy7NYj+Y/mV80tFYqldEzQsiQrlei78Uu5YruGgZogL3ccj+izUPMgmP4f6+9XnSuN9rQ3jhy4k4zQP1BXRcim2YJSxhnGV+1hReLknTX2IwmrQxXfUW4xfQKBgAHZW8qSVK5bXWPjQFnDQhp92QM4cnfzegxe0KMWkp+VfRsrw1vXNx";
                        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(privateSecretKey));

                    
                    var token = new JwtSecurityToken(
                                issuer: "http://www.checkarr.com",
                                audience: "http://www.checkarr.com",
                                expires: DateTime.UtcNow.AddMinutes(5),
                                claims: claims,
                                signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
                            );

                        /*
                        System.Diagnostics.Debug.Print("===========================\n");
                        System.Diagnostics.Debug.Print("Login POST successful");
                        System.Diagnostics.Debug.Print("===========================\n");
                        System.Diagnostics.Debug.Print("Password: " + user.Password + "\n");
                        System.Diagnostics.Debug.Print("Email: " + user.Email + "\n");
                        System.Diagnostics.Debug.Print("===========================\n");
                        */

                        return Ok(new
                        {
                            Token = new JwtSecurityTokenHandler().WriteToken(token),
                            Type = "Bearer",
                            Generation = DateTime.UtcNow,
                            Expiration = token.ValidTo,
                            Issuer = token.Issuer
                        });
                    }
                    else
                    {
                        // To be executed the login fails
                        return Unauthorized();
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
