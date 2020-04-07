using Newtonsoft.Json;
using RockClockMobile.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
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

        public async Task<bool> AddEmployeeBreakLog(int timeId, BreakLog breaklog)
        {
            if (breaklog == null || !IsConnected)
                return false;

            var serializedItem = JsonConvert.SerializeObject(breaklog);

            var response = await client.PostAsync($"api/BreakLog", new StringContent(serializedItem, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;
        }

        public async Task<BreakLog> GetEmployeeBreakLog(int timeId, int breakId)
        {
            if (String.IsNullOrEmpty(timeId.ToString()) && String.IsNullOrEmpty(breakId.ToString()) && IsConnected)
            {
                var json = await client.GetStringAsync($"{baseAddr}/api/BreakLog/{breakId}");
                return await Task.Run(() => JsonConvert.DeserializeObject<BreakLog>(json));
                //var emptlog = JsonConvert.DeserializeObject<TimeLog>(json);
            }
            return null;
        }

        public async Task<IEnumerable<BreakLog>> GetEmployeeBreakLogList(bool forceRefresh = false)
        {
            if (forceRefresh && IsConnected)
            {
                var json = await client.GetStringAsync($"api/BreakLog");
                breaklogs = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<BreakLog>>(json));
            }

            return breaklogs;
        }

        public async Task<bool> UpdateEmployeeBreakLog(BreakLog breaklog)
        {
            if (breaklog == null || breaklog.id == null || !IsConnected)
                return false;

            var serializedItem = JsonConvert.SerializeObject(breaklog);
            var buffer = Encoding.UTF8.GetBytes(serializedItem);
            var byteContent = new ByteArrayContent(buffer);

            var response = await client.PutAsync(new Uri($"api/BreakLog?id={breaklog.timeLogId}"), byteContent);

            return response.IsSuccessStatusCode;
        }
    }
}
