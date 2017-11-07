using System.Web.Mvc;
using HomeWork.Models;
using System.Threading.Tasks;

namespace HomeWork.Controllers
{
    public class UserController : BaseController
    {
        // GET: /User/index
        #region listUsers
        [Route("ListUsers/{page}")]
        public async Task<ActionResult> ListUsers(int? page, string sort, string currentFilter, string search)
        {
            const int pageSize = 4;

            ViewBag.CurrentSort = sort;
            ViewBag.NameSortParam = string.IsNullOrEmpty(sort) ? "name_desc" : "";
            ViewBag.CountUsers = await userService.CountUsersAsync();

            if (search != null)
            {
                page = 1;
            }
            else
            {
                search = currentFilter;
            }

            int pageNum = (page ?? 1);

            var users = await userService.GetUsersForPageList(pageNum, pageSize, sort, search);

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
        
            var listCountries = await userService.GetCountriesAsync(user.Country);
            
            int IdCountry = default(int);

            foreach (var item in listCountries)
            {
                if (item.Text.Equals(user.Country))
                {
                    IdCountry = int.Parse(item.Value);
                }
            }
            var listCities = await userService.GetCitiesAsync(IdCountry,user.City);

            EditViewModel model = new EditViewModel()
            {
                listCountries = listCountries,
                listCities = listCities,
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
                model.listCountries = await userService.GetCountriesAsync(string.Empty);
                model.listCities = await userService.GetCitiesAsync(1,string.Empty);
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
           var listCountries = await userService.GetCountriesAsync(string.Empty);

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
                    listCountry = await userService.GetCountriesAsync(string.Empty),
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
            var selectListCities = await userService.GetCitiesAsync(id.Value,string.Empty);

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