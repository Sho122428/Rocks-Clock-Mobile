using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RockClockMobile.Services
{
    public interface IDataStoreTimeLog<T>
    {
        Task<bool> AddTimeLogAsync(T timelog);
        Task<bool> UpdateTimeLogAsync(T timelog);
        Task<bool> DeleteTimeLogAsync(int timeid);
        Task<T> GetTimeLogAsync(int timeid);
        Task<IEnumerable<T>> GetTimeLogsAsync(bool forceRefresh = false);
    }
   
}
