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
                new TimeLog { TimeId = 1, rocksUserID = 1001, projectID = 111, TimeIn = DateTime.Parse("14:00"), TimeOut = DateTime.Parse("23:00")},
                new TimeLog { TimeId = 2, rocksUserID = 1002, projectID = 112, TimeIn = DateTime.Parse("08:00"), TimeOut = DateTime.Parse("17:00")}
            };
        }

        public async Task<bool> AddTimeLogAsync(TimeLog timelog)
        {
            timelogs.Add(timelog);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteTimeLogAsync(int timeid)
        {
            var oldItem = timelogs.Where((TimeLog arg) => arg.TimeId == timeid).FirstOrDefault();
            timelogs.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<TimeLog> GetTimeLogAsync(int timeid)
        {
            return await Task.FromResult(timelogs.FirstOrDefault(s => s.TimeId == timeid));
        }

        public async Task<IEnumerable<TimeLog>> GetTimeLogsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(timelogs);
        }

        public async Task<bool> UpdateTimeLogAsync(TimeLog timelog)
        {
            var oldItem = timelogs.Where((TimeLog arg) => arg.TimeId == timelog.TimeId).FirstOrDefault();
            timelogs.Remove(oldItem);
            timelogs.Add(timelog);

            return await Task.FromResult(true);
        }
    }
}
