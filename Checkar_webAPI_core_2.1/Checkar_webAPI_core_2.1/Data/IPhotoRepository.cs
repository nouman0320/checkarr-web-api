using Checkar_webAPI_core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Checkar_webAPI_core.Data
{
    public interface IPhotoRepository
    {
        Task<DisplayPicture> GetDisplayPictureFromUserID(int user_id);
    }
}
