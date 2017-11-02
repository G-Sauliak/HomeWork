using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HomeWork.Models;
using HomeWork.Services;
using Microsoft.Practices.Unity;
using HomeWork.Repositories;

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