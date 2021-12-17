using BTL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BTL.Controllers
{
    
    public class LoginController : Controller
    {
        private NewsData db = new NewsData();
        // GET: Login
        
        public ActionResult Index()
        {
            var account = (info)Session["USER"];
            if (account!=null)
            {
                if (account.roleId == 1)
                {
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    if (account.roleId == 2)
                    {
                        return RedirectToAction("Index", "Journalist");
                    }
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult authentication(string username,string password)
        {
            var account = db.infoes.Where(t => t.email == username && t.password == password).FirstOrDefault();            
            if(account != null)
            {
                if (Session["USER"] == null)
                {
                    Session["USER"] = account;
                }
                if (account.roleId == 1)
                {                   
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    if(account.roleId == 2)
                    {
                        return RedirectToAction("Index", "Journalist");                        
                    }
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ViewBag.msg = "Tên tài khoản hoặc mật khẩu không chính xác";
                return View("Index");
            }      
        }

        [HttpGet]
        [Route("logout")]
        public ActionResult Logout()
        {
            Session.RemoveAll();
            return RedirectToAction("Index", "Home");
        }
    }
}