using BTL.security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace BTL.Controllers
{
    [AdminAuthorize]
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

    }

}