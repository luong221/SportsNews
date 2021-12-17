﻿using BTL.Models;
using BTL.security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTL.Controllers
{
    [AdminAuthorize]
    public class JournalistController : Controller
    {
        // GET: Journalist
        private NewsData db = new NewsData();
        public ActionResult Index()
        {
            var info = db.infoes.Where(t => t.role.rolename.Equals("JOURNALIST") && t.status.Equals("ACTIVE")).OrderBy(t => t.id).Take(10);
            return View(info.ToList());
        }
    }
}