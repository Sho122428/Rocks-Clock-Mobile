﻿using System;
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
        public List<BreakLog> breakLogs { get; set; }
        public List<LocationLog> locationLogs { get; set; }
        public int id { get; set; }
        public DateTime modifieddt { get; set; }
        public int modifiedby { get; set; }
        public DateTime createddt { get; set; }
        public int createdby { get; set; }
        public string modifiednotes { get; set; }
        public int status { get; set; }
        public bool isDeleted { get; set; }



        //public bool IsClockedOut { get; set; }
    }
}
