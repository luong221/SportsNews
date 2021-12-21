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
            return View(db.articles.Select(t=>t).OrderByDescending(t=>t.id).Take(4).ToList());
        }
        public PartialViewResult listNews()
        {
            var articles = db.articles.Select(t => t).OrderByDescending(t => t.id).Take(10).ToList();
            return PartialView(articles);
        }
        [HttpGet]
        public PartialViewResult getListData(int page=1)
        {
            int start = page * 10;
            var articles = db.articles.OrderByDescending(t=>t.id).Skip(start).Take(10).ToList();
            return PartialView(articles);
        }
        public PartialViewResult subContent()
        {
            return PartialView();
        }
        public PartialViewResult generalNews()
        {
            var artiles = db.articles.OrderByDescending(t => t.totalView).Select(t=>t).Take(5);
            return PartialView(artiles.ToList());
        }
        public PartialViewResult footballNews()
        {
            var artiles = db.articles.Where(t=>t.category.name.Equals("Bóng Đá")).OrderByDescending(t => t.id).Select(t => t).Take(9);
            return PartialView(artiles.ToList());
        }
        public PartialViewResult videos()
        {
            return PartialView();
        }

    }
}