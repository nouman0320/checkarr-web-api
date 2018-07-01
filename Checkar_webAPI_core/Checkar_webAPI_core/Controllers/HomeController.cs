using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Checkar_webAPI_core.Controllers
{
   // [Authorize]
    [Produces("application/json")]
    [Route("api/Home/[action]")]
    [EnableCors("AllowAnyOrigin")]
    public class HomeController : Controller
    {
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
        [ActionName("post02")]
        public void post02()
        {

        }
        /*
        // GET: api/Home/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/Home
        [HttpPost]
        
        
        // PUT: api/Home/5
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
