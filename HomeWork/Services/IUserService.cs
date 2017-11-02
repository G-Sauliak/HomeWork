using HomeWork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HomeWork.Services
{
    public interface IUserService
    {
        Task <UserInfo> GetUserAsync(int id);
        Task<bool> AddUserAsync(RegisterViewModel model);
        Task<IEnumerable<UserInfo>> GetUsersForPageList(int nextpage, int countShowUsers);
        Task<bool> DeleteUserAsync(int id);
        Task<bool> UpdateUserAsync(EditViewModel user);
        Task<SelectList> GetCitiesAsync(int idCountry);
        Task<SelectList> GetCountriesAsync();
        Task<int> CountUsers();
    }
}