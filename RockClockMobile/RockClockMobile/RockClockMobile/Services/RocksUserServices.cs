using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using RockClockMobile.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace RockClockMobile.Services
{
    public class RocksUserServices : IRocksUserServices<RocksUser>
    {
        HttpClient client;
        Uri baseAddr;
        public  ObservableCollection<RocksUser> EmployeeList { get; set; }

        public RocksUserServices()
        {            
            baseAddr = new Uri("http://18.136.14.237:8282");
            client = new HttpClient { BaseAddress = baseAddr }; 
        }
        bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;        

        public async Task<IEnumerable<RocksUser>> GetEmployeeList(bool forceRefresh)
        {
            if (forceRefresh && IsConnected)
            {
                try
                {
                    var accessToken = GlobalServices.AccessToken;
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                    var json = await client.GetStringAsync($"api/RocksUsers");
                    return await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<RocksUser>>(json));
                }
                catch (Exception exc) {
                    //exc.Message.ToString();
                    Crashes.TrackError(exc);
                }              
            }

            return null;
        }

        public async Task<RocksUser> GetEmployeeById(int id)
        {
            if (id != 0 && IsConnected)
            {
                var accessToken = GlobalServices.AccessToken;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var json = await client.GetStringAsync($"api/RocksUsers/{id}?includeProjects=true");
                //var json = await client.GetStringAsync($"api/Account/loginM");
                return await Task.Run(() => JsonConvert.DeserializeObject<RocksUser>(json));
            }

            return null;
        }       
    }

    public class UserLoginParam
    {
        public int RocksUserId { get; set; }
        public string Password { get; set; }
        public bool? Remember { get; set; }
    }
}
