using System;
using System.Collections.Generic;
using System.Text;

namespace RockClockMobile.Models
{
    public class UserLoginResponse
    {
        public Employee user { get; set; } = new Employee();
        public string result { get; set; } = "Success";
        public IEnumerable<string> roles { get; set; } = null;
        public string token { get; set; }
        public string message { get; set; }
        public bool rememberPassword { get; set; } = false;
    }
}
