using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrivilegeMS.WebAPP.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            bool isSucess = false;
            if (Session["userID"]!=null)
            {

                int userID = Convert.ToInt32(this.Session["userID"]);


                isSucess = true;
            }
           

            if (!isSucess)
            {
                //filterContext.HttpContext.Response.Redirect("/Login/Index");

                filterContext.Result = Redirect("/Login/Index");
            }
        }
    }
}