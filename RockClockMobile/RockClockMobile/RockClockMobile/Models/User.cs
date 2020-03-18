using System;
using System.Collections.Generic;
using System.Text;

namespace RockClockMobile.Models
{
    public class User
    {
		int ID { get; set; }
		DateTime ModifiedDt { get; set; }
		int ModifiedBy { get; set; }
		DateTime CreatedDt { get; set; }
		int CreatedBy { get; set; }
		string ModifiedNotes { get; set; }
		int Status { get; set; }
		bool isDeleted { get; set; }
		string UrlHash { get; set; }
		string Password { get; set; }
		string PasswordSalt { get; set; }
		int Attempts { get; set; }
		int Locked { get; set; }
		DateTime ConfirmExpire { get; set; }
		int Confirm { get; set; }
		int RocksUserId { get; set; }	
	}
}
