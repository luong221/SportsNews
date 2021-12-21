using BTL.Models;
using BTL.security;
using PagedList;
using System.Linq;
using System.Web.Mvc;

namespace BTL.Controllers
{
    [AdminAuthorize]
    public class ReaderController : Controller
    {
        // GET: Reader
        private NewsData db = new NewsData();
        public ActionResult Index(int? page,int? pageSize)
        {
            var info = db.infoes.Where(t => t.role.rolename.Equals("READER") && t.status.Equals("ACTIVE")).OrderBy(t => t.id).ToPagedList(page ?? 1, pageSize ?? 5);
            return View(info);
        }

        [HttpGet]
        public ActionResult details(long id)
        {
            var info = db.infoes.Where(t => t.role.rolename.Equals("READER") && t.id == id).FirstOrDefault();
            return View(info);
        }
    }
}