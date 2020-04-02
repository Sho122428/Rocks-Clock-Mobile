using Newtonsoft.Json;
using RockClockMobile.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RockClockMobile.Services
{
    public class UserServices
    {
        HttpClient client;
        Uri baseAddr;
        public User UserList { get; set; }
        public UserServices(int rockUserId) {
            baseAddr = new Uri("http://18.136.14.237:8282");
            client = new HttpClient { BaseAddress = baseAddr };

            Users(rockUserId);
        }
        public async  Task<User> Users(int rockUserId)
        {
            try
            {
                var response = await client.GetStringAsync($"{baseAddr}api/Users/{ rockUserId }");
                UserList = await Task.Run(() => JsonConvert.DeserializeObject<User>(response));
                //UserList = JsonConvert.DeserializeObject<User>(response);

                if (UserList != null) {
                    GlobalServices.User = UserList;
                }               

                return UserList;
            }
            catch (System.Net.WebException e)
            {
                e.ToString();
            }

            return null;
        }
    }
}
