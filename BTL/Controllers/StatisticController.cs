using BTL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTL.Controllers
{
    public class StatisticController : Controller
    {
        private NewsData db = new NewsData();
        // GET: Statistic
        [HttpGet]
        [Route("countArticleInMonth/{month}/{year?}")]
        public ActionResult countArticleInMonth(int month,int? year)
        {
            if(year == null)
            {
                year = DateTime.Now.Year;
            }
            var subQuery = db.articles.Where(t => t.createAt.Month == month && t.createAt.Year == year).Select(t => new { t.id, t.createAt.Day });
            var query = subQuery.GroupBy(t => t.Day).ToList().Select(t=> new {key = t.Key,count = t.Count() });
            List<string> label = new List<string>();
            int day = countDayofMonth(month, (int)year);
            for (int i = 1; i <= day; i++) { label.Add(""+i); }
            List<Object> data = new List<object>();
            int c = query.Count();
            int j = 0;
            for (int i = 0; i < day; i++)
            {
                if (j < c)
                {
                    var item = query.ElementAt(j);
                    if (item.key == i + 1)
                    {
                        data.Add(new { label = i + 1, value = item.count });
                        j++;
                        continue;
                    }
                }              
                data.Add(new { label = i+1, value = 0 });
            }
            
            return Json(data,JsonRequestBehavior.AllowGet);
        }


        private int countDayofMonth(int month,int year)
        {
            DateTime date = new DateTime(year,month,1);
            return date.AddMonths(1).AddDays(-1).Day;
        }
    }
}