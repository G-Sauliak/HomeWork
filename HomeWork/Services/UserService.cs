using System.Linq;
using System.Threading.Tasks;
using HomeWork.Models;
using HomeWork.Repositories;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Data.Entity;
using X.PagedList;


namespace HomeWork.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        //Add
        #region Add
        public async Task<bool> AddUserAsync(RegisterViewModel model)
        {
            string nameCountry;
            string nameCity;

            if (!IsValidCountry(model.City, model.Country, out nameCity, out nameCountry)) return false;

            var newUser = new UserInfo()
            {
                FirstName = model.FirstName,
                MiddleName = model.MiddleName,
                LastName = model.LastName,
                Email = model.Email,
                Password = model.Password,
                Age = model.Age,
                Country = nameCountry,
                City = nameCity,
                Details = model.Detalis,
                PhoneNumber = model.PhoneNumber
            };

            await userRepository.AddAsync(newUser);

            return true;
        }
        #endregion

        //Delete
        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await userRepository.FindUserAsync(id);
            if (user == null) return false;

            await userRepository.DeleteAsync(id);
            return true;
        }
        //Get Cities
        public async Task<SelectList> GetCitiesAsync(int idCountry)
        {
            var listCities = await userRepository.GetCities().
                Where(i => i.Country_ID.ID == idCountry).
                ToListAsync();

            var selectList = new SelectList(listCities, "ID", "Name");

            return selectList;

        }
        //Get Countries
        public async Task<SelectList> GetCountriesAsync()
        {
            var listCountries = await userRepository.GetCountries().ToListAsync();

            var selectList = new SelectList(listCountries, "ID", "NameCountry");

            return selectList;
        }

        public async Task<UserInfo> GetUserAsync(int id)
        {
            var user = await userRepository.FindUserAsync(id);

            return user;
        }

        public async Task<IEnumerable<UserInfo>> GetUsersForPageList(int page, int pageSize)
        {
            return await userRepository.Get.OrderBy(p => p.Id).ToPagedListAsync(page, pageSize);
        }
        //UPDATE
        #region Update
        public async Task<bool> UpdateUserAsync(EditViewModel model)
        {

            string nameCountry;
            string nameCity;

            if (!IsValidCountry(model.City, model.Country, out nameCity, out nameCountry)) return false;

            var user = await userRepository.FindUserAsync(model.Id);

            if (user == null) return false;

            UserInfo updateUser = new UserInfo()
            {
                Id = model.Id,
                FirstName = model.FirstName,
                MiddleName = model.MiddleName,
                LastName = model.LastName,
                Age = model.Age,
                PhoneNumber = model.PhoneNumber,
                Email = user.Email,
                Password = user.Password,
                City = nameCity,
                Country = nameCountry,
                Details = model.Detalis,
            };

            await userRepository.UpdateAsync(updateUser);

            return true;
        }
        #endregion

        public async Task<int> CountUsers()
        {
            return await userRepository.Get.CountAsync();
        }

        private bool IsValidCountry(int idCity, int idCountry,out string nameCity, out string nameCountry)
        {
            nameCity = string.Empty;
            nameCountry = string.Empty;

            var country = userRepository.GetCountries().Where(c => c.ID == idCountry);

            if (country == null) return false;

            bool result = userRepository.GetCities().Where(n => n.ID == idCity).Any(id => id.Country_ID.ID == idCountry);

            if (result)
            {
                   nameCountry = userRepository.GetCountries().
                   Where(c => c.ID == idCountry).
                   Select(c => c.NameCountry).
                   Single();

                    nameCity = userRepository.
                    GetCities().
                    Where(c => c.ID == idCity).
                    Select(c => c.Name).
                    Single();

                return true;
            }
            return false; 
        }

    }
}