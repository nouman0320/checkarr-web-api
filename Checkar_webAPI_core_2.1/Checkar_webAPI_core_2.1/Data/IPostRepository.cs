using Checkar_webAPI_core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Checkar_webAPI_core.Data
{
   public interface IPostRepository
    {

        Task<bool> AddPost(int userId, String Text_message, String Picture_url, String Video_url,string catagory);
    }
}
