using HomeWork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace HomeWork.Repositories
{
    public interface _IUserRepository
    {
        Task<UserInfo> GetUserAsync(int id);
        Task AddUserAsync(UserInfo user);
        Task<IEnumerable<UserInfo>> GetUsersAsync();
        Task DeleteUserAsync(int id);
        Task UpdateUserAsync(UserInfo user);
        Task<IEnumerable<Cities>> GetCitiesAsync(int id);
        Task<IEnumerable<Country>> GetCountriesAsync();
    }
}