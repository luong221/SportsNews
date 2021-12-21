using BTL.Models;
using BTL.security;
using PagedList;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTL.Controllers
{
    public class JournalistController : Controller
    {
        // GET: Journalist
        
        private NewsData db = new NewsData();
        [AdminAuthorize]
        public ActionResult Index(int? page,int? pageSize)
        {
            var info = db.infoes.Where(t => t.role.rolename.Equals("JOURNALIST") && t.status.Equals("ACTIVE")).OrderBy(t => t.id).ToPagedList(page ?? 1,pageSize ?? 5);
            return View(info);
        }
        [AdminAuthorize]
        [HttpGet]
        public ActionResult details(long id)
        {
            var info = db.infoes.Include(t=>t.journalists).Where(t => t.role.rolename.Equals("JOURNALIST") && t.id==id).FirstOrDefault();
            return View(info);
        }
        [HttpGet]
        [Admin_JournalistAuthorize]
        public ActionResult article()
        {
            var id = ((info)Session["USER"]).id;
            return View(db.articles.Include(t=>t.info).Where(t=>t.infoId == id).ToList());
        }
    }
}