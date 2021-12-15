using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
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
        [AdminAuthorize]
        [Route("censor")]
        public ActionResult listNotCensor()
        {
            var articles = db.articles.Include(a => a.category).Include(a => a.journalist).Where(t=>t.status.Equals("INITIAL"));
            return View(articles);
        }
        [AdminAuthorize]
        [HttpPost]
        [Route("censor/{id}")]
        public ActionResult changeStatus(long id)
        {
            try
            {
                var article = db.articles.Find(id);
                article.status = "PUBLISH";
                db.Entry(article).State = EntityState.Modified;
                db.SaveChanges();
                Response.StatusCode = 200;
                return Json(new { msg = "Đã xuất bản!!" }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception e)
            {
                Response.StatusCode = 500;
                return Json(new { msg = "Có lỗi xảy ra!!" }, JsonRequestBehavior.AllowGet);
            }
        }
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
                        currArticle.thumbnail = __filename;
                        __file.SaveAs(__path);
                    }

                    var keys = f["keywords"].Split(',').Select(t => long.Parse(t)).ToList();
                    if (keys.Count != 0)
                    {
                        var n = currArticle.keywords.ToList();
                        string delete = "delete keyword_article where articleId = '" + currArticle.id + "'";
                        StringBuilder insert = new StringBuilder("Insert into keyword_article values ");
                        SqlConnection conn = new SqlConnection(db.Database.Connection.ConnectionString);
                        conn.Open();
                        SqlCommand qdelete = new SqlCommand(delete, conn);                       
                        qdelete.ExecuteNonQuery();                        
                        foreach (var i in keys)
                        {
                            insert.Append("(" + currArticle.id + "," + db.keywords.Find(i).id + "),");
                        }
                        insert.Remove(insert.Length-1, 1);
                        SqlCommand qinsert = new SqlCommand(insert.ToString(), conn);
                        qinsert.ExecuteNonQuery();
                        conn.Close();
                    }
                    currArticle.updateAt = DateTime.Now;
                    currArticle.categoryId = article.categoryId;
                    currArticle.journalistId = article.journalistId;
                    currArticle.title = article.title;
                    currArticle.description = article.description;
                    foreach (var modelValue in ModelState.Values)
                    {
                        modelValue.Errors.Clear();
                    }
                    db.Entry(currArticle).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");

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
