using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BTL.Models;
namespace BTL.Controllers
{
    public class HomeController : Controller
    {
        [Route("")]
        [Route("home")]
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult listNews()
        {
            return PartialView();
        }
        [HttpGet]
        public PartialViewResult getListData(int page=0)
        {           
            return PartialView();
        }
        public PartialViewResult subContent()
        {
            return PartialView();
        }
        public PartialViewResult generalNews()
        {
            return PartialView();
        }
        public PartialViewResult footballNews()
        {
            return PartialView();
        }
        public PartialViewResult videos()
        {
            return PartialView();
        }

    }
}