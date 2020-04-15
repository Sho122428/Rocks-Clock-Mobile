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
    public class EmployeeServices : IEmployeeServices<Employee>
    {
        HttpClient client;
        Uri baseAddr;
        public  ObservableCollection<Employee> EmployeeList { get; set; }

        public EmployeeServices()
        {            
            baseAddr = new Uri("http://18.136.14.237:8282");
            client = new HttpClient { BaseAddress = baseAddr }; 
        }
        bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;        

        public async Task<IEnumerable<Employee>> GetEmployeeList(bool forceRefresh)
        {
            if (forceRefresh && IsConnected)
            {
                try
                {
                    var accessToken = GlobalServices.AccessToken;
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                    var json = await client.GetStringAsync($"api/RocksUsers");
                    return await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<Employee>>(json));
                }
                catch (Exception exc) {
                    exc.Message.ToString();
                }              
            }

            return null;
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            if (id != 0 && IsConnected)
            {
                var accessToken = GlobalServices.AccessToken;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var json = await client.GetStringAsync($"api/RocksUsers/{id}?includeProjects=true");
                //var json = await client.GetStringAsync($"api/Account/loginM");
                return await Task.Run(() => JsonConvert.DeserializeObject<Employee>(json));
            }

            return null;
        }

        public async Task<UserLoginM> UserLoginById(UserLoginParam userLoginParam)
        {
            if (userLoginParam == null || !IsConnected)
                return null;

            var accessToken = GlobalServices.AccessToken;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var serializedItem = JsonConvert.SerializeObject(userLoginParam);

            var buffer = System.Text.Encoding.UTF8.GetBytes(serializedItem);
            var byteContent = new ByteArrayContent(buffer);

            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var result = client.PostAsync($"api/Account/loginM", byteContent).Result;

            var readResponse = await result.Content.ReadAsStringAsync();

            var convertResponse = JsonConvert.DeserializeObject<UserLoginM>(readResponse);

            return convertResponse;
        }        
    }

    public class UserLoginParam
    {
        public int RocksUserId { get; set; }
        public string Password { get; set; }
        public bool? Remember { get; set; }
    }
}
