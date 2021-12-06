using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BTL.Models;

namespace BTL.Controllers
{
    public class journalistsController : Controller
    {
        private NewsData db = new NewsData();

        // GET: journalists
        public ActionResult Index()
        {
            var journalists = db.journalists.Include(j => j.role);
            return View(journalists.ToList());
        }

        // GET: journalists/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            journalist journalist = db.journalists.Find(id);
            if (journalist == null)
            {
                return HttpNotFound();
            }
            return View(journalist);
        }

        // GET: journalists/Create
        public ActionResult Create()
        {
            ViewBag.roleId = new SelectList(db.roles, "id", "rolename");
            return View();
        }

        // POST: journalists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,workExperience,roleId,email,password,name,lastname,gender,birthday,address,img,status,salary,createAt")] journalist journalist)
        {
            if (ModelState.IsValid)
            {
                db.journalists.Add(journalist);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.roleId = new SelectList(db.roles, "id", "rolename", journalist.roleId);
            return View(journalist);
        }

        // GET: journalists/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            journalist journalist = db.journalists.Find(id);
            if (journalist == null)
            {
                return HttpNotFound();
            }
            ViewBag.roleId = new SelectList(db.roles, "id", "rolename", journalist.roleId);
            return View(journalist);
        }

        // POST: journalists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,workExperience,roleId,email,password,name,lastname,gender,birthday,address,img,status,salary,createAt")] journalist journalist)
        {
            if (ModelState.IsValid)
            {
                db.Entry(journalist).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.roleId = new SelectList(db.roles, "id", "rolename", journalist.roleId);
            return View(journalist);
        }

        // GET: journalists/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            journalist journalist = db.journalists.Find(id);
            if (journalist == null)
            {
                return HttpNotFound();
            }
            return View(journalist);
        }

        // POST: journalists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            journalist journalist = db.journalists.Find(id);
            db.journalists.Remove(journalist);
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
