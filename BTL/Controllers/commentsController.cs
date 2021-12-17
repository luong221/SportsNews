using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using BTL.Models;

namespace BTL.Controllers
{
    public class commentsController : Controller
    {
        private NewsData db = new NewsData();

        // GET: comments
        public ActionResult Index(long id)
        {
            var comments = db.comments.Include(c => c.article).Include(c => c.info).Where(t=>t.articleId == id).OrderByDescending(t=>t.id);
            return View(comments.ToList());
        }


        // POST: comments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "id,articleId,userId,description,createAt,updateAt")] comment comment)
        {
            if (Session["USER"] != null)
            {
                ModelState["userId"].Errors.Clear();
                using(var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        if (ModelState.IsValid)
                        {
                            comment.infoId = ((dynamic)Session["USER"]).id;
                            comment.createAt = DateTime.Now;
                            db.comments.Add(comment);
                            db.SaveChanges();
                            comment comment1 = db.comments.Where(t => t.id == comment.id).Include(t => t.infoId).FirstOrDefault();
                            Response.StatusCode = 200;
                            var time = DateTime.Now - comment1.createAt;
                            var displayTime = "";
                            if (time.Days == 0)
                            {
                                if (time.Hours == 0)
                                {
                                    displayTime = (int)time.TotalMinutes + " phút trước";
                                }
                                else
                                {
                                    displayTime = (int)time.TotalHours + " giờ trước";
                                }
                            }
                            else
                            {
                                displayTime = (int)time.TotalDays + " ngày trước";
                            }
                            var img = comment1.info.img;
                            if (img == null || img == "")
                            {
                                if (comment1.info.gender == true)
                                {
                                    img = "male.jpeg";
                                }
                                else
                                {
                                    img = "female.jpeg";
                                }
                            }
                            dbContext.Commit();
                            StringBuilder resp = new StringBuilder("<div class='content-comment'><div class='user'><img src = '/images/");
                            resp.Append(img);
                            resp.Append("'/></div><div><p class='full-comment'><strong>");
                            resp.Append(comment1.info.lastname + comment1.info.name);
                            resp.Append(":</strong>" + comment1.description + "</p><p>" + displayTime + "</p></div></div>");
                            return Content(resp.ToString());
                        }
                    }
                    catch (Exception e)
                    {
                        dbContext.Rollback();
                        Response.StatusCode = 500;
                        return Json("FAIL", JsonRequestBehavior.AllowGet);
                    }
                }              
            }
            return RedirectToAction("Index", "Login");
        }


        // POST: comments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,articleId,userId,description,createAt,updateAt")] comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.articleId = new SelectList(db.articles, "id", "journalistId", comment.articleId);
            ViewBag.userId = new SelectList(db.infoes, "id", "email", comment.infoId);
            return View(comment);
        }

        // POST: comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            comment comment = db.comments.Find(id);
            db.comments.Remove(comment);
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
