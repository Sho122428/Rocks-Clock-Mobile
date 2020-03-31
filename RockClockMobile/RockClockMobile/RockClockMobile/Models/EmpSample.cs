using System;
using System.Collections.Generic;
using System.Text;

namespace RockClockMobile.Models
{
    public class EmpSample
    {
        public string firstName { get; set; }
        public string middleName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public List<EmployeeProjects> rocksUserProjectMaps { get; set; }
        public int id { get; set; }
        public DateTime modifieddt { get; set; }
        public int modifiedby { get; set; }
        public DateTime createddt { get; set; }
        public int createdby { get; set; }
        public object modifiednotes { get; set; }
        public int status { get; set; }
        public bool isDeleted { get; set; }
    }
}
