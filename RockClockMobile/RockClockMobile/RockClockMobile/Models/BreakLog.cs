using System;
using System.Collections.Generic;
using System.Text;

namespace RockClockMobile.Models
{
    public class BreakLog
    {
        public int BreakId { get; set; }
        public int TimeId { get; set; }
        public DateTime BreakIn { get; set; }
        public DateTime BreakOut { get; set; }
    }
}
