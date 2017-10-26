using System.Web.Mvc;
using HomeWork.Models;
using System.Threading.Tasks;
using HomeWork.Services;
using System.Collections.Generic;

namespace HomeWork.Controllers
{
    public class UserController : Controller
    {
        private const int MaxShowUser = 4;
        //unity inject
        private readonly IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }
        //
        // GET: /User/index
        #region listUsers
        public async Task<ActionResult> Index()
        {
            var _users = await userService.GetUsersAsync();
            
            var indexModel = new IndexViewModel()
            {
                MaxShowUser = MaxShowUser,
                listusers = _users as List<UserInfo>,
            };
            return View(indexModel);
        }
        #endregion
        //GET: /User/EditUser
        #region Edit User
        [HttpGet]
        public async Task<ActionResult> EditUser(int id)
        {
            var user = await userService.GetUserAsync(id);
            var listCountries = await userService.GetCountriesAsync();

            EditViewModel model = new EditViewModel()
            {
                listCountries = listCountries,
                RedirectUrl = Url.Action("Index", "User"),
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password,
                Age = user.Age,
                Detalis = user.Details,
                PhoneNumber = user.PhoneNumber

            };
            return PartialView(model);
        }
        //POST: /User/EditUser
        [HttpPost]
        public async Task<ActionResult> EditUser(EditViewModel model, string redirectUrl)
        {
            if (!ModelState.IsValid)
            {
              RedirectToLocal(Url.Action("EditUser", "User", new { id = model.Id }));
            }

             await userService.UpdateUserAsync(model);

            return RedirectToLocal(redirectUrl);
        }
        #endregion
        //GET: /User/AddUser
        #region Add User
        public async Task<ActionResult> AddUser()
        {
            var listCountries = await userService.GetCountriesAsync();

            var model = new RegisterViewModel()
            {
                JsonActionUrl = Url.Action("GetCities", "User"),
                RedirectUrl = Url.Action("Index", "User"),
                listCountry = listCountries
            };

            return View(model);
        }
        //POST: /User/AddUser
        [HttpPost]
        public async Task<ActionResult> AddUser(RegisterViewModel model, string redirectUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await userService.AddUserAsync(model);

            return RedirectToLocal(redirectUrl);
        }
        #endregion

        private ActionResult RedirectToLocal(string url)
        {
            if (Url.IsLocalUrl(url))
            {
                return Redirect(url);
            }
            return RedirectToAction("Index", "User");
        }

        //GET:
        public async Task<JsonResult> GetCities(int id)
        {
            var selectListCities = await userService.GetCitiesAsync(id, "ID", "Name");

            return Json(selectListCities, JsonRequestBehavior.AllowGet);
        }

        //GET /User/Index
        #region Delete User
        public async Task<ActionResult> DeleteUser(int id)
        {
            if (id > 0)
            {
                await userService.DeleteUserAsync(id);
            }
            var _users = await userService.GetUsersAsync();

            var indexModel = new IndexViewModel()
            {
                MaxShowUser = MaxShowUser,
                listusers = _users as List<UserInfo>,
            };

            return PartialView("Index",indexModel);
        
        }
        #endregion
        //GET:
        public async Task<ActionResult> DetailsUser(int id)
        {
            var userInfo = await userService.GetUserAsync(id);

            return PartialView(userInfo);
        }

    }
}