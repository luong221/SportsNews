using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Data.Linq.SqlClient;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BTL.Models;
using BTL.security;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace BTL.Controllers
{
    [Admin_JournalistAuthorize]
    public class keywordsController : Controller
    {
        private NewsData db = new NewsData();

        // GET: keywords
        public ActionResult Index(string term)
        {
            var list = db.keywords.Where(t => t.name.Contains(term)).OrderBy(t=>t.id).Take(10).Select(t=> new {t.id,t.name }).ToArray();          
            return Json(new { results = list }, JsonRequestBehavior.AllowGet);
        }

        // GET: keywords/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            keyword keyword = db.keywords.Find(id);
            if (keyword == null)
            {
                return HttpNotFound();
            }
            return View(keyword);
        }

        // GET: keywords/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: keywords/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "id,name")] keyword keyword)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.keywords.Add(keyword);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(keyword);
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string name)
        {
            int status = 500;
            string msg = "Thêm keyword "+ name +" thất bại!";
            keyword key = new keyword();
            if (db.keywords.Count(t => t.name == name) == 0)
            {
                key.name = name;
                db.keywords.Add(key);
                db.SaveChanges();
                status = 200;
                msg = "Thêm keyword " + name + " thành công!";
            }
            var resp = new {msg = msg};
            Response.StatusCode = status;
            return Json(resp,JsonRequestBehavior.AllowGet);
        }
        // GET: keywords/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            keyword keyword = db.keywords.Find(id);
            if (keyword == null)
            {
                return HttpNotFound();
            }
            return View(keyword);
        }

        // POST: keywords/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name")] keyword keyword)
        {
            if (ModelState.IsValid)
            {
                db.Entry(keyword).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(keyword);
        }

        // GET: keywords/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            keyword keyword = db.keywords.Find(id);
            if (keyword == null)
            {
                return HttpNotFound();
            }
            return View(keyword);
        }

        // POST: keywords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            keyword keyword = db.keywords.Find(id);
            db.keywords.Remove(keyword);
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
