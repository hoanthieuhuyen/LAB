using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace THA.WEB
{
    public class BaseController : Controller
    {
        // GET: Base
        protected override void  OnActionExecuting(ActionExecutingContext filterContext)
        {
            //var session = (UserLogin)Session[Constants.USER_SESSION];
            //if (session==null)
            //{
            //    filterContext.Result = new RedirectToRouteResult(new
            //        RouteValueDictionary(new { Controller = "Login", Action = "Login" }));
            //}
            //base.OnActionExecuting(filterContext);
        }
        protected void SetAlert(string message, string type)
        {
            TempData["AlertMessage"] = message;
            if (type == "success")
            {
                TempData["AlertType"] = "alert-success";
            }
            else if (type == "warning")
            {
                TempData["AlertType"] = "alert-warning";
            }
            else if (type == "error")
            {
                TempData["AlertType"] = "alert-danger";
            }
        }
    }
}