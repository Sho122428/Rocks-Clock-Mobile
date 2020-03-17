using RockClockMobile.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RockClockMobile.ViewModels
{
    public class UserViewModel
    {
        List<User> Users = new List<User>();
        public List<User> Employees { get { return Users; } }
        public UserViewModel() {
            Users.Add(new User
            {
                FirstName = "Florencio",
                LastName = "Baroman",
            });
            Users.Add(new User
            {
                FirstName = "Janno Timothy",
                LastName = "Pono"
            });
            Users.Add(new User
            {
                FirstName = "Dean Rey",
                LastName = "Cortes"
            });
            Users.Add(new User
            {
                FirstName = "Kiarah Louise",
                LastName = "Ancajas"
            });
            Users.Add(new User
            {
                FirstName = "Alexis",
                LastName = "Denolan"
            });
            Users.Add(new User
            {
                FirstName = "Eliaquim",
                LastName = "Baylon"
            });
            Users.Add(new User
            {
                FirstName = "Jeremias",
                LastName = "Recamadas"
            });
            Users.Add(new User
            {
                FirstName = "Ronie",
                LastName = "Magpantay"
            });
        }
    }
}
