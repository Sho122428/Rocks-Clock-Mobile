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
                FirstName = "Florencio",
                LastName = "Baroman",
            });
            EmployeeList.Add(new Employee
            {
                FirstName = "Janno Timothy",
                LastName = "Pono"
            });
            EmployeeList.Add(new Employee
            {
                FirstName = "Dean Rey",
                LastName = "Cortes"
            });
            EmployeeList.Add(new Employee
            {
                FirstName = "Kiarah Louise",
                LastName = "Ancajas"
            });
            EmployeeList.Add(new Employee
            {
                FirstName = "Alexis",
                LastName = "Denolan"
            });
            EmployeeList.Add(new Employee
            {
                FirstName = "Eliaquim",
                LastName = "Baylon"
            });
            EmployeeList.Add(new Employee
            {
                FirstName = "Jeremias",
                LastName = "Recamadas"
            });
            EmployeeList.Add(new Employee
            {
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
