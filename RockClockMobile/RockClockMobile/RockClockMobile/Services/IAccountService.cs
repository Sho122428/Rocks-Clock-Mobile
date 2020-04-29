using RockClockMobile.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RockClockMobile.Services
{
    public interface IAccountService<T>
    {
        Task<bool> AdminUserLogin(UserLogin userLogin);
        Task<UserLoginM> UserLogin(UserLoginParam userLoginParam);
        Task<bool> ChangePassword(ChangePasswordVM changePasswordVM);
    }
}
