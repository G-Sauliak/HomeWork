using System.Web.Mvc;
using HomeWork.Services;
using Microsoft.Practices.Unity;

namespace HomeWork.Controllers
{
    public class BaseController : Controller
    {
        //unity inject

        [Dependency]
        protected IViewFactory ModelViewBuilder { get; set; }

        [Dependency]
        protected IUserService userService { get; set; }

    }
}