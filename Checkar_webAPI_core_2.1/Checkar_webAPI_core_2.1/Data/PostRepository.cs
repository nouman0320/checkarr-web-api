using Checkar_webAPI_core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Checkar_webAPI_core.Data
{
    public class PostRepository: IPostRepository
    {

        private readonly checkarrContext _context;
        public PostRepository(checkarrContext context)
        {
            _context = context;
        }

        public async Task<bool> AddPost(int userId, String Text_message, String Picture_url, String Video_url,String catagory)
        {
            if(catagory=="politics")
            {
               Politics temp_post = new Politics();
                temp_post.Time = DateTime.UtcNow;
                temp_post.UserId = userId;
                temp_post.TextMessage = Text_message;
                temp_post.PictureUrl = Picture_url;
                temp_post.VideoUrl = Video_url;

                await _context.Politics.AddAsync(temp_post);
                await _context.SaveChangesAsync();


            }



            return true;
        }
    }
}
