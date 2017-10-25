using System.Collections.Generic;
using System.Threading.Tasks;
using HomeWork.Models;
using HomeWork.Common;
using System.Data.Entity;
using System.Linq;


namespace HomeWork.Repositories
{
    public class UserRepository : IUserRepository
    {
        public UserRepository() { }

        public async Task AddUserAsync(UserInfo user)
        {
            using (var userContext = new UserContext())
            {
                userContext.Entry(user).State = EntityState.Added;

                await userContext.SaveChangesAsync();
            }
        }

        public async Task DeleteUserAsync(int id)
        {
            using (var userContext = new UserContext())
            {
                var student = await userContext.Users.FirstOrDefaultAsync(u => u.Id == id);

                userContext.Entry(student).State = EntityState.Deleted;

                await userContext.SaveChangesAsync();
            }
        }

        public async Task<UserInfo> GetUserAsync(int id)
        {
            var user = new UserInfo();

            using (var userContext = new UserContext())
            {
                user = await userContext.Users.FirstOrDefaultAsync(u => u.Id == id);
            }
            return user;
        }

        public async Task<IEnumerable<UserInfo>> GetUsersAsync()
        {
            var listUsers = new List<UserInfo>();

            using (var userContext = new UserContext())
            {
                listUsers = await userContext.Users.ToListAsync();
            }
            return listUsers;
        }

        public async Task<IEnumerable<Cities>> GetCitiesAsync(int id)
        {
            IEnumerable<Cities> listCities;

            using (var userContext = new UserContext())
            {
                listCities = await userContext.Cities.Where(c => c.Country_ID == id).ToListAsync();
            }
            return listCities;
        }

        public async Task UpdateUserAsync(UserInfo user)
        {
            using (var userContext = new UserContext())
            {
                userContext.Entry(user).State = EntityState.Modified;

                await userContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Country>> GetCountriesAsync()
        {
            var listCountry = new List<Country>();

            using (var userContext = new UserContext())
            {
                listCountry = await userContext.Country.ToListAsync();
            }
            return listCountry;
        }
    }
}