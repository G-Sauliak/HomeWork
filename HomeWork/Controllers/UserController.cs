using System.Web.Mvc;
using HomeWork.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using X.PagedList;


namespace HomeWork.Controllers
{
    public class UserController : BaseController
    {
        // GET: /User/index
        #region listUsers
        public async Task<ActionResult> ListUsers(int? page = 1)
        {
            IEnumerable<UserInfo> users = null;
            const int pageSize = 4;

            if (!page.HasValue) return HttpNotFound();

            ViewBag.CountUsers = await userService.CountUsers();
            
            users = await userService.GetUsersForPageList(page.Value, pageSize);

            return View(users);
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

            model.RedirectUrl = Url.Action("ListUsers", "User");

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

            var result = await userService.UpdateUserAsync(model);

            if (!result)
            {
                ModelState.AddModelError(string.Empty, "The user cannot be Update");
                return View(model);
            }

            return RedirectToLocal(redirectUrl);
        }
        #endregion
        //GET: /User/AddUser
        #region Add User
        public async Task<ActionResult> AddUser()
        {
           //var listCountries = await UserService.GetCountriesAsync();
           var listCountries = await userService.GetCountriesAsync();

            var model = new RegisterViewModel()
            {
                JsonActionUrl = Url.Action("GetCities", "User"),
                RedirectUrl = Url.Action("ListUsers", "User"),
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
                ModelState.AddModelError(string.Empty, "The user cannot be added");
                return View(model);
            }

            bool result = await userService.AddUserAsync(model);

            if (!result)
            {
                ModelState.AddModelError(string.Empty, "The user cannot be added");
                View(new RegisterViewModel()
                {
                    JsonActionUrl = Url.Action("GetCities", "User"),
                    listCountry = await userService.GetCountriesAsync(),
                    RedirectUrl = Url.Action("ListUsers", "User")
                });
            }
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
            var selectListCities = await userService.GetCitiesAsync(id.Value);

            return Json(selectListCities, JsonRequestBehavior.AllowGet);
        }

        //GET /User/Index
        [HttpGet]
        #region Delete User
        public async Task<ActionResult> DeleteUser(int? id)
        {
            if (!id.HasValue)
            {
                return HttpNotFound();
            }
           
            await userService.DeleteUserAsync(id.Value);
  
            return RedirectToLocal(Url.Action("ListUsers"));
        }

        #endregion
        //GET:
        public async Task<ActionResult> DetailsUser(int id)
        {
            var newInfo = await userService.GetUserAsync(id);

            return PartialView(newInfo);
        }
        //next page

        private ActionResult RedirectToLocal(string url)
        {
            if (Url.IsLocalUrl(url))
            {
                return Redirect(url);
            }
            return RedirectToAction("ListUsers", "User");
        }

    }

}