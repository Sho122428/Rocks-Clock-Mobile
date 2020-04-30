using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RockClockMobile.Services
{
    public interface IBreakLogServices<T>
    {
        Task<T> GetEmployeeBreakLog(int timeId, int breakId);
        Task<bool> AddEmployeeBreakLog(int rockUserId);
        Task<bool> UpdateEmployeeBreakLog(T breaklog);
        Task<bool> BreakOut(int rocksUserId);
        Task<IEnumerable<T>> GetEmployeeBreakLogList(bool forceRefresh = false);
    }
}
