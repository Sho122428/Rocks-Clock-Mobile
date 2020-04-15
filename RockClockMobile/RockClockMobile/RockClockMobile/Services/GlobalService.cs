using RockClockMobile.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RockClockMobile.Services
{
    public static class GlobalServices
    {
        public static Employee employee { get; set; }
        public static IEnumerable<Employee> employeeList { get; set; }
        public static User User { get; set; }
        public static List<TimeLog> EmployeeTime { get; set; }
        public static List<BreakLog> EmployeeBreak { get; set; }
        public static string AccessToken { get; set; }
    }
}
