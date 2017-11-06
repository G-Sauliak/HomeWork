using HomeWork.Models;
using HomeWork.Services;
using System.Threading.Tasks;

namespace HomeWork.ViewModelBulder
{
    public class EditViewBuilder : IViewBuilder<int, EditViewModel>
    {
        private readonly IUserService userService;

        public EditViewBuilder(IUserService userService)
        {
            this.userService = userService;
        }

        async Task<EditViewModel> IViewBuilder<int, EditViewModel>.Build(int id)
        {

            var listCountries = await userService.GetCountriesAsync(string.Empty);

            var user = await userService.GetUserAsync(id);
            if (user == null) return null;

            EditViewModel model = new EditViewModel()
            {
                listCountries = listCountries,
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password,
                Age = user.Age,
                Detalis = user.Details,
                PhoneNumber = user.PhoneNumber
            };

            return model;
        }
    };

}
