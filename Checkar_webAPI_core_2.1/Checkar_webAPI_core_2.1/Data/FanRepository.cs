using Checkar_webAPI_core.Dtos;
using Checkar_webAPI_core.Model;
using Microsoft.EntityFrameworkCore;
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
           
            var test1 = (from c in _context.Fan
                        
                         join t in _context.UserLog
                         on c.IdFan equals t.IduserLog
                         where c.UserId == userId
                         
                         select new
                         {
                             id_fan = c.IdFan,
                             user_fullname = t.UserFullname,
                             time_added = c.TimeAdded,
                             photo_url = ""

                         }).ToList();

          
            List<FindFanForReturnDto> fan_list = new List<FindFanForReturnDto>();
            foreach (var element in test1)
            {
                FindFanForReturnDto temporary = new FindFanForReturnDto();
                DisplayPicture pic = await _context.DisplayPicture.FirstOrDefaultAsync(i => i.UserId == element.id_fan);
                // String temp1 = await _context.DisplayPicture.ElementAtOrDefault().ToString();
                temporary.IduserLog = element.id_fan;
                if (pic == null)
                    temporary.Url = "null";
                else
                    temporary.Url = pic.Url;
                temporary.UserFullname = element.user_fullname;
                String temp = element.time_added.ToString();
                DateTime tempTime = Convert.ToDateTime(temp);
                temporary.Time = tempTime.ToString("MMMM").ToLower()+ " " + tempTime.ToString("yyyy");

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
