using HomeWork.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using HomeWork.Common;
using System.Data.Entity;

namespace HomeWork.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext context;

        public UserRepository(UserContext context)
        {
            this.context = context;
        }

        public async Task AddAsync(UserInfo item)
        {
            context.Entry(item).State = EntityState.Added;

            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var student = await context.Users.FirstOrDefaultAsync(u => u.Id == id);

            context.Entry(student).State = EntityState.Deleted;

            await context.SaveChangesAsync();
        }

        public IQueryable<UserInfo> Get
        {
            get { return context.Users.AsNoTracking().AsQueryable(); }
        }

        public async Task UpdateAsync(UserInfo user)
        {
            context.Entry(user).State = EntityState.Modified;

            await context.SaveChangesAsync();
        }

        public IQueryable<Cities> GetCities()
        {
            return context.Cities.AsQueryable();
        }

        public IQueryable<Country> GetCountries()
        {
            return context.Country.AsQueryable();
        }

        public async Task<UserInfo> FindUserAsync(int id)
        {
            var user = await context.Users.FindAsync(id);
            if (user != null)
            {
                context.Entry(user).State = EntityState.Detached;
            }
            return user;
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

       
    }
}