using System;
using System.Collections.Generic;
using System.Text;
using RockClockMobile.Enums;

namespace RockClockMobile.Models
{
    public class TimeLogVM
    {
        public int ProjectID { get; set; } = 0;
        public int RocksUserId { get; set; } = 0;
        public DateTime? TimeIn { get; set; }
        public DateTime? TimeOut { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public int LogType { get; set; }
        public Status Status { get; set; } = Status.ForClockIn;
    }
}
