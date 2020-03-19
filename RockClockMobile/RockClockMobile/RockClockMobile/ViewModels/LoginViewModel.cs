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
                EmpID = 1,
                FirstName = "Florencio",
                LastName = "Baroman",
            });
            EmployeeList.Add(new Employee
            {
                EmpID = 2,
                FirstName = "Janno Timothy",
                LastName = "Pono"
            });
            EmployeeList.Add(new Employee
            {
                EmpID = 3,
                FirstName = "Dean Rey",
                LastName = "Cortes"
            });
            EmployeeList.Add(new Employee
            {
                EmpID = 4,
                FirstName = "Kiarah Louise",
                LastName = "Ancajas"
            });
            EmployeeList.Add(new Employee
            {
                EmpID = 5,
                FirstName = "Alexis",
                LastName = "Denolan"
            });
            EmployeeList.Add(new Employee
            {
                EmpID = 6,
                FirstName = "Eliaquim",
                LastName = "Baylon"
            });
            EmployeeList.Add(new Employee
            {
                EmpID = 7,
                FirstName = "Jeremias",
                LastName = "Recamadas"
            });
            EmployeeList.Add(new Employee
            {
                EmpID = 8,
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
