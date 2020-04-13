﻿using System;
using System.Collections.Generic;
using System.Text;

namespace RockClockMobile.Models
{
    public class EmployeeProjects
    {
        public string projectName { get; set; }
        public string clientName { get; set; }
        public string description { get; set; }
        public List<EmployeeProjectMap> rocksUserProjectMaps { get; set; }
        public int id { get; set; }
        public DateTime modifieddt { get; set; }
        public int modifiedby { get; set; }
        public DateTime createddt { get; set; }
        public int createdby { get; set; }
        public string modifiednotes { get; set; }
        public int status { get; set; }
        public bool isDeleted { get; set; }
    }
}
