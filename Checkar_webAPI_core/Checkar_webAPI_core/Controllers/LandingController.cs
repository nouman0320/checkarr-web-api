using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Checkar_webAPI_core.Controllers
{
    
    [Route("api/about")]
    [EnableCors("AllowAnyOrigin")]
    public class LandingController : Controller
    {


        [HttpGet]
        public ContentResult Get()
        {
            return new ContentResult
            {
                ContentType = "text/html",
                StatusCode = (int)HttpStatusCode.OK,
                Content = "<html><body><h1>Property of Checkarr Inc.</h1> <h1>Unauthorized access is not allowed<h1><h5>* Data scrapping is forbidden</h5> <p>* Unauthorized access can be used against you in any court of law</p> </body></html>"

            };
        }

        
    }
}
