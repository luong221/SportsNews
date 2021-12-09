using BTL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BTL.security
{
    public class AdminAuthorize : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            dynamic user = filterContext.HttpContext.Session["USER"];
            if (user != null)
            {
                if(user.roleId == 1)
                {
                    return;
                }
                else
                {
                    //var url = filterContext.RequestContext.HttpContext.Request.RawUrl;
                    filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { controller = "Home", action = "Index" }));
                    
                }
               
            }
            else
            {
                //var url = filterContext.RequestContext.HttpContext.Request.RawUrl;
                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new {controller = "Login",action="Index"}));
            }
        }
    }
}