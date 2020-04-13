using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Xamarin.Forms.Internals;

namespace RockClockMobile.Models
{
  
    public class Employee
    {
        public string email { get; set; }
        public string userName { get; set; }
        public string firstName { get; set; }
        public string middleName { get; set; }
        public string lastName { get; set; }
        public string urlHash { get; set; }
        public string password { get; set; }
        public string passwordSalt { get; set; }
        public int attempts { get; set; }
        public bool isLocked { get; set; }
        public bool isTempPassword { get; set; }
        public DateTime loginTime { get; set; }
        public DateTime changePasswordTime { get; set; }
        public User user { get; set; }
        public UserRole rocksUserRoleMap { get; set; }
        public List<EmployeeProjectMap> rocksUserProjectMaps { get; set; }
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
