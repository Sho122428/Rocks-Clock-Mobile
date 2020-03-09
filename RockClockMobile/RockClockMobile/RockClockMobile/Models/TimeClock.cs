using System;
using System.Collections.Generic;
using System.Text;

namespace RockClockMobile.Models
{
    class TimeClock
    {
        public string empID { get; set; }
        public DateTime clockIn { get; set; }
        public DateTime clockOut { get; set; }
        
    }
}
