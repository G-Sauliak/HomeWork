using System.Web.Mvc;
using HomeWork.Models;
using System.Threading.Tasks;
using HomeWork.Services;

namespace HomeWork.Controllers
{
    public class UserController : Controller
    {
        private const int CountShowUser = 4;

        //unity inject
        private readonly IUserService userService;
        private readonly IViewFactory ModelViewBuilder;

        public UserController(IUserService userService, IViewFactory viewFactory)
        {
            this.userService = userService;
            this.ModelViewBuilder = viewFactory;
        }
        //
        // GET: /User/index
        #region listUsers
        public async Task<ActionResult> Index()
        {
            //start viewModel
            var _indexModel = await ModelViewBuilder.CreateView<int, int, int, IndexViewModel>(0, 1, CountShowUser);

            return View(_indexModel);
        }
        #endregion
        //GET: /User/EditUser
        #region Edit User
        [HttpGet]
        public async Task<ActionResult> EditUser(int? id)
        {
            if (!id.HasValue)
            {
                return HttpNotFound();
            }

            var user = await userService.GetUserAsync(id.Value);

            if (user == null)
            {
                return HttpNotFound();
            }
            //Create EditModel
            var model = await ModelViewBuilder.CreateView<int, EditViewModel>(id.Value);

            model.RedirectUrl = Url.Action("Index", "User");

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

        //GET:
        public async Task<JsonResult> GetCities(int? id)
        {
            if (!id.HasValue)
            {
                return Json(null);
            }
            //get lits cities
            var selectListCities = await userService.GetCitiesAsync(id.Value, "ID", "Name");

            return Json(selectListCities, JsonRequestBehavior.AllowGet);
        }

        //GET /User/Index
        [HttpGet]
        #region Delete User
        public async Task<ActionResult> DeleteUser(int? id, int? indexPage, int? counter)
        {
            if (!id.HasValue && !indexPage.HasValue && !counter.HasValue && counter.Value > 0 && indexPage.Value > 0)
            {
                return HttpNotFound();
            }
            //isfind
            var user = await userService.GetUserAsync(id.Value);

            if (user == null)
            {
                return HttpNotFound();
            }
            //delete
            await userService.DeleteUserAsync(id.Value);

            //get new list 
            var _indexModel = await ModelViewBuilder.
                CreateView<int, int, int, IndexViewModel>(indexPage.Value, counter.Value, CountShowUser);

            return PartialView("ListUsers", _indexModel);
        }

        #endregion
        //GET:
        public async Task<ActionResult> DetailsUser(int id)
        {
            var userInfo = await userService.GetUserAsync(id);

            return PartialView(userInfo);
        }
        //next page
        public async Task<ActionResult> RefreshList(int? indexPage, int? counter)
        {
            if (!indexPage.HasValue && !counter.HasValue && counter.Value > 0 && indexPage.Value > 0)
            {
                return HttpNotFound();
            }

            var indexModel_ = await ModelViewBuilder.
                CreateView<int, int, int, IndexViewModel>(indexPage.Value, counter.Value, CountShowUser);
        
            return PartialView("ListUsers", indexModel_);
        }

        private ActionResult RedirectToLocal(string url)
        {
            if (Url.IsLocalUrl(url))
            {
                return Redirect(url);
            }
            return RedirectToAction("Index", "User");
        }

    }

}