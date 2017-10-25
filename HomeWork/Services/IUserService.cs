using HomeWork.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HomeWork.Services
{
    public interface IUserService
    {
        Task<UserInfo> GetUserAsync(int id);
        Task AddUserAsync(RegisterViewModel model);
        Task<SelectList> GetUsersAsync(string dataValueField, string dataTextField);
        Task DeleteUserAsync(int id);
        Task UpdateUserAsync(EditViewModel user);
        Task<SelectList> GetCitiesAsync(int idCountry, string dataValueField, string dataTextField);
        Task<SelectList> GetCountriesAsync();
    }
}