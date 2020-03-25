using RockClockMobile.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace RockClockMobile.ViewModels
{
    public class LoginViewModel
    {
        public ObservableCollection<Employee> EmployeeList { get; set; }

        //List<Employee> EmployeeList = new List<Employee>();
        public LoginViewModel() {
            EmployeeList = new ObservableCollection<Employee>();

            EmployeeList.Add(new Employee
            {
                EmpID = 1,
                FirstName = "Florencio",
                LastName = "Baroman",
                ProjectId = 1,
                ProjectName = "Project1"
            });
            EmployeeList.Add(new Employee
            {
                EmpID = 2,
                FirstName = "Janno Timothy",
                LastName = "Pono",
                ProjectId = 2,
                ProjectName = "Project2"
            });
            EmployeeList.Add(new Employee
            {
                EmpID = 3,
                FirstName = "Dean Rey",
                LastName = "Cortes",
                ProjectId = 3,
                ProjectName = "Project3"
            });
            EmployeeList.Add(new Employee
            {
                EmpID = 4,
                FirstName = "Kiarah Louise",
                LastName = "Ancajas",
                ProjectId = 4,
                ProjectName = "Project4"
            });
            EmployeeList.Add(new Employee
            {
                EmpID = 5,
                FirstName = "Alexis",
                LastName = "Denolan",
                ProjectId = 5,
                ProjectName = "Project5"
            });
            EmployeeList.Add(new Employee
            {
                EmpID = 6,
                FirstName = "Eliaquim",
                LastName = "Baylon",
                ProjectId = 6,
                ProjectName = "Project6"
            });
            EmployeeList.Add(new Employee
            {
                EmpID = 7,
                FirstName = "Jeremias",
                LastName = "Recamadas",
                ProjectId = 7,
                ProjectName = "Project7"
            });
            EmployeeList.Add(new Employee
            {
                EmpID = 8,
                FirstName = "Ronie",
                LastName = "Magpantay",
                ProjectId = 8,
                ProjectName = "Project8"
            });
            EmployeeList.Add(new Employee
            {
                EmpID = 9,
                FirstName = "Christian",
                LastName = "Cabang",
                ProjectId = 9,
                ProjectName = "Project9"
            });
            EmployeeList.Add(new Employee
            {
                EmpID = 10,
                FirstName = "Randy",
                LastName = "Pacatang",
                ProjectId = 10,
                ProjectName = "Project10"
            });

        }
         
        //public List<Employee> Employees()
        //{
        //    return EmployeeList;
        //}

     

        //public List<Employee> GetEmployee() {
        //    List<Employee> EmployeeList = new List<Employee>();

        //    foreach (var dtl in Employees().OrderBy(a => a.FirstName))
        //    {
        //        dtl.FullName = $"{dtl.FirstName} {dtl.LastName}";
        //        EmployeeList.Add(new Employee
        //        {
        //            FullName = dtl.FullName
        //        });
        //    }

        //    return EmployeeList;
        //}
    }
}
