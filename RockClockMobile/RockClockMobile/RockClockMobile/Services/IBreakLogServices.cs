using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RockClockMobile.Services
{
    public interface IBreakLogServices<T>
    {
        Task<T> GetEmployeeBreakLog(int timeId, int breakId);
        Task<bool> AddEmployeeBreakLog(T breaklog);
        Task<bool> UpdateEmployeeBreakLog(T breaklog);
        Task<bool> BreakOut(int timeLogID);
        Task<IEnumerable<T>> GetEmployeeBreakLogList(bool forceRefresh = false);
    }
}
