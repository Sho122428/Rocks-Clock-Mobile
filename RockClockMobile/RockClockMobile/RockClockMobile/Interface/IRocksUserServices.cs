using RockClockMobile.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace RockClockMobile.Services
{
    public interface IRocksUserServices<T>
    {
        Task<T> GetEmployeeById(int id);      
        Task<IEnumerable<T>> GetEmployeeList(bool forceRefresh = false);
    }
}
