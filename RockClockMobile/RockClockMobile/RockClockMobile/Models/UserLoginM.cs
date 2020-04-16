using System;
using System.Collections.Generic;
using System.Text;

namespace RockClockMobile.Models
{
    public class UserLoginM
    {
        public Employee rocksUser { get; set; } = new Employee();
        public string result { get; set; } = "Success";
        public string message { get; set; }
    }
}
