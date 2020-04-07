using System;
using System.Collections.Generic;
using System.Text;

namespace RockClockMobile.Models
{
    public class TimeLog
    {
        //public int TimeId { get; set; }
        //public int rocksUserID { get; set; }
        //public DateTime TimeIn { get; set; }
        //public DateTime TimeOut { get; set; }
        //public int projectID { get; set; }
        //public string projectName { get; set; }
        //public bool IsClockedOut { get; set; }


        public int timeLogId { get; set; }
        public int projectID { get; set; }
        public int rocksUserId { get; set; }
        public DateTime timeIn { get; set; }
        public DateTime timeOut { get; set; }
        public int status { get; set; }
        public bool isDeleted { get; set; }
        public List<object> breakLogs { get; set; }
        public List<object> locationLogs { get; set; }




        //public bool IsClockedOut { get; set; }
    }
}
