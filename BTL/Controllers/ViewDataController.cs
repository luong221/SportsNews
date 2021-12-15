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
            article article = db.articles.Find(id);
            article.totalView += 1; 
            db.Entry(article).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            article a = db.articles.Find(id);
            return PartialView(a);
        }
        [Route("View/{name}")]
        [HttpGet]
        public PartialViewResult viewByCategory(string name)
        {
            var articles = db.articles.Where(t => t.categoryId.Equals(name)).ToList();
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
    }
}