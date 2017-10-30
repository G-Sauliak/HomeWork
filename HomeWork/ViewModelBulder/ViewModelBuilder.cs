using HomeWork.Models;
using HomeWork.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeWork.ViewModelBulder
{
    //create index model
    public class IndexViewBuild : IViewBuilder<int, int, int, IndexViewModel>
    {
        private readonly IUserService userService;

        public IndexViewBuild(IUserService service)
        {
            this.userService = service;
        }

        public async Task<IndexViewModel> Build(int indexPage, int counter, int countShowUser)
        {
            List<UserInfo> newUserlist = new List<UserInfo>();

            var userList = await userService.GetUsersAsync() as IList<UserInfo>;

            int countShow = 0;
            for (int i = indexPage * countShowUser; i < userList.Count; i++)
            {
                if (countShow >= countShowUser)
                {
                    break;
                }
                countShow++;
                newUserlist.Add(userList[i]);
            }


            var nextListIndexModel = new IndexViewModel()
            {
                Counter = counter,
                CurrentPage = indexPage,
                MaxShowUser = countShowUser,
                listusers = newUserlist,
                PreviousPage = indexPage - 1,
                NextPage = indexPage + 1,
                TotalCountUser = userList.Count
            };

            return nextListIndexModel;
        }
    }
    //create EditViewModel
    public class EditViewBuilder : IViewBuilder<int, EditViewModel>
    {
        private readonly IUserService userService;

        public EditViewBuilder(IUserService service)
        {
            this.userService = service;
        }

        async Task<EditViewModel> IViewBuilder<int, EditViewModel>.Build(int input)
        {

            var listCountries = await userService.GetCountriesAsync();

            var user = await userService.GetUserAsync(input);
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
