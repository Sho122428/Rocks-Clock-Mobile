using RockClockMobile.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RockClockMobile.Enums;

namespace RockClockMobile.Services
{
    public interface ITimeLogServices<T>
    {
        Task<T> GetEmployeeTimeLog(int id);
        Task<bool> AddEmployeeTimeLog(T timelog);
        Task<bool> UpdateEmployeeTimeLog(T timelog);
        Task<T> GetEmployeeBreakLog(int id);
        Task<IEnumerable<T>> GetEmployeeTimeLogList(bool forceRefresh = false);
        Task<TimeLogVM> GetTimeLogStatus(int rocksUserID);
        Task<bool> ClockIn(int projectId,int rocksUserID,T timelog);
        Task<bool> ClockOut(int id);
        Task<ButtonAccess> GetTimeLogButtonAccess(int rocksUserID);
    }
}
