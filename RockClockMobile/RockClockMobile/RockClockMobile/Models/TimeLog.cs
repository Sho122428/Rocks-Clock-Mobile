using System;
using System.Collections.Generic;
using System.ComponentModel;
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


        //Prev properties
        //public int id { get; set; }
        //public int jobcode_id { get; set; }
        //public DateTime start { get; set; }
        //public DateTime end { get; set; }
        //public int user_id { get; set; }
        //public string notes { get; set; }
        //public DateTime last_modified { get; set; }
        //public DateTime create_at { get; set; }
        //public DateTime updated_at { get; set; }
        //public DateTime deleted_at { get; set; }
        //public bool isDeleted { get; set; }
        //public int status { get; set; }
        //public DateTime date { get; set; }
        //public List<BreakLog> breakLogs { get; set; }
        //public string location { get; set; }



        public int ProjectID { get; set; }
        public int RocksUserId { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public int? Duration { get; set; }
        public int LogType { get; set; }

        public ICollection<LocationLog> LocationLogs { get; set; } = new List<LocationLog>();

        public int id { get; set; }
        public DateTime modifieddt { get; set; } = DateTime.UtcNow;
        public int modifiedby { get; set; } = -1;
        public DateTime createddt { get; set; } = DateTime.UtcNow;
        public int createdby { get; set; } = -1;
        public string modifiednotes { get; set; } = "";
        public int status { get; set; } = 1;
        [DefaultValue(false)]
        public bool isDeleted { get; set; } = false;
    }
}
