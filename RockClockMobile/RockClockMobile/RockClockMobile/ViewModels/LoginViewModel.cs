using RockClockMobile.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RockClockMobile.ViewModels
{
    public class LoginViewModel
    {
        List<Employee> EmployeeList = new List<Employee>();
        public LoginViewModel() {
            EmployeeList.Add(new Employee
            {
                EmpID = 1001,
                FirstName = "Florencio",
                LastName = "Baroman"
            });
            EmployeeList.Add(new Employee
            {
                EmpID = 1002,
                FirstName = "Janno Timothy",
                LastName = "Pono"
            });
            EmployeeList.Add(new Employee
            {
                EmpID = 1003,
                FirstName = "Dean Rey",
                LastName = "Cortes"
            });
            EmployeeList.Add(new Employee
            {
                EmpID = 1004,
                FirstName = "Kiarah Louise",
                LastName = "Ancajas"
            });
            EmployeeList.Add(new Employee
            {
                EmpID = 1005,
                FirstName = "Alexis",
                LastName = "Denolan"
            });
            EmployeeList.Add(new Employee
            {
                EmpID = 1006,
                FirstName = "Eliaquim",
                LastName = "Baylon"
            });
            EmployeeList.Add(new Employee
            {
                EmpID = 1007,
                FirstName = "Jeremias",
                LastName = "Recamadas"
            });
            EmployeeList.Add(new Employee
            {
                EmpID = 1008,
                FirstName = "Ronie",
                LastName = "Magpantay"
            });
        }

        public List<Employee> Employees()
        {
            return EmployeeList;
        }
    }
}
