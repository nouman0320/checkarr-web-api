using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Checkar_webAPI_core.Model;
using Microsoft.EntityFrameworkCore;

namespace Checkar_webAPI_core.Data
{
    public class PhotoRepository : IPhotoRepository
    {

        private readonly checkarrContext _context;
        public PhotoRepository(checkarrContext context)
        {
            _context = context;
        }

        public Task<DisplayPicture> GetDisplayPictureFromUserID(int user_id)
        {
            throw new NotImplementedException();
        }

        public async Task<DisplayPicture> GetPhoto(int id)
        {
            var photo = await _context.DisplayPicture.FirstOrDefaultAsync(p => p.Id == id);
            return photo;
        }
    }
}
