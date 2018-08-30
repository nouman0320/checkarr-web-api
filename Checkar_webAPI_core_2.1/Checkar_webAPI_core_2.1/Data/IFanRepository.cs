using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Checkar_webAPI_core.Data
{
    public interface IFanRepository
    {
        Task<Boolean> AddFan(int userId,int fanID);
    }
}
