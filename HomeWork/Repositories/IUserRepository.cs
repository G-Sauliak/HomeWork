using HomeWork.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HomeWork.Repositories
{
    public interface IUserRepository : IDisposable
    {
        Task<UserInfo> FindUserAsync(int id);
        IQueryable<UserInfo> Get { get; }
        Task AddAsync(UserInfo obj);
        Task DeleteAsync(int id);
        Task UpdateAsync(UserInfo obj);
        IQueryable<Cities> GetCities();
        IQueryable<Country> GetCountries();
    }
}