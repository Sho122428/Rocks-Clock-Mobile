﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RockClockMobile.Services
{
    public interface IEmployeeServices<T>
    {
        Task<T> GetEmployeeTimeLog(string id);
        Task<bool> UpdateEmployeeAsync(T item);
        Task<T> GetEmployeeAsync(string id);
        Task<IEnumerable<T>> GetEmployeeAsync(bool forceRefresh = false);
    }
}
