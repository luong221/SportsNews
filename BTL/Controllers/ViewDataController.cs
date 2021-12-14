using BTL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTL.Controllers
{
    public class ViewDataController : Controller
    {
        private NewsData db = new NewsData();
        // GET: ViewData
        [Route("detail/{id}")]
        [HttpGet]
        public PartialViewResult detail(long id)
        {
            article a = db.articles.Find(id);
            return PartialView(a);
        }
    }
}