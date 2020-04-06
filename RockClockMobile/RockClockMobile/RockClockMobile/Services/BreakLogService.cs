using RockClockMobile.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RockClockMobile.Services
{
    public class BreakLogService : IBreakLogServices<BreakLog>
    {
        public Task<bool> AddEmployeeBreakLog(int timeId, BreakLog breaklog)
        {
            throw new NotImplementedException();
        }

        public Task<BreakLog> GetEmployeeBreakLog(int timeId, int breakId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BreakLog>> GetEmployeeBreakLogList(bool forceRefresh = false)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateEmployeeBreakLog(BreakLog breaklog)
        {
            throw new NotImplementedException();
        }
    }
}
