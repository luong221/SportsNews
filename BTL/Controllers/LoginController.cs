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
        public async Task<ActionResult> authentication(string username,string password)
        {
            dynamic account = null;
            account = await db.users.SqlQuery("SELECT * FROM USERS WHERE email = @1 AND password = @2 COLLATE Latin1_General_CS_AS_KS_WS", new SqlParameter("@1", username), new SqlParameter("@2", password)).FirstOrDefaultAsync();
            if (account == null)
            {
                account = await db.journalists.SqlQuery("SELECT * FROM JOURNALIST WHERE email = @1 AND password = @2 COLLATE Latin1_General_CS_AS_KS_WS", new SqlParameter("@1", username), new SqlParameter("@2", password)).FirstOrDefaultAsync();
                if (account == null)
                {
                    account = await db.administratives.SqlQuery("SELECT * FROM ADMINISTRATIVE WHERE email = @1 AND password = @2 COLLATE Latin1_General_CS_AS_KS_WS", new SqlParameter("@1", username), new SqlParameter("@2", password)).FirstOrDefaultAsync();
                }
            }
            if (account != null)
            {
                Session["USER"] = account;
                if (account is administrative)
                {
                    return RedirectToAction("Index", "Admin");
                }
                
                return RedirectToAction("Index","Home");
            }
            ViewBag.msg = "Tên tài khoản hoặc mật khẩu không chính xác";
            return View("Index");
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