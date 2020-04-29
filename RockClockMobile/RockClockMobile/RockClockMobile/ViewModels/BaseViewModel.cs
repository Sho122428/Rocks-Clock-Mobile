using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;

using RockClockMobile.Models;
using RockClockMobile.Services;
using System.Runtime.Serialization;

namespace RockClockMobile.ViewModels
{
    [DataContract]
    public class BaseViewModel : INotifyPropertyChanged
    {
        public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>();
        
        public ITimeLogServices<TimeLog> TimeLogServices => DependencyService.Get<ITimeLogServices<TimeLog>>();
        public IBreakLogServices<BreakLog> BreakLogServices => DependencyService.Get<IBreakLogServices<BreakLog>>();
        public IUserServices<User> UserServices => DependencyService.Get<IUserServices<User>>();
        public IEmployeeServices<Employee> EmployeeServices => DependencyService.Get<IEmployeeServices<Employee>>();
        public IAccountService<ChangePasswordVM> AccountService => DependencyService.Get<IAccountService<ChangePasswordVM>>();

        bool isBusy = false;
        double isBusyOpacity = 1;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        public double IsBusyOpacity
        {
            get { return isBusyOpacity; }
            set { SetProperty(ref isBusyOpacity, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
