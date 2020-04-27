using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace RockClockMobile.Models
{
    public class TimeLog
    {
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


        //Timelog DTO
        //public int id { get; set; }
        public int jobcode_id { get; set; }
        public DateTime start { get; set; } = DateTime.UtcNow;
        public DateTime? end { get; set; }
        public int user_id { get; set; }
        public string notes { get; set; }
        public DateTime last_modified { get; set; }
        public DateTime create_at { get; set; } = DateTime.UtcNow;
        public DateTime updated_at { get; set; }
        public DateTime deleted_at { get; set; }
        //public bool isDeleted { get; set; }
        //public int status { get; set; }
        public DateTime date => this.start.Date;
        public ICollection<BreakLog> BreakLogs { get; set; }
       
    }
}
