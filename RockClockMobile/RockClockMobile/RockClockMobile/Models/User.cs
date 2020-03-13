using System;
using System.Collections.Generic;
using System.Text;

namespace RockClockMobile.Models
{
    public class User
    {
        int id;
        string firstName;
        string lastName;
        DateTime dateOfBirth;
        bool isActive;
        bool isDeleted;
        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }
        public DateTime DateOfBirth
        {
            get { return dateOfBirth; }
            set { dateOfBirth = value; }
        }
        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }
        public bool IsDeleted
        {
            get { return isDeleted; }
            set { isDeleted = value; }
        }
    }
}
