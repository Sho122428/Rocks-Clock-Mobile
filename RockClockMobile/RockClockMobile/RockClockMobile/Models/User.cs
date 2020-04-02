using System;
using System.Collections.Generic;
using System.Text;

namespace RockClockMobile.Models
{
    public class User
    {
        public string urlHash { get; set; }
        public string password { get; set; }
        public string passwordSalt { get; set; }
        public int attempts { get; set; }
        public bool isLocked { get; set; }
        public bool isTempPassword { get; set; }
        public DateTime changePasswordTime { get; set; }
        public int rocksUserId { get; set; }
        public UserRole userRole { get; set; }
        public int id { get; set; }
        public DateTime modifieddt { get; set; }
        public int modifiedby { get; set; }
        public DateTime createddt { get; set; }
        public int createdby { get; set; }
        public string modifiednotes { get; set; }
        public int status { get; set; }
        public bool isDeleted { get; set; }
    }
}
