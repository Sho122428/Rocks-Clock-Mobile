using Newtonsoft.Json;
using RockClockMobile.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace RockClockMobile.Services
{
    public class UserServices : IUserServices<User>
    {
        HttpClient client;
        Uri baseAddr;

        public UserServices() 
        {
            baseAddr = new Uri("http://18.136.14.237:8282");
            client = new HttpClient { BaseAddress = baseAddr };
        }

        bool isConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;

        public async Task<User> GetUserList(bool refresh, int userId)
        {
            if (isConnected && refresh)
            {                
                //var json = await client.GetStringAsync($"api/Account/GetUserBy/{ userId }");
                var json = await client.GetStringAsync($"api/Account");
                var UsersToJson = JsonConvert.DeserializeObject<List<User>>(json);
                var userLoggedIn = UsersToJson.Where(a => a.rocksUserId == userId).FirstOrDefault();
                return await Task.Run(() => userLoggedIn);
            }
            return null;
        }
        public async Task<User> GetUser(int id)
        {

            if (id != 0 && isConnected)
            {
                var json = await client.GetStringAsync($"{baseAddr}api/Users/{id}");
                return await Task.Run(() => JsonConvert.DeserializeObject<User>(json));
            }
            return null;
        }

        public async Task<bool> AddUser(User user)
        {
            if (user == null || !isConnected)
                return false;

            var serializedItem = JsonConvert.SerializeObject(user);

            var response = await client.PostAsync($"api/Users", new StringContent(serializedItem, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateUser(User user)
        {
            if (user == null || !isConnected)
                return false;

            var serializedItem = JsonConvert.SerializeObject(user);
            var buffer = Encoding.UTF8.GetBytes(serializedItem);
            var byteContent = new ByteArrayContent(buffer);

            var response = await client.PutAsync(new Uri($"api/Users/{user.userRole.userId}"), byteContent);

            return response.IsSuccessStatusCode;
        }
    }
}
