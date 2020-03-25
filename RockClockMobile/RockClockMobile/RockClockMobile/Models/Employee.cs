using System;
using System.Collections.Generic;
using System.Text;

namespace RockClockMobile.Models
{
    public class Employee
    {
        public int EmpID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
    }
}
