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
        public Command LoadTimeLogsCommand { get; set; }

        public TimeLogViewModel()
        {
            Title = "Browse";
            TimeLogs = new ObservableCollection<TimeLog>();
            LoadTimeLogsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<TimeClockPage, TimeLog>(this, "AddTimeLog", async (obj, timelog) =>
            {
                var newTimelog = timelog as TimeLog;
                TimeLogs.Add(newTimelog);
                await DataStoreTimeLog.AddTimeLogAsync(newTimelog);
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                TimeLogs.Clear();
                var timelogs = await DataStoreTimeLog.GetTimeLogsAsync(true);
                foreach (var timelog in timelogs)
                {
                    TimeLogs.Add(timelog);
                }
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
