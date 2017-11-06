using HomeWork.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HomeWork.Services
{
    public interface IUserService
    {
        Task<UserInfo> GetUserAsync(int id);
        Task<bool> AddUserAsync(RegisterViewModel model);
        Task<IEnumerable<UserInfo>> GetUsersForPageList(int nextpage, int countShowUsers, string sort, string search);
        Task<bool> DeleteUserAsync(int id);
        Task<bool> UpdateUserAsync(EditViewModel user);
        Task<SelectList> GetCitiesAsync(int idCountry, string selected);
        Task<SelectList> GetCountriesAsync(string selected);
        Task<int> CountUsersAsync();
    }
}