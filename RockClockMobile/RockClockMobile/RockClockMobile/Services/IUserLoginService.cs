using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RockClockMobile.Services
{
    public interface IUserLoginService<T>
    {
        Task<bool> AddUserLogin(T userLogin);
    }
}
