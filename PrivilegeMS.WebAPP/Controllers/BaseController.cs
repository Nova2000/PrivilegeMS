using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrivilegeMS.WebAPP.Controllers
{
    public class BaseController : Controller
    {
        IBLL.IUserInfoService userInfoService = new BLL.UserInfoService();
        IBLL.IActionInfoService actionInfoService = new BLL.ActionInfoService();
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            bool isSucess = false;
            if (Session["userID"] != null)
            {
                isSucess = true;

                int userID = Convert.ToInt32(this.Session["userID"]);
                //获取请求方式和请求地址
                string url = Request.Url.AbsolutePath.ToUpper();
                string httpMehotd = Request.HttpMethod.ToUpper();

                //1.【用户--权限】
                var actionInfo = actionInfoService.LoadEntities(a => a.Url.ToUpper() == url && a.HttpMethod.ToUpper() == httpMehotd).FirstOrDefault();
                        //获取登录用户
                var loginUser = userInfoService.LoadEntities(u => u.ID == userID).FirstOrDefault();
                var isExt = (from a in loginUser.RUserInfoActionInfo
                             where a.ActionInfoID == actionInfo.ID
                             select a).FirstOrDefault();

                if (isExt != null)
                {
                    if (isExt.IsPass)
                    {
                        return;
                    }
                    else
                    {
                        filterContext.Result = JavaScript("<script>alert('您未拥有 " + actionInfo.Name + "  权限')</script>");
                        return;
                    }
                }
                //2.【用户--角色--权限】

                var loginRole = loginUser.RoleInfo;
                var count = (from r in loginRole
                             from a in r.ActionInfo
                             where a.ID == actionInfo.ID
                             select a).Count();
                if (count<1)
                {
                    filterContext.Result = JavaScript("<script>alert('您未拥有 " + actionInfo.Name + "  权限')</script>");
                    //filterContext.Result = Redirect("/Error.html");
                    return;
                }

            }
            if (!isSucess)
            {
                //filterContext.HttpContext.Response.Redirect("/Login/Index");

                filterContext.Result = Redirect("/Login/Index");
                return;
            }



        }
    }
}