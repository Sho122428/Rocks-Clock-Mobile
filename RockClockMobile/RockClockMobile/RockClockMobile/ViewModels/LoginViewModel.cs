using RockClockMobile.Models;
using RockClockMobile.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace RockClockMobile.ViewModels
{
    public class LoginViewModel
    {
        EmployeeServices employeeServices = new EmployeeServices();

        public LoginViewModel()
        {
            NamesList = employeeServices.EmployeeList;
        }

        public ObservableCollection<Employee> NamesList { get; set; }
    }
}
