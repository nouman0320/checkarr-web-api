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
        /*
        // GET: api/Authentication
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Authentication/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        */
        // POST: api/Authentication
        [HttpPost]
        public JObject Post([FromBody]JObject value)
        {
            Boolean validationCheck = false;

            try
            {
                //String temp = "noumanarshad0320@gmail.com";
                //System.Diagnostics.Debug.WriteLine(value["AccessToken"]);
                Classes.Token token = new Classes.Token();
                validationCheck = token.ValidateToken(value["AccessToken"].ToString(), value["Email"].ToString());
                //System.Diagnostics.Debug.WriteLine("VALIDATION CHECK => "+ validationCheck);

                
            }
            catch(Exception e)
            {

            }

            JObject returnObject = new JObject();
            returnObject.Add("AccessValidation", validationCheck);
            return returnObject;

        }
        /*
        // PUT: api/Authentication/5
        [HttpPut("{id}")]
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
