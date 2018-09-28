using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mvcVeteriner_temiz.Utils
{
    public class BaseController :System.Web.Mvc.Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if(Session["Admin"]== null || Session["Admin"].ToString() != "1")
            {
                filterContext.Result = new RedirectResult("~/Uye/Index");
                return;
            }

            base.OnActionExecuting(filterContext);
        }
    }
}