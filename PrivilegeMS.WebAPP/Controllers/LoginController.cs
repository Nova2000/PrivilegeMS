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
            var userinfo = UserInfoService.LoadEntities(u => u.AccountNum == account && u.Pwd == pwd).FirstOrDefault();
            return userinfo != null ? Content("ok") : Content("error");
           
        }
    }
}