using RockClockMobile.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RockClockMobile.ViewModels
{
    public class LoginViewModel
    {
        UserViewModel userViewModel = new UserViewModel();

        public List<User> Users()
        {
            return userViewModel.Employees;
        }
    }
}
