using System;
using System.Collections.Generic;
using System.Text;

namespace RockClockMobile.Models
{
    public class User
    {
        string username;
        string password;
        public string Username 
        {
            get { return username; }
            set { username = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }
    }
}
