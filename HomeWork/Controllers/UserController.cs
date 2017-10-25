using System.Web.Mvc;
using HomeWork.Models;
using System.Threading.Tasks;
using HomeWork.Services;

namespace HomeWork.Controllers
{
    public class UserController : Controller
    {
        //unity inject
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        #region listUsers
        public async Task<ActionResult> Index()
        {
            var users = await userService.GetUsersAsync();

            var indexModel = new IndexViewModel()
            {
                userList = users
            };
            return View(indexModel);
        }

        //redirect to EditAction
        [HttpPost]
        public ActionResult Index(UserInfo model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToLocal(Url.Action("EditUser", "User", new { id = model.FirstName }));
            }
            return RedirectToLocal(Url.Action("Index", "User"));
        }
        #endregion

        #region Edit User
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
            return View(model);
        }

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

        public async Task<JsonResult> GetCities(int id)
        {
            var selectListCities = await userService.GetCitiesAsync(id, "ID", "Name");

            return Json(selectListCities, JsonRequestBehavior.AllowGet);
        }

       
        #region Delete User
        public async Task<JsonResult> DeleteUser(int id)
        {
            await userService.DeleteUserAsync(id);

            var selectListUsers = await userService.GetUsersAsync();

            return Json(selectListUsers, JsonRequestBehavior.AllowGet);
        }
        #endregion

        public async Task<ActionResult> DetailsUser(int id)
        {
            var userInfo = await userService.GetUserAsync(id);

            return PartialView(userInfo);
        }
    }
}