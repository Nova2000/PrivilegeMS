using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrivilegeMS.WebAPP.Controllers
{
    public class LoginController : Controller
    {
        IBLL.IUserInfoService UserInfoService=new BLL.UserInfoService();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult UserLogin()
        {
            string account = Request["Name"];
            string pwd = Request["Pwd"];
            var userID = UserInfoService.LoadEntities(u => u.AccountNum == account && u.Pwd == pwd).Select(u=>u.ID).FirstOrDefault();

            if (userID !=0)
            {
                Session["userID"] = userID;
                //HttpCookie cookie = new HttpCookie("session",Session.SessionID);
                return Content("ok");
            }
            else
            {
                return Content("no");
            }
           
        }
        public ActionResult LogOut()
        {
            //if(Session["userInfo"] != null)
            //{
                this.Session.Clear();
                this.Response.Redirect("/Login/Index");
            //}
            return Content("ok");
        }
    }
}