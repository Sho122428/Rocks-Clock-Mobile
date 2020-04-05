using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RockClockMobile.Services
{
    public interface IUserServices<T>
    {
        Task<T> GetUser(int id);
        Task<bool> AddUser(T user);
        Task<bool> UpdateUser(T timelog);
        //Task<IEnumerable<T>> GetEmployeeTimeLogList(bool forceRefresh = false);
    }
}
