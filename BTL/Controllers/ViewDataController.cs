using BTL.Models;
using BTL.security;
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
            article article = db.articles.Find(id);
            article.totalView += 1; 
            db.Entry(article).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            article a = db.articles.Find(id);
            return PartialView(a);
        }
        [Route("View/{name}")]
        [HttpGet]
        public PartialViewResult viewByCategory(long name)
        {
            var articles = db.articles.Where(t => t.categoryId.Equals(name)).OrderByDescending(t=>t.id).ToList();
            bool isEmpty = !articles.Any();
            if (isEmpty)
            {
                ViewBag.title = "Không có bài viết.";
                
            }
            else
            {
                ViewBag.title = articles.First().category.name;
            }
            return PartialView(articles);
        }
        [HttpGet]
        [Route("keyword/{keyword}")]
        public PartialViewResult viewByKeyword(string keyword)
        {
            var articles = db.keywords.Where(c => c.name.Equals(keyword)).OrderByDescending(t => t.id).SelectMany(c => c.articles);
            bool isEmpty = !articles.Any();
            if (isEmpty)
            {
                ViewBag.title = "Không có bài viết.";

            }
            else
            {
                ViewBag.title = keyword;
            }
            return PartialView(articles.ToList());
        }

        [HttpGet]
        [Route("search")]
        public PartialViewResult search(string keyword)
        {
            var articles = db.articles.Where(t => t.title.Contains(keyword));
            bool isEmpty = !articles.Any();
            if (isEmpty)
            {
                ViewBag.title = "Không có bài viết.";

            }
            else
            {
                ViewBag.title = "Kết quả cho \""+keyword+"\"";
            }
            return PartialView(articles.ToList());
        }

        [AdminAuthorize]
        [HttpPost]
        [Route("search")]
        public PartialViewResult searchByAdmin(string searchBy,string key)
        {
            if (searchBy.Equals("article"))
            {
                return PartialView("~/Views/ViewData/article.cshtml",db.articles.Where(t => t.title.Contains(key)).ToList());
            }
            else if (searchBy.Equals("category"))
            {
                return PartialView("~/Views/ViewData/category.cshtml", db.categories.Where(t => t.name.Contains(key)).ToList());
            }
            else if (searchBy.Equals("journalist"))
            {
                return PartialView("~/Views/ViewData/journalist.cshtml", db.infoes.Where(t => t.name.Contains(key) && t.role.rolename.Equals("JOURNALIST")).ToList());
            }
            else if (searchBy.Equals("reader"))
            {
                return PartialView("~/Views/ViewData/reader.cshtml", db.infoes.Where(t => t.name.Contains(key) && t.role.rolename.Equals("READER")).ToList());
            }
            else
            {
                return PartialView("~/Views/Shared/Error.cshtml");
            }
        }
    }
}