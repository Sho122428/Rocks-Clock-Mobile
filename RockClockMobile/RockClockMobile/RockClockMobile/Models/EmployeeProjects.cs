using System;
using System.Collections.Generic;
using System.Text;

namespace RockClockMobile.Models
{
    public class EmployeeProjects
    {
        int rocksProjectId { get; set; }
        string projectName { get; set; }
        string clientName { get; set; }
        string descriptioin { get; set; }
        int id { get; set; }
        DateTime modifieddt { get; set; }
        int modifiedby { get; set; }
        DateTime createddt { get; set; }
        string createdby { get; set; }
        string modifiednotes { get; set; }
        int status { get; set; }
        bool isDeleted { get; set; }
    }
}
