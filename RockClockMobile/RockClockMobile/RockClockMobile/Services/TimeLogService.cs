using RockClockMobile.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Newtonsoft.Json;

namespace RockClockMobile.Services
{
    
    public class TimeLogService : ITimeLogServices<TimeLog>
    {
        HttpClient client;
        Uri baseAddr;
        IEnumerable<TimeLog> timelogs;

        public TimeLogService()
        {
            baseAddr = new Uri("http://18.136.14.237:8282");
            client = new HttpClient { BaseAddress = baseAddr };
            timelogs = new List<TimeLog>();
        }

        bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;

        public async Task<IEnumerable<TimeLog>> GetEmployeeTimeLogList(bool forceRefresh = false)
        {
            if (forceRefresh && IsConnected)
            {
                var json = await client.GetStringAsync($"api/TimeLog");
                timelogs = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<TimeLog>>(json));
            }

            return timelogs;
        }

        public async Task<TimeLog> GetEmployeeTimeLog(int id)
        {

            if (id != null && IsConnected)
            {
                var json = await client.GetStringAsync($"api/TimeLog/GetTimeLogDataByRocksUserId/{id}");
                return await Task.Run(() => JsonConvert.DeserializeObject<TimeLog>(json));
                //var emptlog = JsonConvert.DeserializeObject<TimeLog>(json);
            }
            return null;
        }

        public async Task<bool> AddEmployeeTimeLog(TimeLog timelog)
        {
            if (timelog == null || !IsConnected)
                return false;

            var serializedItem = JsonConvert.SerializeObject(timelog);

            var response = await client.PostAsync($"api/TimeLog", new StringContent(serializedItem, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;
        }

        public async Task<int> GetTimeLogStatus(int rocksUserID)
        {

            if (rocksUserID != null && IsConnected)
            {
                var json = await client.GetStringAsync($"api/TimeLog/GetTimeLogStatusFromDb/{rocksUserID}");
                return int.Parse(json);
                //var emptlog = JsonConvert.DeserializeObject<TimeLog>(json);
            }
            return -1;
        }

        public async Task<bool> UpdateEmployeeTimeLog(TimeLog timelog)
        {
            if (timelog == null || timelog.id == null || !IsConnected)
                return false;

            var serializedItem = JsonConvert.SerializeObject(timelog);
            //var buffer = Encoding.UTF8.GetBytes(serializedItem);
            //var byteContent = new ByteArrayContent(buffer);

            //var response = await client.PutAsync(new Uri($"api/TimeLog?id={timelog.timeLogId}"), byteContent);
            var response = await client.PutAsync($"api/TimeLog?id={timelog.id}",  new StringContent(serializedItem, Encoding.UTF8, "application/json"));
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> ClockOut(int rocksUserID)
        {
            if (String.IsNullOrEmpty(rocksUserID.ToString()) || !IsConnected)
                return false;


            var json = await client.GetStringAsync($"api/TimeLog/ClockOut/{rocksUserID}");
            return bool.Parse(json);

        }



        public async Task<TimeLog> GetEmployeeBreakLog(int id)
        {
            try
            {

                if (id != null && IsConnected)
                {
                    var json = await client.GetStringAsync($"api/BreakLog/{id}");
                    return await Task.Run(() => JsonConvert.DeserializeObject<TimeLog>(json));
                }
            }
            catch (Exception ex)
            {
                var message = ex.Message;
            }
            return null;
        }

    }
}
