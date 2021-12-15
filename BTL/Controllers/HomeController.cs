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
        private NewsData db = new NewsData();
        [Route("")]
        [Route("home")]
        public ActionResult Index()
        {
            return View(db.articles.Select(t=>t).OrderBy(t=>t.id).Take(4).ToList());
        }
        public PartialViewResult listNews()
        {
            var articles = db.articles.Select(t => t).OrderBy(t => t.id).Skip(0).Take(20).ToList();
            return PartialView(articles);
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