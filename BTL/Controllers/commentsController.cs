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
        public ActionResult Create([Bind(Include = "id,articleId,infoId,description,createAt,updateAt")] comment comment)
        {
            if (Session["USER"] != null)
            {
                using(var dbContext = db.Database.BeginTransaction())
                {
                    try
                    {
                        if (ModelState.IsValid)
                        {
                            comment.infoId = ((info)Session["USER"]).id;
                            comment.createAt = DateTime.Now;
                            db.comments.Add(comment);
                            db.SaveChanges();
                            comment comment1 = db.comments.Where(t => t.id == comment.id).Include(t => t.info).FirstOrDefault();
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
        [Route("comment/modified")]
        public ActionResult Edit(long id,string content)
        {
            comment comment = db.comments.Find(id);
            try
            {
                if(((info)Session["USER"]).id == comment.infoId || ((info)Session["USER"]).role.rolename.Equals("ADMIN"))
                {
                    comment.description = content;
                    db.Entry(comment).State = EntityState.Modified;
                    db.SaveChanges();
                    Response.StatusCode = 200;
                    return Json("OK", JsonRequestBehavior.AllowGet);
                }
                Response.StatusCode = 500;
                return Json("BAD REQUEST", JsonRequestBehavior.AllowGet);
            }
            catch(Exception e)
            {
                Response.StatusCode = 500;
                return Json("BAD REQUEST", JsonRequestBehavior.AllowGet);
            }         
        }

        // POST: comments/Delete/5
        [HttpPost]
        [Route("comment/delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            try
            {
                comment comment = db.comments.Find(id);
                if (((info)Session["USER"]).id == comment.infoId || ((info)Session["USER"]).role.rolename.Equals("ADMIN"))
                {
                    
                    db.comments.Remove(comment);
                    db.SaveChanges();
                    Response.StatusCode = 200;
                    return Json("OK", JsonRequestBehavior.AllowGet);
                }
                Response.StatusCode = 500;
                return Json("BAD REQUEST", JsonRequestBehavior.AllowGet);
            }
            catch(Exception e)
            {
                Response.StatusCode = 500;
                return Json("BAD REQUEST", JsonRequestBehavior.AllowGet);
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
      