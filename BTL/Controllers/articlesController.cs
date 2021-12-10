using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BTL.Models;
using BTL.security;

namespace BTL.Controllers
{
    [AdminAuthorize]
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
            ViewBag.journalistId = new SelectList(db.journalists, "id", "name");
            return View();
        }

        // POST: articles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,journalistId,categoryId,title,totalView,thumbnail,description,status,createAt,updateAt")] article article)
        {
            try
            {
                var __file = Request.Files["img-file"];
                if (__file != null && __file.ContentLength > 0)
                {
                    string __filename = System.IO.Path.GetFileName(__file.FileName);
                    string __path = Server.MapPath("~/images/") + __filename;
                    article.thumbnail = __filename;
                    article.createAt = DateTime.Now;
                    db.articles.Add(article);
                    db.SaveChanges();
                    __file.SaveAs(__path);
                    return RedirectToAction("Index");
                }
                ViewBag.categoryId = new SelectList(db.categories, "id", "name", article.categoryId);
                ViewBag.journalistId = new SelectList(db.journalists, "id", "name", article.journalistId);
                return View(article);
            }
            catch (Exception e)
            {
                ViewBag.message = "File upload failed!!";
                ViewBag.exception = e.Message;
                return View();
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
        public ActionResult Edit([Bind(Include = "id,journalistId,categoryId,title,totalView,thumbnail,description,status,createAt,updateAt")] article article)
        {
            if (ModelState.IsValid)
            {
                db.Entry(article).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.categoryId = new SelectList(db.categories, "id", "name", article.categoryId);
            ViewBag.journalistId = new SelectList(db.journalists, "id", "name", article.journalistId);
            return View(article);
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
            article article = db.articles.Find(id);
            db.articles.Remove(article);
            db.SaveChanges();
            return RedirectToAction("Index");
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
