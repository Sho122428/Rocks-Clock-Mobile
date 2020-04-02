using System;
using System.Collections.Generic;
using System.Text;

namespace RockClockMobile.Models
{
    public class BreakLog
    {
        //public int BreakId { get; set; }
        //public int TimeId { get; set; }
        //public DateTime BreakIn { get; set; }
        //public DateTime BreakOut { get; set; }
        //public bool IsTakingABreak { get; set; }


        public DateTime breakIn { get; set; }
        public DateTime breakOut { get; set; }
        public int timeLogId { get; set; }
        public int id { get; set; }
        public DateTime modifieddt { get; set; }
        public int modifiedby { get; set; }
        public DateTime createddt { get; set; }
        public int createdby { get; set; }
        public string modifiednotes { get; set; }
        public int status { get; set; }
        public bool isDeleted { get; set; }

        public bool IsTakingABreak { get; set; }
    }
}
