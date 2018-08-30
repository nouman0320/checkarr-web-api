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

        public async Task<DisplayPicture> GetDisplayPictureFromUserID(int user_id)
        {
            var currentPhoto = await _context.DisplayPicture.FirstOrDefaultAsync(i => i.UserId == user_id && i.Active == "T");
            return currentPhoto;
        }

        public async Task<DisplayPicture> GetPhoto(int id)
        {
            var photo = await _context.DisplayPicture.FirstOrDefaultAsync(p => p.Id == id);
            return photo;
        }

        public async Task<bool> SaveAll()
        {
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
