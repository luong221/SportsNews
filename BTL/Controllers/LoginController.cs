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
            return View();
        }

        [HttpPost]
        public ActionResult authentication(string username,string password)
        {
            dynamic account = db.users.AsQueryable().Where(t => t.email == username && t.password == password && t.status == "ACTIVE").FirstOrDefault();
            if (account == null)
            {
                account = db.journalists.AsQueryable().Where(t => t.email == username && t.password == password && t.status == "WORKED").FirstOrDefault();
                if (account == null)
                {
                    account = db.administratives.AsQueryable().Where(t => t.email == username && t.password == password).FirstOrDefault();
                }
            }
            
            if(account != null)
            {
                if (account.roleId == 1)
                {
                    if (Session["USER"] == null)
                    {
                        Session["USER"] = account;
                    }
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