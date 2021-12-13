using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BTL.Models;
using BTL.security;

namespace BTL.Controllers
{
    [Admin_JournalistAuthorize]
    public class articlesController : Controller
    {
        private NewsData db = new NewsData();

        // GET: articles
        public ActionResult Index()
        {
            ViewBag.title = "Bài Viết";
            var articles = db.articles.Include(a => a.category).Include(a => a.journalist);
            return View(articles.ToList());
        }

        // GET: articles/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            article article = db.articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // GET: articles/Create
        public ActionResult Create()
        {
            ViewBag.categoryId = new SelectList(db.categories, "id", "name");
            return View();
        }

        // POST: articles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,journalistId,categoryId,title,totalView,thumbnail,description,status,createAt,updateAt")] article article,FormCollection f)
        {
            using (var dbcontext = db.Database.BeginTransaction())
            {
                try
                {
                    var __file = Request.Files["img-file"];
                    if (__file != null && __file.ContentLength > 0)
                    {

                        string __filename = System.IO.Path.GetFileName(__file.FileName);
                        string __path = Server.MapPath("~/images/") + __filename;
                        article.thumbnail = __filename;
                        dynamic journalist = Session["USER"];
                        article.journalistId = journalist is administrative ? "ADMIN" : journalist.id;
                        article.createAt = DateTime.Now;
                        string[] keys = f["keywordSubmit"].Split(',');
                        db.articles.Add(article);
                        db.SaveChanges();
                        if (keys.Length >= 1)
                        {
                            foreach (string i in keys)
                            {
                                long index = long.Parse(i);
                                article.keywords.Add(db.keywords.Find(index));
                                db.SaveChanges();
                            }
                        }
                        dbcontext.Commit();
                        __file.SaveAs(__path);
                        return RedirectToAction("Index");
                    }
                    ViewBag.categoryId = new SelectList(db.categories, "id", "name", article.categoryId);
                    return View(article);
                }
                catch (Exception e)
                {
                    dbcontext.Rollback();
                    ViewBag.message = "File upload failed!!";
                    ViewBag.exception = e.Message;
                    ViewBag.categoryId = new SelectList(db.categories, "id", "name", article.categoryId);
                    return View();
                }
            }

        }

        // GET: articles/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            article article = db.articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            ViewBag.categoryId = new SelectList(db.categories, "id", "name", article.categoryId);
            ViewBag.journalistId = new SelectList(db.journalists, "id", "name", article.journalistId);
            return View(article);
        }

        // POST: articles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,journalistId,categoryId,title,totalView,thumbnail,description,status,createAt,updateAt")] article article,FormCollection f)
        {
                      
            try
            {
                article currArticle = db.articles.AsNoTracking().Where(t => t.id == article.id).First();
                article.keywords = currArticle.keywords;
                var __file = Request.Files["img-file"];
                if (__file != null && __file.ContentLength > 0)
                {
                    ModelState["thumbnail"].Errors.Clear();
                    string __filename = System.IO.Path.GetFileName(__file.FileName);
                    string __path = Server.MapPath("~/images/") + __filename;
                    article.thumbnail = __filename;
                    __file.SaveAs(__path);
                }
                else
                {
                    article.thumbnail = currArticle.thumbnail;
                    ModelState["thumbnail"].Errors.Clear();
                }

                var keys = f["keywords"].Split(',').Select(t => long.Parse(t)).ToList();
                if (keys.Count!=0)
                {
                    foreach(var i in currArticle.keywords)
                    {
                        article.keywords.Remove(i);
                        if (article.keywords.Count == 0) break;
                    }
                    foreach (var i in keys)
                    {                       
                        article.keywords.Add(db.keywords.Find(i));
                    }

                }
                article.updateAt = DateTime.Now;
                if (ModelState.IsValid)
                {
                    db.Entry(article).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return RedirectToAction("edit", "articles");
            }
            catch (Exception e)
            {
                ViewBag.message = "File upload failed!!";
                ViewBag.exception = e.Message;
                return RedirectToAction("edit", "articles");
            }
            
        }

        // GET: articles/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            article article = db.articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // POST: articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            try
            {
                article article = db.articles.Find(id);
                db.articles.Remove(article);
                db.SaveChanges();
                Response.StatusCode = 200;
                return Json(new {msg="Xóa thành công!!"}, JsonRequestBehavior.AllowGet);
            }
            catch(Exception e)
            {
                Response.StatusCode = 500;
                return Json(new { msg = "Xóa Thất bại!!" }, JsonRequestBehavior.AllowGet);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
