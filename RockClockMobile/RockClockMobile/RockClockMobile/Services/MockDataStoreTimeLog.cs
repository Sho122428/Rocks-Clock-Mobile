using RockClockMobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockClockMobile.Services
{
    public class MockDataStoreTimeLog : IDataStoreTimeLog<TimeLog>
    {
        readonly List<TimeLog> timelogs;
        public MockDataStoreTimeLog()
        {
            timelogs = new List<TimeLog>()
            {
                
            };
        }

        public async Task<bool> AddTimeLogAsync(TimeLog timelog)
        {
            timelogs.Add(timelog);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteTimeLogAsync(int timeid)
        {
            var oldItem = timelogs.Where((TimeLog arg) => arg.timeLogId == timeid).FirstOrDefault();
            timelogs.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<TimeLog> GetTimeLogAsync(int timeid)
        {
            return await Task.FromResult(timelogs.FirstOrDefault(s => s.timeLogId == timeid));
        }

        public async Task<IEnumerable<TimeLog>> GetTimeLogsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(timelogs);
        }

        public async Task<bool> UpdateTimeLogAsync(TimeLog timelog)
        {
            var oldItem = timelogs.Where((TimeLog arg) => arg.timeLogId == timelog.timeLogId).FirstOrDefault();
            timelogs.Remove(oldItem);
            timelogs.Add(timelog);

            return await Task.FromResult(true);
        }
    }
}
