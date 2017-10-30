using System.Linq;
using System.Threading.Tasks;
using HomeWork.Models;
using HomeWork.Repositories;
using System.Web.Mvc;
using System.Collections;
using System.Collections.Generic;

namespace HomeWork.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task AddUserAsync(RegisterViewModel model)
        {

            var country = await userRepository.GetCountriesAsync();
            var cities = await userRepository.GetCitiesAsync(model.Country);

            string nameCountry = country.ElementAt(model.Country - 1).NameCountry;
            string nameCity = cities.FirstOrDefault(v => v.ID == model.City).Name;

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

            await userRepository.AddUserAsync(newUser);
        }

        public async Task DeleteUserAsync(int id)
        {
            await userRepository.DeleteUserAsync(id);
        }

        public async Task<SelectList> GetCitiesAsync(int idCountry, string dataValueField, string dataTextField)
        {
            var listCities = await userRepository.GetCitiesAsync(idCountry);

            var selectList = new SelectList(listCities, dataValueField, dataTextField);

            return selectList;
        }

        public async Task<SelectList> GetCountriesAsync()
        {
            var listCountry = await userRepository.GetCountriesAsync();

            var listCountries = new SelectList(listCountry, "ID", "NameCountry");

            return listCountries;
        }

        public async Task<UserInfo> GetUserAsync(int id)
        {
            return await userRepository.GetUserAsync(id);
        }

        public async Task<IEnumerable<UserInfo>> GetUsersAsync()
        {
            return await userRepository.GetUsersAsync();
        }

        public async Task UpdateUserAsync(EditViewModel model)
        {
            var country = await userRepository.GetCountriesAsync();
            var cities = await userRepository.GetCitiesAsync(model.Country);

            string nameCountry = country.ElementAt(model.Country - 1).NameCountry;
            string nameCity = cities.FirstOrDefault(v => v.ID == model.City).Name;

            var user = await userRepository.GetUserAsync(model.Id);

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

            await userRepository.UpdateUserAsync(updateUser);
        }
    }
}
