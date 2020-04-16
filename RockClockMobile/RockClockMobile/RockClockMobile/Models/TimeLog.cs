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


        //public int timeLogId { get; set; }
        //public int projectID { get; set; }
        //public int rocksUserId { get; set; }
        //public DateTime timeIn { get; set; }
        //public DateTime timeOut { get; set; }
        //public List<BreakLog> breakLogs { get; set; }
        //public List<LocationLog> locationLogs { get; set; }
        //public int id { get; set; }
        //public DateTime modifieddt { get; set; }
        //public int modifiedby { get; set; }
        //public DateTime createddt { get; set; }
        //public int createdby { get; set; }
        //public string modifiednotes { get; set; }
        //public int status { get; set; }
        //public bool isDeleted { get; set; }



        //public bool IsClockedOut { get; set; }

        //new properties
        public int id { get; set; }
        public int jobcode_id { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public int user_id { get; set; }
        public string notes { get; set; }
        public DateTime last_modified { get; set; }
        public DateTime create_at { get; set; }
        public DateTime updated_at { get; set; }
        public DateTime deleted_at { get; set; }
        public bool isDeleted { get; set; }
        public int status { get; set; }
        public DateTime date { get; set; }
        public List<BreakLog> breakLogs { get; set; }
        public string location { get; set; }
    }
}
