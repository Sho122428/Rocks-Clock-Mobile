using System;
using System.Collections.Generic;
using System.Text;

namespace RockClockMobile.Models
{
    public class TimeClock
    {
        public string empID { get; set; }
        public DateTime clockIn { get; set; }
        public DateTime clockOut { get; set; }
        public DateTime breakStart { get; set; }
        public DateTime breakEnd { get; set; }
        
    }
}
