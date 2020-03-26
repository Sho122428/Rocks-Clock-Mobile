using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Xamarin.Forms.Internals;

namespace RockClockMobile.Models
{
    [Preserve(AllMembers = true)]
    [DataContract]
    public class Employee
    {
        [DataMember(Name = "EmpID")]
        public int EmpID { get; set; }

        [DataMember(Name = "FirstName")]
        public string FirstName { get; set; }

        [DataMember(Name = "LastName")]
        public string LastName { get; set; }

        [DataMember(Name = "FullName")]
        public string FullName { get; set; }

        [DataMember(Name = "ProjectId")]
        public int ProjectId { get; set; }

        [DataMember(Name = "ProjectName")]
        public string ProjectName { get; set; }
    }
}
