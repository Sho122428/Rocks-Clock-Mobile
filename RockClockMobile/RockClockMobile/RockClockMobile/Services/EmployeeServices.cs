using Newtonsoft.Json;
using RockClockMobile.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
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
                var json = await client.GetStringAsync($"api/RocksUsers");
                return await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<Employee>>(json));
            }

            return null;
        }
    }    
}
