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
        Uri baseAddr;
        public ObservableCollection<EmpSample> EmpSamples { get; set; }
        public  ObservableCollection<Employee> EmployeeList { get; set; }
        IEnumerable<TimeLog> timelogs;

        public EmployeeServices() {
            
            baseAddr = new Uri("http://18.136.14.237:8282");
            client = new HttpClient { BaseAddress = baseAddr };


            timelogs = new List<TimeLog>();
            Employees();
           
        }
        bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;
        public async Task<ObservableCollection<Employee>> Employees()
        {
            try
            {
                EmployeeList = new ObservableCollection<Employee>();
                //var baseAddr = new Uri("http://18.136.14.237:8282");
                //var client = new HttpClient { BaseAddress = baseAddr };


                var response = await client.GetStringAsync($"{baseAddr}api/RocksUsers");
                var empFromAPI = await Task.Run(() => JsonConvert.DeserializeObject<ObservableCollection<EmpSample>>(response));

                EmpSamples = empFromAPI;

                foreach (var dtl in EmpSamples)
                {

                    EmployeeList.Add(new Employee
                    {
                        rocksUserId = dtl.id,
                        FirstName = dtl.firstName,
                        LastName = dtl.lastName,
                        email = dtl.email,
                        rocksProjects = dtl.rocksUserProjectMaps
                    });

                }

                return EmployeeList;
            }
            catch (System.Net.WebException e)
            {
                e.ToString();
            }

            return null;
        }

        public async Task<IEnumerable<EmpSample>> GetRocksUsers(bool forceRefresh)
        {
            if (forceRefresh && IsConnected)
            {
                var json = await client.GetStringAsync($"{baseAddr}api/RocksUsers");
                return await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<EmpSample>>(json));
            }

            return null;
        }

    }    
}
