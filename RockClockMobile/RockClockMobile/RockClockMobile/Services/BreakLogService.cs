using Newtonsoft.Json;
using RockClockMobile.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace RockClockMobile.Services
{
    public class BreakLogService : IBreakLogServices<BreakLog>
    {
        HttpClient client;
        Uri baseAddr;
        IEnumerable<TimeLog> timelogs;
        IEnumerable<BreakLog> breaklogs;

        public BreakLogService()
        {
            baseAddr = new Uri("http://18.136.14.237:8282");
            client = new HttpClient { BaseAddress = baseAddr };
            timelogs = new List<TimeLog>();
        }
        bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;

        public async Task<bool> AddEmployeeBreakLog(int rocksUserId)
        {
            if (rocksUserId == 0 || !IsConnected)
                return false;

            var accessToken = GlobalServices.AccessToken;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            //var serializedItem = JsonConvert.SerializeObject(breaklog);
            //var response = await client.PostAsync($"api/BreakLog", new StringContent(serializedItem, Encoding.UTF8, "application/json"));

            var response = await client.PostAsync($"api/timelog/BreakIn?rocksUserId={ rocksUserId }", null);

            return response.IsSuccessStatusCode;
        }

        public async Task<BreakLog> GetEmployeeBreakLog(int timeId, int breakId)
        {
            if (String.IsNullOrEmpty(timeId.ToString()) && String.IsNullOrEmpty(breakId.ToString()) && IsConnected)
            {
                var accessToken = GlobalServices.AccessToken;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var json = await client.GetStringAsync($"{baseAddr}/api/BreakLog/{breakId}");
                return await Task.Run(() => JsonConvert.DeserializeObject<BreakLog>(json));
            }
            return null;
        }

        public async Task<IEnumerable<BreakLog>> GetEmployeeBreakLogList(bool forceRefresh = false)
        {
            if (forceRefresh && IsConnected)
            {
                var accessToken = GlobalServices.AccessToken;
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var json = await client.GetStringAsync($"api/BreakLog");
                breaklogs = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<BreakLog>>(json));
            }

            return breaklogs;
        }

        public async Task<bool> UpdateEmployeeBreakLog(BreakLog breaklog)
        {
            if (breaklog == null || breaklog.id == 0 || !IsConnected)
                return false;

            var accessToken = GlobalServices.AccessToken;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var serializedItem = JsonConvert.SerializeObject(breaklog);
            var response = await client.PutAsync($"api/BreakLog?id={breaklog.id}", new StringContent(serializedItem, Encoding.UTF8, "application/json"));
            
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> BreakOut(int rocksUserId)
        {
            if (String.IsNullOrEmpty(rocksUserId.ToString()) || !IsConnected)
                return false;

            var accessToken = GlobalServices.AccessToken;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await client.PostAsync($"api/timelog/BreakOut?rocksUserId={ rocksUserId }", null);

            return response.IsSuccessStatusCode;
        }
    }
}
