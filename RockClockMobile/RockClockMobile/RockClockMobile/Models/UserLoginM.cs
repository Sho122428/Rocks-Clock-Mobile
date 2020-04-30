using System;
using System.Collections.Generic;
using System.Text;

namespace RockClockMobile.Models
{
    public class UserLoginM
    {
        public RocksUser rocksUser { get; set; } = new RocksUser();
        public string result { get; set; } = "Success";
        public string message { get; set; }
    }
}
