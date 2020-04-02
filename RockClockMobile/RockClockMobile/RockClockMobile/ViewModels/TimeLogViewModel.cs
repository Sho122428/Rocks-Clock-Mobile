using RockClockMobile.Models;
using RockClockMobile.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RockClockMobile.ViewModels
{
    public class TimeLogViewModel : BaseViewModel
    {
        public ObservableCollection<TimeLog> TimeLogs { get; set; }
        public TimeLog EmployeeTimeLog { get; set; }
        public Command LoadTimeLogsCommand { get; set; }
        public Command LoadEmployeeTimeLog { get; set; }

        public TimeLogViewModel()
        {
            Title = "Browse";
            TimeLogs = new ObservableCollection<TimeLog>();
            EmployeeTimeLog = new TimeLog();
            LoadTimeLogsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            LoadEmployeeTimeLog = new Command(async () => await ExecuteLoadEmployeeTimeLog(EmployeeTimeLog));

            MessagingCenter.Subscribe<TimeClockPage, TimeLog>(this, "AddTimeLog", async (obj, timelog) =>
            {
                var newTimelog = timelog as TimeLog;
                TimeLogs.Add(newTimelog);
                //await DataStoreTimeLog.AddTimeLogAsync(newTimelog);
            });
        }

        /*
         *ExecuteLoadItemsCommand
         *Loads all saved time logs data
         **/

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                TimeLogs.Clear();
                //var timelogs = await DataStoreTimeLog.GetTimeLogsAsync(true);
                //foreach (var timelog in timelogs)
                //{
                //    TimeLogs.Add(timelog);
                //}
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        /*
         * Loads employee time log
         * Receives employee's ID as parameter
         */
        async Task ExecuteLoadEmployeeTimeLog(TimeLog employeeTimeLog)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                //TimeLogs.Clear();
                //var timelogs = await DataStoreTimeLog.GetTimeLogsAsync(true);
                //foreach (var timelog in timelogs)
                //{
                //    if(timelog.rocksUserId == employeeTimeLog.rocksUserId)
                //    {
                //        EmployeeTimeLog = await DataStoreTimeLog.GetTimeLogAsync(employeeTimeLog.timeLogId);
                //    }
                    
                //}
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
