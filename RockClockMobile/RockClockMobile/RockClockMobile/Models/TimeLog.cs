using System;
using System.Collections.Generic;
using System.Text;

namespace RockClockMobile.Models
{
    public class TimeLog
    {
        public int TimeId { get; set; }
        public int rocksUserID { get; set; }
        public DateTime TimeIn { get; set; }
        public DateTime TimeOut { get; set; }
        public int projectID { get; set; }
        public bool IsClokedOut { get; set; }
    }
}
