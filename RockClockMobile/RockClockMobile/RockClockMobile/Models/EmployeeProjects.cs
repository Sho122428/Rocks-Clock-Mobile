using System;
using System.Collections.Generic;
using System.Text;

namespace RockClockMobile.Models
{
    public class EmployeeProjects
    {
        public int rocksProjectId { get; set; }
        public int rocksUserId { get; set; }
        public string rocksProject { get; set; }
        public DateTime modifieddt { get; set; }
        public int modifiedby { get; set; }
        public DateTime createddt { get; set; }
        public int createdby { get; set; }
        public int status { get; set; }
        public string modifiednotes { get; set; }
        public bool isDeleted { get; set; }
    }
}
