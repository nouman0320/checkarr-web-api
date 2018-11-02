using Checkar_webAPI_core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Checkar_webAPI_core.Data
{
    public class FanRepository : IFanRepository
    {
        private readonly checkarrContext _context;
        public FanRepository(checkarrContext context)
        {
            _context = context;
        }
        public async Task<bool> AddFan(int userId,int fanID)
        {
            
            Fan temp_fan = new Fan();
            temp_fan.TimeAdded =DateTime.UtcNow;
            temp_fan.UserId = userId;
            temp_fan.IdFan = fanID;
            await _context.Fan.AddAsync(temp_fan);
            await _context.SaveChangesAsync();
          
            return true;
        }

        public async Task<> FindFan()
        {

        }




    }
}
