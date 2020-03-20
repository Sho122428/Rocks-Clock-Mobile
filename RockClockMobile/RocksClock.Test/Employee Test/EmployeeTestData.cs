using RockClockMobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RocksClock.Test.Employee_Test
{
    public class EmployeeTestData
    {
        public static IEnumerable<Employee> GetSampleBreakLogs(bool hasData)
        {
            if (hasData == false)
                return new List<Employee>().AsEnumerable();

            return new List<Employee>
            {
                new Employee
                {
                    EmpID = 1,
                    FirstName = "test1",
                    LastName = "test1",
                    FullName = "Test1 test1"
                },
                new Employee
                {
                    EmpID = 2,
                    FirstName = "test2",
                    LastName = "test2",
                    FullName = "Test2 test2"
                }
            };
        }
    }
}
