using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
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
    public class infoesController : Controller
    {
        private NewsData db = new NewsData();


        // GET: profile
        [HttpGet]
        [Route("profile")]
        public ActionResult Details()
        {
            try
            {
                info session = (info)Session["USER"];
                if (session != null)
                {
                    info info = db.infoes.Where(t => t.id == session.id).Include(t => t.journalists).FirstOrDefault();
                    if (info == null)
                    {
                        return HttpNotFound();
                    }
                    return View(info);
                }
                return null;
            }
            catch (Exception e)
            {
                return null;
            }

        }

        // GET: infoes/Create
        [HttpGet]
        [Route("info/create")]
        [AdminAuthorize]
        public ActionResult Create()
        {
            ViewBag.roleId = new SelectList(db.roles, "id", "rolename",3);
            return View();
        }

        // POST: infoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("info/create",Name ="createByAdmin")]
        [ValidateAntiForgeryToken]
        [AdminAuthorize]
        public ActionResult Create([Bind(Include = "id,roleId,email,password,name,lastname,gender,birthday,address,img,status,createAt")] info info)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    info.createAt = DateTime.Now;
                    var __file = Request.Files["img-file"];
                    if (__file != null && __file.ContentLength > 0)
                    {
                        string __filename = System.IO.Path.GetFileName(__file.FileName);
                        string __path = Server.MapPath("~/images/") + __filename;
                        info.img = __filename;
                        __file.SaveAs(__path);
                    }
                    db.infoes.Add(info);
                    db.SaveChanges();
                    ViewBag.roleId = new SelectList(db.roles, "id", "rolename");
                    ViewBag.msg = "Thêm thành công";
                    ViewBag.status = "success";
                    return View();
                }
                ViewBag.msg = "Thêm thất bại";
                ViewBag.status = "danger";
                ViewBag.roleId = new SelectList(db.roles, "id", "rolename", info.roleId);
                return View(info);
            }
            catch(Exception e)
            {
                ViewBag.msg = "Thêm thất bại";
                ViewBag.status = "danger";
                ViewBag.roleId = new SelectList(db.roles, "id", "rolename", info.roleId);
                return View(info);
            }
        }

        // GET: info/Edit/5
        [AdminAuthorize]
        [HttpGet]
        [Route("info/edit/{id}")]
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            info info = db.infoes.Find(id);
            if (info == null)
            {
                return HttpNotFound();
            }
            ViewBag.roleId = new SelectList(db.roles, "id", "rolename", info.roleId);
            return View(info);
        }
        //Get profile/edit
        [HttpGet]
        [Route("profile/edit")]
        public ActionResult EditInfor()
        {
            info session = (info)Session["USER"];
            if (Session["USER"] != null)
            {
                info info = db.infoes.Where(t => t.id == session.id).Include(t => t.journalists).FirstOrDefault();
                if (info == null)
                {
                    return HttpNotFound();
                }
                return View(info);
            }
            return null;
        }
        // POST: profile/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("profile/edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,roleId,email,password,name,lastname,gender,birthday,address,img,status,createAt")] info info)
        {
            try
            {
                foreach (var modelstate in ModelState.Values)
                {
                    modelstate.Errors.Clear();
                }
                info current = db.infoes.AsNoTracking().Where(t => t.id == info.id).FirstOrDefault();
                info.roleId = current.roleId;
                info.password = current.password;
                info.createAt = current.createAt;
                info.status = current.status;
                info.email = current.email;
                var __file = Request.Files["img-file"];
                if (__file != null && __file.ContentLength > 0)
                {
                    string __filename = System.IO.Path.GetFileName(__file.FileName);
                    string __path = Server.MapPath("~/images/") + __filename;
                    info.img = __filename;
                    __file.SaveAs(__path);
                }
                else
                {
                    info.img = current.img;
                }
                db.Entry(info).State = EntityState.Modified;
                db.SaveChanges();
                return Redirect("/profile");
            }catch(Exception e)
            {
                return RedirectToAction("EditInfor");
            }
        }
        [AdminAuthorize]
        [HttpPost]
        [Route("info/edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditByAdmin([Bind(Include = "id,roleId,email,password,name,lastname,gender,birthday,address,img,status,createAt")] info info)
        {
            try
            {              
                info current = db.infoes.AsNoTracking().Where(t => t.id == info.id).FirstOrDefault();
                current.roleId = info.roleId;
                current.address = info.address;
                current.birthday = info.birthday;
                current.gender = info.gender;
                current.lastname = info.lastname;
                current.name = info.name;
                current.journalists = info.journalists;                              
                var __file = Request.Files["img-file"];
                if (__file != null && __file.ContentLength > 0)
                {
                    string __filename = System.IO.Path.GetFileName(__file.FileName);
                    string __path = Server.MapPath("~/images/") + __filename;
                    current.img = __filename;
                    __file.SaveAs(__path);
                }
                foreach (var modelstate in ModelState.Values)
                {
                    modelstate.Errors.Clear();
                }
                if (current.journalists != null)
                {
                    string update = "UPDATE journalist set workExperience = "+long.Parse(Request.Form["workExperience"])+", salary = "+int.Parse(Request.Form["salary"]) + " where id= "+current.journalists.First().id;
                    SqlConnection conn = new SqlConnection(db.Database.Connection.ConnectionString);
                    conn.Open();
                    SqlCommand exp_sa = new SqlCommand(update, conn);
                    exp_sa.ExecuteNonQuery();
                    conn.Close();
                }
                db.Entry(current).State = EntityState.Modified;
                db.SaveChanges();
                return Redirect("/journalist/details/"+info.id);
            }
            catch (Exception e)
            {
                return RedirectToAction("info/edit/" + info.id);
            }
        }
        // POST: infoes/Delete/5
        [AdminAuthorize]
        [HttpPost]
        [Route("info/delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            try
            {
                info info = db.infoes.Find(id);
                db.infoes.Remove(info);
                db.SaveChanges();
                Response.StatusCode = 200;
                return Json(new {msg= "Xoá thành công!!" },JsonRequestBehavior.AllowGet);
            }
            catch(Exception e)
            {
                Response.StatusCode = 500;
                return Json(new { msg = "Xoá thành công!!" }, JsonRequestBehavior.AllowGet);
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
