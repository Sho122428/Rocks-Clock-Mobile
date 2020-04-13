using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace RockClockMobile.Services
{
    public interface IEmployeeServices<T>
    {
        //Task<T> GetEmployeeTimeLog(string id);
        //Task<bool> UpdateEmployeeAsync(T item);
        Task<T> GetEmployeeById(int id);
        Task<IEnumerable<T>> GetEmployeeList(bool forceRefresh = false);
    }
}
