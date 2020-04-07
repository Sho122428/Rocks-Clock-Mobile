using RockClockMobile.Models;
using RockClockMobile.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace RockClockMobile.ViewModels
{
    public class UserViewModel:BaseViewModel
    {
        public UserViewModel()
        {
        }

        public ICommand GetUserCommand { get; set; }
        public User User {get; set; }
        public async Task<User> GetUser()
        {
            IsBusy = true;

            var gg = GlobalServices.employee;

            try
            {
                User = await UserServices.GetUser(gg.id);
                return User;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }

            return User;
        }
        private async Task AddEmployeeTimeLog()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                var countTimeID = 0;

                //if (empUserLog != null)
                //    countTimeID = empUserLog.Count;

                //var userTimeLog = new TimeLog
                //{
                //    //timeLogId = countTimeID + 1,
                //    //rocksUserId = empDtl.rocksUserId,
                //    //timeIn = DateTime.Now,
                //    //projectID = 1,
                //    //status = 0,
                //    //isDeleted = false


                //    projectID = 4,
                //    rocksUserId = empDtl.EmpID,
                //    timeIn = DateTime.UtcNow,
                //    timeOut = DateTime.UtcNow,
                //    status = 1,
                //    isDeleted = false

                //};
                //var items = await TimeLogServices.AddEmployeeTimeLog(userTimeLog);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
                //this.SignOut();
            }
        }

        private async Task UpdateEmployeeTimeLog()
        {

        }
    }
}
