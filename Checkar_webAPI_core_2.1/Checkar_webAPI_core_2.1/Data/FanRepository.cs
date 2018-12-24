using Checkar_webAPI_core.Dtos;
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

        public async Task<List<FindFanForReturnDto>> FindFanReturn(int userId)
        {
            Console.WriteLine("LPC");
            var test1 = (from c in _context.Fan
                         join e in _context.DisplayPicture
                             on c.IdFan equals e.UserId
                         join t in _context.UserLog
                         on c.IdFan equals t.IduserLog
                         where c.UserId == userId
                         //join p in db.Phones on c.id equals p.id_contact
                         select new
                         {
                             IdFan = c.IdFan,
                             UserFullname = t.UserFullname,
                             TimeAdded = c.TimeAdded,
                             Url = e.Url
           

        }).ToList();
           // FindFanForReturnDto temporary= new FindFanForReturnDto();
            List<FindFanForReturnDto> fan_list = new List<FindFanForReturnDto>();
            foreach (var element in test1)
            {
                FindFanForReturnDto temporary = new FindFanForReturnDto();

               // String temp1 = await _context.DisplayPicture.ElementAtOrDefault().ToString();
                temporary.IduserLog = element.IdFan;
                temporary.Url = element.Url;
                temporary.UserFullname = element.UserFullname;
                temporary.Time = element.TimeAdded;

                fan_list.Add(temporary);
            }
            
            foreach(FindFanForReturnDto element in fan_list)
            {
                Console.WriteLine(element.UserFullname);
            }
            return fan_list;
        }




    }
}
