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
    public class EmployeeServices
    {
        HttpClient client;
        public ObservableCollection<EmpSample> EmpSamples { get; set; }
        public  ObservableCollection<Employee> EmployeeList { get; set; }

        public EmployeeServices() {          
            Employees();

            client = new HttpClient();
            client.BaseAddress = new Uri($"http://18.136.14.237:8282/");
        }
        bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;
        public async Task<ObservableCollection<Employee>> Employees()
        {
            try
            {
                EmployeeList = new ObservableCollection<Employee>();
                //var baseAddr = new Uri("http://18.136.14.237:8282");
                //var client = new HttpClient { BaseAddress = baseAddr };


                var response = await client.GetStringAsync($"api/RocksUsers");
                var empFromAPI = JsonConvert.DeserializeObject<ObservableCollection<EmpSample>>(response);

                EmpSamples = empFromAPI;

                foreach (var dtl in EmpSamples) {
                    
                    EmployeeList.Add(new Employee
                    {
                        rocksUserId = dtl.rocksUserId,
                        FirstName = dtl.firstName,
                        LastName = dtl.lastName,
                        email = dtl.email
                        //rocksProjects = dtl.rocksProjects
                    }) ;

                }

                return EmployeeList;
            }
            catch (System.Net.WebException e)
            {
                 e.ToString();
            }

            return null;
        }

        public async Task<TimeLog> GetEmployeeTimeLog(string id)
        {
            if (id != null && IsConnected)
            {
                var json = await client.GetStringAsync($"api/item/{id}");
                return await Task.Run(() => JsonConvert.DeserializeObject<TimeLog>(json));
            }

            return null;
        }
    }    
}
