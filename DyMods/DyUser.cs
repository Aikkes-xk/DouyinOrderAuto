using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DyOrderAuto.DyMods
{
    public class DyUser
    {
        public string UserName { get; set; }
        public ulong UserID { get; set; }

        public string avatar_url { get; set; }

    }
    public class DyUserList 
    {
        public string Userid { get; set; }

        public string UserName { get; set;}

        public string UserAvateIcon { get; set; }
        public int RecordsNumber { get; set; }

        public DateTime ListTime { get; set; }

        public int TodayNumber { get; set; }
    }


}
