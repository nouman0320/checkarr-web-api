using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Checkar_webAPI_core.Model;

namespace Checkar_webAPI_core.Data
{
    public class PhotoRepository : IPhotoRepository
    {
        public Task<DisplayPicture> GetDisplayPictureFromUserID(int user_id)
        {
            throw new NotImplementedException();
        }
    }
}
